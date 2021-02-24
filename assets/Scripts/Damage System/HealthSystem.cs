using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float healthPoints = 100f;
    protected float maxHealthPoints;
    public bool isDead = false;

    private void Start()
    {
        maxHealthPoints = healthPoints;
    }

    public virtual void ApplyDamage(float damage)
    {
        healthPoints -= damage;
        healthPoints = Mathf.Clamp(healthPoints, 0, maxHealthPoints);

        ChangeUIValues();

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

    protected virtual void ChangeUIValues() {}
}
