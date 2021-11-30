using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private void OnTriggerEnter(Collider other)
    {
        IDamageable<float> damageTarget = other.gameObject.GetComponent<IDamageable<float>>();

        if (damageTarget != null)
        {
            damageTarget.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
