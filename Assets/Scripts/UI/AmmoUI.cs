using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI totalText;
    public ShootScript shootScript;
    

    // Update is called once per frame
    void Update()
    {
        totalText.text = "AMMO " + shootScript.ammoInMag + "/" + shootScript.heldAmmo;
    }
}
