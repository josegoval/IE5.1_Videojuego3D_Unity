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
    public float fireRate;
    // Weapon functionality
    private float timeBetweenShoots;
    private bool isSelected = false;
        // TODO: AttackPoint
    // Object references
    public GameObject MuzzleFlash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        timeBetweenShoots = fireRate;
    }

    private void Update()
    {
        // Update time
        if (isSelected)
        {
            timeBetweenShoots += Time.deltaTime;
        }
    }

    public void Shoot()
    {
        
        if (timeBetweenShoots >= fireRate)
        {
            PlayShootAnimation();
            timeBetweenShoots = 0;
        }
    }

    public void DrawWeapon()
    {
        gameObject.SetActive(true);
        timeBetweenShoots = fireRate;
        isSelected = true;
    }

    public void SaveWeapon()
    {
        gameObject.SetActive(false);
        isSelected = false;
    }

    public void PlayShootAnimation()
    {
        animator.SetTrigger(WeaponsTags.PARAMETER_SHOOT);
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


}
