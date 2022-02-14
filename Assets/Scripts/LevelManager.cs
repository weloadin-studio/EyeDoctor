using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;
using SubjectNerd.Utilities;

[Serializable]
public class LevelProperties
{
    public string name;
    public Sprite sprite;
    public string levelPrefab;
}

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int checkLevel;

    private int currentLevel, levelCount;

    [Reorderable]
    public LevelProperties[] levels;

    public GameObject loadedLevel;
    public string levelStartType;

    public int LEVELCOUNT { get { levelCount = PlayerPrefs.GetInt("levelCount"); return levelCount; } set { levelCount = value; PlayerPrefs.SetInt("levelCount", value); } }
    public int CURRENTLEVEL { get { currentLevel = PlayerPrefs.GetInt("Level"); return currentLevel; } set { currentLevel = value; PlayerPrefs.SetInt("Level", value); } }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AssignInstance();
    }

    private void Start()
    {
        StartCoroutine(Init());
    }

    public IEnumerator Init()
    {
        yield return null;
        LoadLevel();
    }

    public void LoadLevel()
    {
        if (loadedLevel)
        {
            Destroy(loadedLevel);
            loadedLevel = null;
        }
        LeanPool.Spawn(Resources.Load("Levels/" + levels[CURRENTLEVEL].levelPrefab) as GameObject);
    }

    public void ChangeLevel()
    {
        LEVELCOUNT++;
        CURRENTLEVEL++;
        if (CURRENTLEVEL >= levels.Length)
        {
            CURRENTLEVEL = 0;
        }
        LoadLevel();
    }

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
