using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    public NavMeshAgent playerAgent;
    public Transform debugTransform;
    private Camera mainCam;

    private ShootScript shootScript;

    private Vector3 walkPos;
    [HideInInspector]
    public Vector3 shootPos;

    public bool isShooting;

    public enum PlayerState
    {
        Walking,
        Shooting,
        Reloading,
        Grapple
    }
    public PlayerState pState;

    public LayerMask walkableMask;
    public LayerMask shootableMask;

    // Start is called before the first frame update
    void Awake()
    {
        mainCam = Camera.main;

        shootScript = GetComponent<ShootScript>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerInput.OnAimToggle += PlayerInput_AimToggle;
        playerInput.OnWalk += PlayerInput_SetDestination;
        playerInput.OnAimDown += PlayerInput_OnAimDown;
        playerInput.OnAimUp += PlayerInput_OnAimUp;
        
        pState = PlayerState.Walking;
    }

    

    private void OnDestroy()
    {
        playerInput.OnAimToggle -= PlayerInput_AimToggle;
        playerInput.OnWalk -= PlayerInput_SetDestination;
        playerInput.OnAimDown -= PlayerInput_OnAimDown;
        playerInput.OnAimUp -= PlayerInput_OnAimUp;
    }

    // Update is called once per frame
    void Update()
    {
        switch (pState)
        {
            case PlayerState.Walking:
                DoMovement();
                break;

            case PlayerState.Shooting:
                DoShooting();
                break;

            case PlayerState.Reloading:
                DoReload();
                break;

            case PlayerState.Grapple:

                break;

            default:
                break;
        }
    }

    private void DoReload()
    {
        isShooting = false;

        playerAgent.isStopped = true;
        playerAgent.ResetPath();
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

        /*if(Input.GetMouseButtonDown(0))
        {
            playerAgent.SetDestination(walkPos);
        }*/

    }

    private void DoShooting()
    {
        isShooting = true;
        
        playerAgent.isStopped = true;
        playerAgent.ResetPath(); //include if the player should NOT continue after shooting
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, shootableMask))
        {
            debugTransform.position = hit.point;
            shootPos = hit.point + (ray.direction * 0.5f);
        }
        else
        {
            shootPos = ray.direction * 999f;
            debugTransform.position = shootPos;
        }

        //transform.LookAt(new Vector3(shootPos.x, transform.position.y, shootPos.z));
        Vector3 aimDir = new Vector3(shootPos.x, transform.position.y, shootPos.z);
        aimDir = (aimDir - transform.position).normalized;
        transform.forward = Vector3.Lerp(transform.forward, aimDir, Time.deltaTime * 15f);

    }

    private void PlayerInput_AimToggle()
    {
        if(pState == PlayerState.Walking)
        {
            pState = PlayerState.Shooting;
        }
        else if(pState == PlayerState.Shooting)
        {
            pState = PlayerState.Walking;
        }
    }

    private void PlayerInput_SetDestination()
    {
        playerAgent.SetDestination(walkPos);
    }
    
    private void PlayerInput_OnAimUp()
    {
        if (pState == PlayerState.Shooting)
        {
            pState = PlayerState.Walking;
        }
    }

    private void PlayerInput_OnAimDown()
    {
        if (pState == PlayerState.Walking)
        {
            pState = PlayerState.Shooting;
        }
    }
}
