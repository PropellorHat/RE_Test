using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    private NavMeshAgent playerAgent;
    private PlayerController playerCont;
    private Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        playerAgent = GetComponent<NavMeshAgent>();
        playerCont = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Speed", playerAgent.velocity.magnitude);
        anim.SetBool("IsShooting", playerCont.isShooting);
    }
}
