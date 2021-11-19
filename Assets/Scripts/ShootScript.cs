using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    public Transform firePos;
    
    public float damage;
    public float fireRate;
    private float cooldown;
    public float magSize;
    public float reloadTime;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnShoot(Vector3 shootTarget, LayerMask mask)
    {
        if (Physics.Linecast(firePos.position, shootTarget, out RaycastHit hit, mask))
        {
            
        }
    }
}
