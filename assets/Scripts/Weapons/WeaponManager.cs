using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weaponHandlers;
    private int currentSelectedWeapon = 0;
    private bool previousWeapon = false;
    private bool nextWeapon = false;

    // Start is called before the first frame update
    void Start()
    {
        weaponHandlers[currentSelectedWeapon].DrawWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        checkNewWeapon();
    }

    private void checkNewWeapon()
    {
        // Mouse wheel input
        if (Input.mouseScrollDelta.y > 0 || nextWeapon)
        {
            changeWeapon(currentSelectedWeapon - 1);
            ChangeNextWeapon(false);
            return;
        }
        if (Input.mouseScrollDelta.y < 0 || previousWeapon)
        {
            changeWeapon(currentSelectedWeapon + 1);
            ChangePreviousWeapon(false);
            return;
        }

        // Alpha inputs
        for (int i = 0; i < WeaponsTags.ALL_SLOTS.Length ; i++)
        {
            if (Input.GetKeyDown(WeaponsTags.ALL_SLOTS[i]))
            {
                changeWeapon(i);
                return;
            }
        }
    }

    private void changeWeapon(int newWeaponSelected)
    {
        if (currentSelectedWeapon == newWeaponSelected) return;

        int nextWeapon = newWeaponSelected < 0 
            ? weaponHandlers.Length - 1 
            : newWeaponSelected < weaponHandlers.Length
                ? newWeaponSelected
                : 0;

        weaponHandlers[currentSelectedWeapon].SaveWeapon();
        weaponHandlers[nextWeapon].DrawWeapon();
        currentSelectedWeapon = nextWeapon;
    }

    public WeaponHandler getCurrentSelectedWeaponHandler() { return weaponHandlers[currentSelectedWeapon]; }

    public void ChangeNextWeapon(bool value)
    {
        nextWeapon = value;
    }public void ChangePreviousWeapon(bool value)
    {
        previousWeapon = value;
    }
}
