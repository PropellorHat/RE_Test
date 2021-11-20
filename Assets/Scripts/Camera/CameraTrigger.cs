using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera linkedCam;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CameraManager.SwitchCamera(linkedCam);
            Debug.Log("Switched Camera to " + linkedCam.name);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;

        Gizmos.DrawLine(transform.position, linkedCam.transform.position);
    }
}
