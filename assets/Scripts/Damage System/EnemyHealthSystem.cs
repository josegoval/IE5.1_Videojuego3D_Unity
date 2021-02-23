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
    //private Animator animator;
    private EnemySounds enemySounds;
    // Features
    public bool hasDeadAnimation = true;
    public float timeToDestroyAfterDeath = 3f;
    public float timeDelayForDeadSound = 0.3f;
    public bool isValidForCompleteRound = false;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyController = GetComponent<EnemyController>();
        boxCollider = GetComponent<BoxCollider>();
        enemyAnimationsController = GetComponent<EnemyAnimationsController>();
        //animator = GetComponent<Animator>();
        enemySounds = GetComponent<EnemySounds>();
    }

    protected override void DyingBehaviour()
    {
        if (isDead) return;

        // Try to complete the round
        if (isValidForCompleteRound)
        {
            GameRoundsController.singleton.RemoveEnemyRequired();
        }
        GameRoundsController.singleton.TryToCompleteRound();

        // If hasn't dead animation
        if (!hasDeadAnimation)
        {
            //animator.enabled = false;
            enemySounds.PlayDeadSound();
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
        StartCoroutine(PlayDelayedDeadSound());
        Invoke("DestroyEnemy", 3);
    }

    void DestroyEnemy()
    {
        Destroy(gameObject);
    }

    IEnumerator PlayDelayedDeadSound()
    {
        yield return new WaitForSeconds(timeDelayForDeadSound);
        enemySounds.PlayDeadSound();
    }
}
