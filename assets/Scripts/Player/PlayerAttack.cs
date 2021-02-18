using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;
    private float timeBetweenShoots = 10000f;
    private float previousFireRate;

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckShoot();
    }

    private void CheckShoot()
    {
        WeaponHandler currentWeapon = weaponManager.getCurrentSelectedWeaponHandler();
        // Checking previous weapon to adapt fireRate
        if (previousFireRate != currentWeapon.fireRate)
        {
            previousFireRate = currentWeapon.fireRate;
            timeBetweenShoots = previousFireRate;
        }

        timeBetweenShoots += Time.deltaTime;

        // Shoot functionality
        if (Input.GetKey(PlayerControlTags.ACTION_1))
        {
            
            if (timeBetweenShoots >= previousFireRate)
            {
                currentWeapon.PlayShootAnimation();
                timeBetweenShoots = 0;
            }
            return;
        }
        //// Non automatic weapon
        //if (Input.GetKeyDown(PlayerControlTags.ACTION_1))
        //{
        //    // For aim weapons
        //    //if (currentWeapon.weaponAimType == WeaponAimType.SELF_AIM)
        //    //{
        //    //    return;
        //    //}
        //    // The rest
        //    currentWeapon.PlayShootAnimation();
        //    if (currentWeapon.projectileType == ProjectileType.BULLET)
        //    {
        //        // ShootBullet();
        //    }
        //}
    }
}
