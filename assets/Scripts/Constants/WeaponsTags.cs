using UnityEngine;
using System.Collections;


public class WeaponsTags {
    // Animation parameters
    public const string PARAMETER_SHOOT = "Shoot";
    public const string PARAMETER_IS_AIMING = "isAiming";
    // Weapon keyboard hotkeys
    public const KeyCode SLOT_0 = KeyCode.Alpha1;
    public const KeyCode SLOT_1 = KeyCode.Alpha2;
    public const KeyCode SLOT_2 = KeyCode.Alpha3;
    public const KeyCode SLOT_3 = KeyCode.Alpha4;
    public const KeyCode SLOT_4 = KeyCode.Alpha5;
    public const KeyCode SLOT_5 = KeyCode.Alpha6;
    public static KeyCode[] ALL_SLOTS = { SLOT_0, SLOT_1, SLOT_2, SLOT_3, SLOT_4, SLOT_5 };
}
