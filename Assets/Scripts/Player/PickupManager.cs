using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    private PlayerHealth health;
    private ShootScript shootScript;

    public float reachRadius;
    public LayerMask pickupMask;

    public List<Pickup> pickups;
    
    // Start is called before the first frame update
    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        shootScript = GetComponent<ShootScript>();
    }

    // Update is called once per frame
    void Update()
    {
        pickups.Clear();
        Collider[] pickupsInRange = Physics.OverlapSphere(transform.position + Vector3.up, reachRadius, pickupMask, QueryTriggerInteraction.Collide);
        if(pickupsInRange.Length > 0)
        {
            for (int i = 0; i < pickupsInRange.Length; i++)
            {
                pickups.Add(pickupsInRange[i].GetComponent<Pickup>());
            }
        }
        
        

        if(Input.GetKeyDown(KeyCode.E) && pickups.Count > 0)
        {
            switch (pickups[0].pickupType)
            {
                case Pickup.PickupType.Health:
                    health.TakeDamage(-pickups[0].quantity);
                    Destroy(pickups[0].gameObject);
                    pickups.Clear();
                    return;

                case Pickup.PickupType.Ammo:
                    shootScript.heldAmmo += pickups[0].quantity;
                    Destroy(pickups[0].gameObject);
                    pickups.Clear();
                    return;

                default:
                    Debug.LogWarning(pickups[0].name + " has no type");
                    break;
            }
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < pickups.Count; i++)
        {
            pickups[i].SetActive();
        }
        //pickups[0].SetActive();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, reachRadius);
    }
}
