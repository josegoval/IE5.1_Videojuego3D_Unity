using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;
    private WeaponHandler currentSelectedWeapon;
    private Animator firstPersonCameraAnimator;
    private GameObject crosshair;

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        firstPersonCameraAnimator = transform.Find(PlayerControlTags.LOOK_ROOT_TAG).transform.Find(CameraTags.FP_CAMERA_TAG).GetComponent<Animator>();
        crosshair = GameObject.FindWithTag(PlayerControlTags.CROSSHAIR_TAG);
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
            if (Input.GetKey(PlayerControlTags.ACTION_2))
            {
                StartZooming();
            }

            if (Input.GetKeyUp(PlayerControlTags.ACTION_2))
            {
                StopZooming();
            }
        }
    }

    private void StartZooming()
    {
        if (currentSelectedWeapon.weaponAimType == WeaponAimType.AIM)
        {
            firstPersonCameraAnimator.SetBool(CameraTags.IS_ZOOMING_PARAM, true);
            crosshair.SetActive(false);
            return;
        }

        if (currentSelectedWeapon.weaponAimType == WeaponAimType.SELF_AIM)
        {
            currentSelectedWeapon.StartAiming();
            return;
        }
    }
    private void StopZooming()
    {
        if (currentSelectedWeapon.weaponAimType == WeaponAimType.AIM)
        {
            firstPersonCameraAnimator.SetBool(CameraTags.IS_ZOOMING_PARAM, false);
            crosshair.SetActive(true);
            return;
        }

        if (currentSelectedWeapon.weaponAimType == WeaponAimType.SELF_AIM)
        {
            currentSelectedWeapon.StopAiming();
            return;
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
