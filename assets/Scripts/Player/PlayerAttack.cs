using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;
    private WeaponHandler currentSelectedWeapon;
    private Animator firstPersonCameraAnimator;

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        firstPersonCameraAnimator = transform.FindChild(PlayerControlTags.LOOK_ROOT_TAG).transform.FindChild(CameraTags.FP_CAMERA_TAG).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentSelectedWeapon = weaponManager.getCurrentSelectedWeaponHandler();
        CheckAim();
        CheckShoot();
    }

    private void CheckAim()
    {
        if (currentSelectedWeapon.weaponAimType != WeaponAimType.NONE)
        {
            if (Input.GetKeyDown(PlayerControlTags.ACTION_2))
            {

            }
        }
    }

    private void CheckShoot()
    {
        // Shoot functionality
        if (Input.GetKey(PlayerControlTags.ACTION_1))
        {
            currentSelectedWeapon.Shoot();
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
