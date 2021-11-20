using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager
{
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCamera = null;
    
    public static bool IsActiveCamera(CinemachineVirtualCamera camera)
    {
        if(camera == ActiveCamera)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        for (int c = 0; c < cameras.Count; c++)
        {
            cameras[c].Priority = 0;
        }

        camera.Priority = 10;
        ActiveCamera = camera;
    }

    public static void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
        Debug.Log(camera.name + " registered");
    }

    public static void Unregister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
        Debug.Log(camera.name + " unregistered");
    }
}
