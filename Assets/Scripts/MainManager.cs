using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour {

    private static MainManager mainManager;
    public string currentSceneName;
    public string nextSceneName;
    private AsyncOperation resourceUnloadTask;
    private AsyncOperation sceneLoadTask;
    private enum SceneState { Reset, Preload, Load, Unload, Postload, Ready, Run, Count };
    private SceneState sceneState;
    private delegate void UpdateDelegate();
    private UpdateDelegate[] updateDelegates;
    public GameObject fadeScreen, fadeCanvas;
    private bool stopStates;
    public bool oldDevice;
    public Text loadingProgress;
    public Image loadingFill;

    public float fadeTime = 1;
    public bool isFading;

    [HideInInspector]
    public bool offlineScreenShown, appStarted;

    public static void SwitchScene(string nextSceneName)
    {
        if (mainManager != null)
        {
            if (mainManager.currentSceneName != nextSceneName)
            {
                mainManager.nextSceneName = nextSceneName;
            }
            else
            {
                mainManager.sceneState = SceneState.Reset;
            }

        }
    }

    protected void Awake()
    {
        DontDestroyOnLoad(gameObject);
        mainManager = this;
        stopStates = true;

        QualitySettings.vSyncCount = 0;  // VSync must be disabled
        Application.targetFrameRate = 60;

        StartCoroutine(LoadFirstScene(0));

    }

    private IEnumerator LoadFirstScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        updateDelegates = new UpdateDelegate[(int)SceneState.Count];

        //Set each updateDelegate
        updateDelegates[(int)SceneState.Reset] = UpdateSceneReset;
        updateDelegates[(int)SceneState.Preload] = UpdateScenePreload;
        updateDelegates[(int)SceneState.Load] = UpdateSceneLoad;
        updateDelegates[(int)SceneState.Unload] = UpdateSceneUnload;
        updateDelegates[(int)SceneState.Postload] = UpdateScenePostload;
        updateDelegates[(int)SceneState.Ready] = UpdateSceneReady;
        updateDelegates[(int)SceneState.Run] = UpdateSceneRun;

        nextSceneName = "Level";
        stopStates = false;
        sceneState = SceneState.Reset;
    }

    protected void OnDestroy()
    {
        if (updateDelegates != null)
        {
            for (int i = 0; i < (int)SceneState.Count; i++)
            {
                updateDelegates[i] = null;
            }
            updateDelegates = null;
        }

        if (mainManager != null)
        {
            mainManager = null;
        }
    }

    public void LoadingScreen(bool status)
    {
        fadeCanvas.SetActive(status);
        fadeScreen.SetActive(status);
    }

    public void ProcessingScreen(bool status)
    {
        //LoadingCanvas.SetActive(status);
        //processingScreen.SetActive(status);
    }

    public IEnumerator FadeIn()
    {
        if (!fadeCanvas.activeSelf && fadeScreen.activeSelf)
        {
            LoadingScreen(true);
        }
        isFading = true;
        fadeScreen.GetComponent<Animator>().SetTrigger("Fade In");
        yield return new WaitForSeconds(fadeTime);
        isFading = false;
        LoadingScreen(false);
    }

    public IEnumerator FadeOut()
    {
        LoadingScreen(true);
        isFading = true;
        fadeScreen.GetComponent<Animator>().SetTrigger("Fade Out");
        yield return new WaitForSeconds(fadeTime);
        isFading = false;
    }

    public static MainManager Instance
    {
        get
        {
            if (mainManager == null)
            {

                mainManager = GameObject.FindObjectOfType<MainManager>();
            }
            return mainManager;
        }
    }

    protected void Update()
    {
        if(!stopStates)
        {
            if (updateDelegates[(int)sceneState] != null)
            {
                updateDelegates[(int)sceneState]();
            }
        }
    }

    private void UpdateSceneReset()
    {        
            // run a gc pass
        System.GC.Collect();
        sceneState = SceneState.Preload;        
    }

    private void UpdateScenePreload()
    {
        if (nextSceneName == "GamePlay" || nextSceneName == "MainMenu")
        {
            stopStates = true;
            StartCoroutine(LoadingHaloIn());
        }
        else
        {
            sceneLoadTask = SceneManager.LoadSceneAsync(nextSceneName);
            sceneLoadTask.allowSceneActivation = false;
            sceneState = SceneState.Load;
        }
    }

    private void UpdateSceneLoad()
    {
        // done loading?
        if (sceneLoadTask.isDone == true)
        {
            sceneState = SceneState.Unload;
        }
        else
        {          
           if(nextSceneName == "GamePlay" ||  nextSceneName == "MainMenu")
            {
                stopStates = true;
                StartCoroutine(LoadingHaloOut());
            }
            else
            {
                if (nextSceneName == "Level")
                {
                    if (loadingProgress && loadingFill)
                    {
                        loadingProgress.text = (sceneLoadTask.progress * 100).ToString("F0") + "%";
                        //loadingFill.fillAmount = sceneLoadTask.progress;
                    }
                }
                sceneLoadTask.allowSceneActivation = true;
            }
        }
    }

    private IEnumerator LoadingHaloOut()
    {
        yield return new WaitForSeconds(0);
        sceneLoadTask.allowSceneActivation = true;
        stopStates = false;
        StopCoroutine(LoadingHaloOut());
    }

    private IEnumerator LoadingHaloIn()
    {
        //LoadingScreen(true);
        //AudioManager.Instance.MinimizeVolume();
        yield return new WaitForSeconds(0);
        sceneLoadTask = SceneManager.LoadSceneAsync(nextSceneName);
        sceneLoadTask.allowSceneActivation = false;
        sceneState = SceneState.Load;
        stopStates = false;
        StopCoroutine(LoadingHaloIn());
    }

    private void UpdateSceneUnload()
    {
        if (resourceUnloadTask == null)
        {
            resourceUnloadTask = Resources.UnloadUnusedAssets();
        }
        else
        {
            if (resourceUnloadTask.isDone == true)
            {
                resourceUnloadTask = null;
                sceneState = SceneState.Postload;
            }
        }
    }

    private void UpdateScenePostload()
    {
        currentSceneName = nextSceneName;
        sceneState = SceneState.Ready;
    }

    private void UpdateSceneReady()
    {
        //LoadingScreen(false);
        System.GC.Collect();
        sceneState = SceneState.Run;
      
    }

    // wait for scene change
    private void UpdateSceneRun()
    {
        if (currentSceneName != nextSceneName)
        {
            sceneState = SceneState.Reset;
        }
    }
}
