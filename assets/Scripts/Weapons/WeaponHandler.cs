using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    private Animator animator;
    // Weapon sounds
    [SerializeField]
    private AudioSource ShootAudioSourceSound, ReloadAudioSourceSound;
    // Weapon characteristics
    public WeaponFireType weaponFireType;
    public ProjectileType projectileType;
    public WeaponAimType weaponAimType;
    public GameObject projectile;
    public Transform projectileSpawnSpot;
    public float fireRate;
    public float projectileDamage = 50f;
    public AnimationClip drawAnimation;
    // Weapon functionality
    private float timeBetweenShoots;
    private bool isSelected = false;
    private bool isReady = false;
    private float drawTime;
    // Object references
    public GameObject MuzzleFlash;
    public DamageTriggerBySphere damageTriggerBySphere;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        ResetDrawRestrictions();
    }

    private void Update()
    {
        CheckIfIsReadyToShoot();
        // Update time
        if (isSelected && isReady)
        {
            timeBetweenShoots += Time.deltaTime;
        }
    }

    private void CheckIfIsReadyToShoot()
    {
        if (!isReady)
        {
            drawTime -= Time.deltaTime;
            isReady = drawTime <= 0;
        }
    }

    public void Shoot(Camera mainCamera)
    {
        
        if (isReady && timeBetweenShoots >= fireRate && PlayShootAnimation())
        {
            timeBetweenShoots = 0;

            LaunchProjectile(mainCamera);
        }
    }

    private void LaunchProjectile(Camera mainCamera)
    {
        if (projectileType == ProjectileType.NONE) return;

        // Raycast projectile
        if (projectileType == ProjectileType.BULLET)
        {
            LaunchRaycast(mainCamera);
        }

        // Physical projectile
        if (CanLaunchProjectile())
        {
            GameObject projectile = Instantiate(this.projectile, projectileSpawnSpot);
            projectile.GetComponent<ProjectileHandler>().SpawnAndLauchProjectile(mainCamera, projectileDamage);
        }
    }

    private void LaunchRaycast(Camera mainCamera)
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            GameObject target = hit.transform.gameObject;
            if (target.tag == EnemyTags.ENEMY_TAG)
            {
                target.GetComponent<HealthSystem>().ApplyDamage(projectileDamage);
            }
        }
    }

    private bool CanLaunchProjectile()
    {
        return projectileType == ProjectileType.SPEAR || projectileType == ProjectileType.ARROW;
    }

    public void DrawWeapon()
    {
        gameObject.SetActive(true);
        ResetDrawRestrictions();
        isSelected = true;
    }

    private void ResetDrawRestrictions()
    {
        drawTime = drawAnimation.length;
        isReady = false;
        timeBetweenShoots = fireRate;
    }

    public void SaveWeapon()
    {
        gameObject.SetActive(false);
        isSelected = false;
    }

    public bool PlayShootAnimation()
    {
        if (weaponAimType == WeaponAimType.SELF_AIM && !IsAiming()) return false;

        animator.SetTrigger(WeaponsTags.PARAMETER_SHOOT);
        return true;
    }
    public void PlayShootSound()
    {
        ShootAudioSourceSound.Play();
    }
    public void PlayReloadSound()
    {
        ReloadAudioSourceSound.Play();
    }

    public void StartAiming()
    {
        animator.SetBool(WeaponsTags.PARAMETER_IS_AIMING, true);
    }
    public void StopAiming()
    {
        animator.SetBool(WeaponsTags.PARAMETER_IS_AIMING, false);
    }
    public bool IsAiming()
    {
        return animator.GetBool(WeaponsTags.PARAMETER_IS_AIMING);
    }

    public void EnableDamageTriggerBySphere()
    {
        damageTriggerBySphere.EnableDamageTriggerBySphere();
    }
    public void DisableDamageTriggerBySphere()
    {
        if (damageTriggerBySphere.gameObject.activeInHierarchy)
        {
            damageTriggerBySphere.DisableDamageTriggerBySphere();
        }
    }

    public void ActiveMuzzleFlash()
    {
        MuzzleFlash.gameObject.SetActive(true);
    }
    public void DeactiveMuzzleFlash()
    {
        MuzzleFlash.gameObject.SetActive(false);
    }
}
