using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline1))]
public class Pickup : MonoBehaviour
{
    private Outline1 outline;

    public enum PickupType
    {
        Health,
        Ammo
    }
    
    public PickupType pickupType;

    public int quantity;
    
    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline1>();
        SetInactive();
    }


    public void SetActive()
    {
        outline.enabled = true;
    }

    public void SetInactive()
    {
        outline.enabled = false;
    }
}
