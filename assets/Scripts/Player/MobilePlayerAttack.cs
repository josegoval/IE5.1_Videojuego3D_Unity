using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobilePlayerAttack : PlayerAttack
{
    private bool isPressingAimButton = false;
    private bool isPressingShootButton = false;

    protected override bool IsPressingShootAction()
    {
        return isPressingShootButton;
    }

    protected override bool IsPressingAimAction()
    {
        return isPressingAimButton;
    }
    protected override bool IsReleasingShootAction()
    {
        return !isPressingAimButton;
    }

    public void ToggleIsPressingAimButton()
    {
        isPressingAimButton = !isPressingAimButton;
    }
    public void ToggleIsPressingShootButton()
    {
        isPressingShootButton = !isPressingShootButton;
    }

}
