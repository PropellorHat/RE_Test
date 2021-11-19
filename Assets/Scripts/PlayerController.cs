using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    public Transform debugTransform;
    private Camera mainCam;

    private Vector3 targetPos;

    public LayerMask clickableMask;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        DoMovement();
    }

    private void DoMovement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, clickableMask))
        {
            debugTransform.position = hit.point;
            targetPos = hit.point;
        }
        else
        {
            targetPos = ray.direction * 999f;
            debugTransform.position = targetPos;
        }

        if(Input.GetMouseButtonDown(0))
        {
            playerAgent.SetDestination(targetPos);
        }
    }
}
