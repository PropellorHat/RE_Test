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

    private Vector3 walkPos;
    private Vector3 shootPos;

    public bool isShooting;

    public LayerMask walkableMask;
    public LayerMask shootableMask;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            DoShooting();
        }
        else
        {
            DoMovement();
        }
        
    }

    private void DoMovement()
    {
        isShooting = false;
        
        playerAgent.isStopped = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, walkableMask))
        {
            debugTransform.position = hit.point;
            walkPos = hit.point;
        }
        else
        {
            walkPos = ray.direction * 999f;
            debugTransform.position = walkPos;
        }

        if(Input.GetMouseButtonDown(0))
        {
            playerAgent.SetDestination(walkPos);
        }
    }

    private void DoShooting()
    {
        isShooting = true;
        
        playerAgent.isStopped = true;
        playerAgent.ResetPath(); //include if the player should NOT continue after shooting
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, walkableMask))
        {
            debugTransform.position = hit.point;
            shootPos = hit.point;
        }
        else
        {
            shootPos = ray.direction * 999f;
            debugTransform.position = shootPos;
        }

        transform.LookAt(new Vector3(shootPos.x, transform.position.y, shootPos.z));

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shot a bullet at " + shootPos);
            
            //do shooting
        }
    }
}
