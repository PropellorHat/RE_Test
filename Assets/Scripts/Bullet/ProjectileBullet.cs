using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : Bullet
{
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        IDamageable<float> damageTarget = collision.gameObject.GetComponent<IDamageable<float>>();

        if(damageTarget != null)
        {
            damageTarget.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
