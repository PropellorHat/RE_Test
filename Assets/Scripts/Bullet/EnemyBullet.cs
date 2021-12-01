using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageTarget = other.gameObject.GetComponent<IDamageable>();

        if (damageTarget != null)
        {
            damageTarget.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
