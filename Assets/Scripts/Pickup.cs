using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Outline1))]
public class Pickup : MonoBehaviour
{
    private Outline1 outline;

    private bool crRunning;

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
        //SetInactive();
    }


    public void SetActive()
    {
        outline.enabled = true;
        //if(!crRunning) StartCoroutine(SetInactiveRoutine());
    }

    public void SetInactive()
    {
        outline.enabled = false;
    }

    public IEnumerator SetInactiveRoutine()
    {
        crRunning = true;
        yield return new WaitForSeconds(0.1f);
        outline.enabled = false;
        crRunning = false;
    }
}
