﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float healthPoints = 100f;
    protected bool isDead = false;

    public void ApplyDamage(float damage)
    {
        healthPoints -= damage;

        if (healthPoints <= 0)
        {
            DyingBehaviour();
            isDead = true;
        }
    }

    protected virtual void DyingBehaviour()
    {
        print("No dying behaviour implemented");
    }
}
