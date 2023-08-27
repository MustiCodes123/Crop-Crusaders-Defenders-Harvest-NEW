using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CameraChanger : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private Camera[] cams;
    [SerializeField] private Button button;
    [SerializeField] private CinemachineVirtualCameraBase[] virtualCameras;
    
    int active_Camera_Index = 0;
    void Start()
    {
        for(int i=1;i<cams.Length;i++)
        {
            cams[i].enabled = false;
        }

        button.onClick.AddListener(ChangeCamera);
    }

    public void ActivateVirtualCamera(int cameraIndex)
    {
        // Check if the provided camera index is within the valid range
        if (cameraIndex >= 0 && cameraIndex < virtualCameras.Length)
        {
            // Set the priority of the desired virtual camera to a high value to activate it
            for (int i = 0; i < virtualCameras.Length; i++)
            {
                if (i == cameraIndex)
                {
                    virtualCameras[i].Priority = 12; // Set a high value, e.g., 10, to activate this camera
                }
                else
                {
                    virtualCameras[i].Priority = 0; // Set a low value, e.g., 0, for other cameras
                }
            }
        }
        else
        {
            Debug.LogError("Invalid camera index provided.");
        }
    }

    void ChangeCamera()
    {
        cams[active_Camera_Index].enabled = false;
        active_Camera_Index = (active_Camera_Index + 1) % cams.Length;
        cams[active_Camera_Index].enabled = true;
         ActivateVirtualCamera(active_Camera_Index);
    }
}
