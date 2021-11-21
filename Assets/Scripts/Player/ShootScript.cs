using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    private PlayerController playerController;
    
    public enum GunType
    {
        manual,
        auto,
        charge
    }

    public GunType gunType;

    public bool isReloading;
    public bool hasFired;

    public float damage;
    public GameObject bullet;
    public Transform firingPos;
    public int numberOfBullets;
    public float spreadOfBullets;
    public float bulletSpeed;
    public float bulletLifetime;

    public float fireRate;
    [HideInInspector]
    public float fireCooldown;

    public int magSize;
    public int ammoInMag;
    public int ammoCost;
    public float reloadTime;


    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        hasFired = false;
        
        if(playerController.isShooting)
        {
            if(!isReloading)
            {
                switch (gunType)
                {
                    case GunType.manual:
                        ShootManual();
                        break;
                    case GunType.auto:
                        ShootAuto();
                        break;
                    case GunType.charge:
                        Debug.LogWarning("Charge does not exist");
                        break;
                    default:
                        Debug.LogWarning("gun type does not exist");
                        break;
                }
            }


            //firingPos.LookAt(playerController.shootPos);
            
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine("Reload");
        }
    }

    private void ShootManual()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (fireCooldown <= 0f && ammoInMag > 0)
            {
                                
                fireCooldown = fireRate;
                for (int f = 0; f < numberOfBullets; f++)
                {
                    SpawnProjectile();
                }
                ammoInMag -= ammoCost;
                hasFired = true;
            }
        }
        fireCooldown -= 1 * Time.deltaTime;
    }

    private void ShootAuto()
    {
        if (Input.GetButton("Fire1"))
        {
            if (fireCooldown <= 0f && ammoInMag > 0)
            {
                
                fireCooldown = fireRate;
                for (int f = 0; f < numberOfBullets; f++)
                {
                    SpawnProjectile();
                }
                ammoInMag -= ammoCost;
                hasFired = true;
            }
        }
        fireCooldown -= 1 * Time.deltaTime;
    }

    private void SpawnProjectile()
    {
        GameObject bul = Instantiate(bullet, firingPos.transform.position, firingPos.transform.rotation);
        Quaternion randRot = Random.rotation;
        bul.transform.rotation = Quaternion.RotateTowards(bul.transform.rotation, randRot, spreadOfBullets);
        Bullet bulStats = bul.GetComponent<Bullet>();
        bulStats.damage = damage;
        bulStats.speed = bulletSpeed;
        Destroy(bul, bulletLifetime);
    }

    public IEnumerator Reload()
    {
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);
        ammoInMag = magSize;

        isReloading = false;
    }
}
