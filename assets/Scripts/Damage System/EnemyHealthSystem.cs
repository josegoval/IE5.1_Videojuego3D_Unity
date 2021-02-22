using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealthSystem : HealthSystem
{
    // References
    private NavMeshAgent navMeshAgent;
    private EnemyController enemyController;
    private BoxCollider boxCollider;
    private Rigidbody thisRigidbody;
    private EnemyAnimationsController enemyAnimationsController;
    // Features
    public bool hasDeadAnimation = true;
    public float timeToDestroyAfterDeath = 3f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
        boxCollider = GetComponent<BoxCollider>();
        thisRigidbody = GetComponent<Rigidbody>();
        enemyAnimationsController = GetComponent<EnemyAnimationsController>();
    }

    protected override void DyingBehaviour()
    {
        Invoke("DestroyEnemy", 3);

        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        enemyController.enabled = false;
        boxCollider.isTrigger = false;
        // If hasn't dead animation
        if (!hasDeadAnimation)
        {
            thisRigidbody.AddTorque(-transform.position * 50f);
            return;
        }
        // If it has dead animation
        enemyAnimationsController.TriggerIsDead();
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
