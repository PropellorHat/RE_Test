using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth health;

    public TextMeshProUGUI healthText;

    // Update is called once per frame
    void Update()
    {
        healthText.gameObject.SetActive(health.isLowHealth);
    }
}
