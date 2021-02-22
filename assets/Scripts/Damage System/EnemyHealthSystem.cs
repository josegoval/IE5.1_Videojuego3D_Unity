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
    private EnemyAnimationsController enemyAnimationsController;
    private Animator animator;
    // Features
    public bool hasDeadAnimation = true;
    public float timeToDestroyAfterDeath = 3f;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
        boxCollider = GetComponent<BoxCollider>();
        enemyAnimationsController = GetComponent<EnemyAnimationsController>();
        animator = GetComponent<Animator>();
    }

    protected override void DyingBehaviour()
    {
        if (isDead) return;

        // If hasn't dead animation
        if (!hasDeadAnimation)
        {
            //animator.enabled = false;
            DestroyEnemy();
            //thisRigidbody.AddTorque(-transform.position * 50f);
            return;
        }
        // If it has dead animation
        navMeshAgent.isStopped = true;
        navMeshAgent.velocity = Vector3.zero;
        enemyController.enabled = false;
        boxCollider.isTrigger = false;
        enemyAnimationsController.TriggerIsDead();
        Invoke("DestroyEnemy", 3);
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
