using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    private Animator anim;
    private NavMeshAgent agent;
    private EnemyAI enemy;
    
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.state == EnemyAI.EnemyState.Chasing)
        {
            anim.SetBool("IsWalking", true);
        }
        else
        {
            anim.SetBool("IsWalking", false);
        }

        if(enemy.hasAttacked == true)
        {
            anim.SetTrigger("Attack");
        }
    }
}
