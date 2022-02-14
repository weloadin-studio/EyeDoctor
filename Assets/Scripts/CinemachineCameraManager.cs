using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCameraManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera patientBodyCam;
    [SerializeField] CinemachineVirtualCamera patientEyeCam;

    private void OnEnable()
    {
        CameraSwitch.Register(patientBodyCam);
        CameraSwitch.Register(patientEyeCam);
        CameraSwitch.SwichCamera(patientBodyCam);
    }

    private void OnDisable()
    {
        CameraSwitch.DeRegister(patientBodyCam);
        CameraSwitch.DeRegister(patientEyeCam);
    }

    public void SwitchCameraOnButtonClick()
    {
        CameraSwitch.SwichCamera(patientEyeCam);
    }
}
