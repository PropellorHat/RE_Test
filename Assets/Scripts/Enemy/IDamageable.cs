using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable<T>
{
    void OnKill();

    void TakeDamage(T damageTaken);
}
