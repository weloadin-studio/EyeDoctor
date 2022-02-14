using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public enum SoundType
{ 
    Sound
}


[System.Serializable]
public class AudioProperties
{
    public string name;
    public bool loop; 
    public GameObject audioFile;

    public SoundType currentSoundType;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioProperties[] audioProperties;

    public int soundEnabled; 
    public int SOUNDENABLED { get { soundEnabled = PlayerPrefs.GetInt("SoundEnabled"); return soundEnabled; } set { soundEnabled = value; PlayerPrefs.SetInt("SoundEnabled", value); } }


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AssignInstance();
    }

    public GameObject PlaySound(SoundType soundType, Transform parent)
    {
        if (SOUNDENABLED == 0)
        {
            GameObject sound = LeanPool.Spawn(audioProperties[(int)soundType].audioFile, parent);
            sound.GetComponent<AudioSource>().Play();
            return sound;
        }
        else
        {
            return null;
        }
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
