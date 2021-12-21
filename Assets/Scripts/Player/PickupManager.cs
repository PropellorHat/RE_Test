using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PickupManager : MonoBehaviour
{
    private PlayerInput playerInput;
    
    private PlayerHealth health;
    private ShootScript shootScript;

    public float reachRadius;
    public LayerMask pickupMask;

    public List<Pickup> pickups;
    //public Collider[] pickupCols;
    //private Pickup pickupToGrab;
    
    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        
        health = GetComponent<PlayerHealth>();
        shootScript = GetComponent<ShootScript>();
    }

    private void Start()
    {
        playerInput.OnPickup += PlayerInput_DoPickup;
    }

    private void OnDestroy()
    {
        playerInput.OnPickup -= PlayerInput_DoPickup;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(((1 << other.gameObject.layer) & pickupMask) != 0)
        {
            Pickup currPickup = other.gameObject.GetComponent<Pickup>();
            if(!pickups.Contains(currPickup))
            {
                pickups.Add(currPickup);
                currPickup.SetActive();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (((1 << other.gameObject.layer) & pickupMask) != 0)
        {
            Pickup currPickup = other.gameObject.GetComponent<Pickup>();
            if (pickups.Contains(currPickup))
            {
                pickups.Remove(currPickup);
                currPickup.SetInactive();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        ///PHYSICS.OVERLAPSPHERE METHOD
        /*pickupCols = Physics.OverlapSphere(transform.position + Vector3.up, reachRadius, pickupMask, QueryTriggerInteraction.Collide);
        if(pickupCols.Length > 0)
        {
            pickupToGrab = pickupCols[0].GetComponent<Pickup>();
            pickupToGrab.SetActive();
        }
        else
        {
            pickupToGrab = null;
        }*/
    }

    private void PlayerInput_DoPickup()
    {
        if (pickups.Count > 0)
        {
            Pickup currPickup = pickups[0];
            switch (currPickup.pickupType)
            {
                case Pickup.PickupType.Health:
                    health.Heal(currPickup.quantity);
                    pickups.Remove(currPickup);
                    Destroy(currPickup.gameObject);
                    break;

                case Pickup.PickupType.Ammo:
                    shootScript.heldAmmo += currPickup.quantity;
                    pickups.Remove(currPickup);
                    Destroy(currPickup.gameObject);
                    break;

                default:
                    Debug.LogWarning(currPickup.name + " has no type");
                    break;
            }
        }

        ///PHYSICS.OVERLAPSPHERE METHOD
        /*if (pickupToGrab != null)
        {
            switch (pickupToGrab.pickupType)
            {
                case Pickup.PickupType.Health:
                    health.Heal(pickupToGrab.quantity);
                    Destroy(pickupToGrab.gameObject);
                    break;

                case Pickup.PickupType.Ammo:
                    shootScript.heldAmmo += pickupToGrab.quantity;
                    Destroy(pickupToGrab.gameObject);
                    break;

                default:
                    Debug.LogWarning(pickupToGrab.name + " has no type");
                    break;
            }
        }*/

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + Vector3.up, reachRadius);
    }
}
