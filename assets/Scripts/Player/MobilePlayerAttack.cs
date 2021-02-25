using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlayerAttack : PlayerAttack
{
    private bool isPressingShootButton = false;

    protected override void CheckShoot()
    {
        // Shoot functionality
        if (isPressingShootButton)
        {
            currentSelectedWeapon.Shoot(mainCamera);
        }
    }

    public void ToggleIsPressingShootButton()
    {
        isPressingShootButton = !isPressingShootButton;
    }
}
