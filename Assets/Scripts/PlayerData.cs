using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;

    private int cashCount;

    public int CASHCOUNT { get { cashCount = PlayerPrefs.GetInt("cashCount"); return cashCount; } set { cashCount = value; PlayerPrefs.SetInt("cashCount", value); } }
   
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AssignInstance();
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
