using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTriggerBySphere : MonoBehaviour
{
    public float damagePoints = 2f;
    public float sphereRadius = 1f;
    public LayerMask layerMaskToAttack;

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius, layerMaskToAttack);
        if (colliders.Length > 0)
        {
            colliders[0].gameObject.GetComponent<HealthSystem>().ApplyDamage(damagePoints);

            DisableDamageTriggerBySphere();
        }
    }

    public void DisableDamageTriggerBySphere() {
        gameObject.SetActive(false);
    } 
    public void EnableDamageTriggerBySphere() {
        gameObject.SetActive(true);
    }
}
