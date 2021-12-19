using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerInput playerInput;
    
    public enum GunType
    {
        manual,
        auto,
        charge
    }

    public GunType gunType;

    public bool isReloading;
    public bool hasFired;

    [Header("Bullet Stats")]
    public int damage;
    public GameObject bullet;
    public Transform firingPos;
    public int numberOfBullets;
    public float spreadOfBullets;
    public float bulletSpeed;
    public float bulletLifetime;

    [Header("Speed")]
    public float fireRate;
    [HideInInspector]
    public float fireCooldown;

    [Header("Reload")]
    public int magSize;
    public int ammoInMag;
    public int ammoCost;
    public float reloadTime;

    [Header("Ammo")]
    public int heldAmmo;


    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        playerInput.OnReload += PlayerInput_DoReload;
        playerInput.OnShoot += PlayerInput_DoShoot;
    }

    private void OnDestroy()
    {
        playerInput.OnReload -= PlayerInput_DoReload;
        playerInput.OnShoot -= PlayerInput_DoShoot;
    }

    // Update is called once per frame
    void Update()
    {
        hasFired = false;
        /*hasFired = false;

        if (playerController.isShooting)
        {
            if (!isReloading)
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

        }*/

        fireCooldown -= 1 * Time.deltaTime;
    }

    private void PlayerInput_DoReload()
    {
        StartCoroutine("Reload");
    }

    private void PlayerInput_DoShoot()
    {
        

        if (playerController.isShooting && !isReloading)
        {
            switch (gunType)
            {
                case GunType.manual:
                    ShootManual();
                    break;
                case GunType.auto:
                    //ShootAuto();
                    Debug.LogWarning("Auto does not exist");
                    break;
                case GunType.charge:
                    Debug.LogWarning("Charge does not exist");
                    break;
                default:
                    Debug.LogWarning("gun type does not exist");
                    break;
            }
        }
    }

    private void ShootManual()
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

    /*private void ShootAuto()
    {
        if (fireCooldown <= 0f && ammoInMag > 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
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
    }*/

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
        playerController.pState = PlayerController.PlayerState.Reloading;
        isReloading = true;

        yield return new WaitForSeconds(reloadTime);

        //heldAmmo -= magSize - ammoInMag;
        //ammoInMag = magSize;
        int curAmmoInMag = ammoInMag;

        for (int i = 0; i < magSize - curAmmoInMag; i++)
        {
            if(heldAmmo > 0)
            {
                heldAmmo--;
                ammoInMag++;
            }
        }
        

        isReloading = false;
        playerController.pState = PlayerController.PlayerState.Walking;
    }
}
