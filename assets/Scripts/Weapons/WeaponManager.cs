using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weaponHandlers;
    private int currentSelectedWeapon = 0;

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
        if (Input.mouseScrollDelta.y > 0)
        {
            changeWeapon(currentSelectedWeapon - 1);
            return;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            changeWeapon(currentSelectedWeapon + 1);
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
}
