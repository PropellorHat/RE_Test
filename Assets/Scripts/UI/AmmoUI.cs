using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI magText;
    public TextMeshProUGUI totalText;
    public ShootScript shootScript;
    

    // Update is called once per frame
    void Update()
    {
        magText.text = shootScript.ammoInMag.ToString();
        totalText.text = shootScript.heldAmmo.ToString();
    }
}
