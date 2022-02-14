using Lean.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;

    //public GameObject MainMenuScreen, GamePlayScreen, EndScreen;
    //public GameObject StartScreen, PlayButton, NextButton, RestartButton;
    //public GameObject CoinCollect;
    //public GameObject Continue;
    //public string[] Cheers;

    //public Text[] Likes;

    //public GameObject ResetButton;
    //public DynamicJoystick joystick;

    //public GameObject progressBar;
    //public Image progressBarFill;
    //public float meterFillTime;

    //public GameObject[] TUTORIAL;

    //public Text moneyText, stageCompletionText;

    void Awake()
    {
        AssignInstance();
    }

    private void Start()
    {
        //UpdateCash(0);
    }

    //public void SetMeterText(Text meterText, int count, int total)
    //{
    //    meterText.text = count + "/" + total;
    //}

    //public void ToggleMainMenuScreen(bool enable)
    //{
    //    MainMenuScreen.SetActive(enable);
    //    StartScreen.SetActive(enable);
    //}

    //public void ResetPlayerPrefs()
    //{
    //    PlayerPrefs.DeleteAll();
    //}

    //public void CheckTutorial()
    //{
    //    TUTORIAL[0].SetActive(true);
    //}

    //private void DeactivateTutorialScreen()
    //{
    //    for (int i = 0; i < TUTORIAL.Length; i++)
    //    {
    //        if (TUTORIAL[i].activeSelf)
    //            TUTORIAL[i].SetActive(false);
    //    }
    //}

    //public void DisableTutorial(bool delay)
    //{
    //    if (delay)
    //    {
    //        StartCoroutine(DisableTutorialAfterDelay());
    //    }
    //    else
    //    {
    //        DeactivateTutorialScreen();
    //    }
    //}

    //private IEnumerator DisableTutorialAfterDelay()
    //{
    //    yield return new WaitForSeconds(2f);
    //    DeactivateTutorialScreen();
    //}

    //public void StartLevel()
    //{
    //    ToggleMainMenuScreen(false);
    //    GamePlayScreen.SetActive(true);
    //}

    //public void SetProgress(float amount, bool overTime)
    //{
    //    if (overTime)
    //    {
    //        StartCoroutine(UpdateProgress(amount));
    //    }
    //    else
    //    {
    //        progressBarFill.fillAmount += amount;
    //    }
    //}

    //public IEnumerator UpdateProgress(float amount)
    //{
    //    float currentAmount = progressBarFill.fillAmount;
    //    float endAmount = currentAmount + amount;
    //    float t = 0;
    //    while (t < 1)
    //    {
    //        t += Time.unscaledDeltaTime / meterFillTime;
    //        progressBarFill.fillAmount = Mathf.Lerp(currentAmount, endAmount, t);
    //        yield return null;
    //    }
    //}

    //public IEnumerator CompleteProgress(Image fill)
    //{
    //    float currentAmount = fill.fillAmount;
    //    float t = 0;
    //    while (t < 1)
    //    {
    //        t += Time.unscaledDeltaTime / meterFillTime;
    //        fill.fillAmount = Mathf.Lerp(currentAmount, 1, t);
    //        yield return null;
    //    }
    //}

    //public void ResetProgressBar(Image fill)
    //{
    //    fill.fillAmount = 0;
    //}

    //public void UpdateCash(int value)
    //{
    //    PlayerData.Instance.CASHCOUNT += value;
    //    for (int i = 0; i < Likes.Length; i++)
    //    {
    //        float likeConversion = PlayerData.Instance.CASHCOUNT;
    //        Likes[i].text = likeConversion.ToString();
    //    }
    //}

    //public void GetMoney()
    //{
    //    StartCoroutine(Next());
    //}

    //public IEnumerator Next()
    //{
    //    float amount = 50;
    //    moneyText.text = "+" + (int)amount;
    //    CollectCash(amount, CoinCollect);
    //    yield return new WaitForSeconds(1f);
    //    Continue.SetActive(false);
    //    yield return new WaitForSeconds(1f);
    //    EndScreen.SetActive(false);
    //}

    //public void CollectCash(float amount, GameObject collect)
    //{
    //    collect.GetComponent<Cash>().amount = (int)amount;
    //    collect.GetComponent<Cash>().SetText();
    //    collect.GetComponent<Animator>().SetTrigger("Collect");
    //}

    void AssignInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(Instance.gameObject);
                Instance = this;
            }
        }
    }
}