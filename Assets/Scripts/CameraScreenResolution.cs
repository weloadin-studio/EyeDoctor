using UnityEngine;
using System.Collections;
using Cinemachine;
public class CameraScreenResolution : MonoBehaviour {
	public bool maintainWidth=true;
	[Range(-1,1)]
	public int adaptPosition;
    public  Camera vCam;

	public  float iPhoneX, iPhhone6, iPad;
	Vector3 CameraPos;
    //public static CameraScreenResolution instance;

    private void Awake()
    {
        //AssignInstance();
    }

    // Use this for initialization
    void Start () 
	{
        Invoke("Init", 0.1f);
 	}

    private void Init()
    {
        vCam = GetComponent<Camera>();
        ChangeCamerSize();
    }

  

    public void ChangeCamerSize() 
	{

        if (vCam.aspect <= 0.5f)
        {
            vCam.orthographicSize = iPhoneX;
        }
        else if (vCam.aspect <= 0.6f)
        {
            vCam.orthographicSize = iPhhone6;
        }
        else
        {
            vCam.orthographicSize = iPad;
        }
    }

    //void AssignInstance()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        if (instance != this)
    //        {
    //            Destroy(instance.gameObject);
    //            instance = this;
    //        }
    //    }

    //}
}
