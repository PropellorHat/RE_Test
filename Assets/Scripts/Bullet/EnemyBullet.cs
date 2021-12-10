using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyBullet : Bullet
{
    private CinemachineImpulseSource impulseSource;

    private void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageTarget = other.gameObject.GetComponent<IDamageable>();

        if (damageTarget != null)
        {
            impulseSource.GenerateImpulse();
            damageTarget.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
