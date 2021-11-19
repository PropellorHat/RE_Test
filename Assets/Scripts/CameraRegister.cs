using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRegister : MonoBehaviour
{
    private CinemachineVirtualCamera cam;
    
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();

        CameraManager.Register(cam);
    }
}
