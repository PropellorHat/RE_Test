using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnKill()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        if (currentHealth <= 0f)
        {
            OnKill();
        }
    }
}
