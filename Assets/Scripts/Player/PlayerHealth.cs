using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;
    public bool isLowHealth;


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= maxHealth / 4)
        {
            isLowHealth = true;
        }
        else
        {
            isLowHealth = false;
        }
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

    public void Heal(int healthGained)
    {
        currentHealth += healthGained;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }
}
