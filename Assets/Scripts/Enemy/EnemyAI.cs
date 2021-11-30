using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    
    public bool isHunting;
    public float detectRange;
    public float attackRange;

    public enum EnemyState
    {
        Idle,
        Chasing,
        Attacking
    }
    public EnemyState state = EnemyState.Idle;

    private float distToPlayer;

    public bool hasAttacked;
    public float attackRate;
    private float attackCooldown;
    public GameObject bullet;
    public Transform firingPos;
    public float damage;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        distToPlayer = (player.position - transform.position).magnitude;

        switch (state)
        {
            case EnemyState.Idle:
                Idle();
                break;

            case EnemyState.Chasing:
                Chase();
                break;

            case EnemyState.Attacking:
                Attack();
                break;

            default:
                break;
        }

        attackCooldown -= Time.deltaTime;
    }

    private void Idle()
    {
        agent.isStopped = true;
        
        if (distToPlayer < detectRange)
        {
            state = EnemyState.Chasing;
        }
    }
    
    private void Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if(distToPlayer < attackRange)
        {
            state = EnemyState.Attacking;
        }
    }

    private void Attack()
    {
        agent.isStopped = true;

        if (attackCooldown < 0f)
        {
            hasAttacked = true;

            //do the attack here
            GameObject bul = Instantiate(bullet, firingPos.transform.position, firingPos.transform.rotation);
            Bullet bulStats = bul.GetComponent<Bullet>();
            bulStats.damage = damage;

            attackCooldown = attackRate;
        }

        if(distToPlayer > attackRange)
        {
            state = EnemyState.Chasing;
        }
    }

    private void LateUpdate()
    {
        hasAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
