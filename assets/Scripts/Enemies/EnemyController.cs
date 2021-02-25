using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // References
    private EnemyAnimationsController enemyAnimationsController;
    private NavMeshAgent navMeshAgent;
    private GameObject target;
    public DamageTriggerBySphere damageTriggerBySphere;
    private EnemySounds enemySounds;
    // Enemy features and functionalities
    private EnemyStates enemyState;
    private float timePatrolling;
    public float maxTimePatrolling = 2f;
    public float maxPatrollingDistance = 20f;
    public float minPatrollingDistance = 5f;
    private float originalChaseDistance;
    public float minChaseDistance = 10f;
    public float minAttackDistance = 2f;
    private float timeBetweenAttackToChaseGap = 0f;
    public float minTimeBetweenAttackToChaseGap = 1f;
    private float timeBetweenAttacks = 0f;
    public float minTimeBetweenAttacks = 1.2f;
    private bool playedSpottedSound = false;
        // Mobility
    public float walkingSpeed = 0.5f;
    public float runningSpeed = 2f;

    private void Awake()
    {
        enemyAnimationsController = GetComponent<EnemyAnimationsController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemySounds = GetComponent<EnemySounds>();
    }

    // Start is called before the first frame update
    void Start()
    {
        timePatrolling = maxTimePatrolling;
        originalChaseDistance = minChaseDistance;
        target = GameObject.FindGameObjectWithTag(PlayerControlTags.PLAYER_TAG);
    }

    // Update is called once per frame
    void Update()
    {
        SetCurrentState();
        Attack();
        Chase();
        Patrol();
    }

    private void SetCurrentState()
    {
        if (Vector3.Distance(transform.position, target.transform.position) <= minAttackDistance)
        {
            transform.LookAt(target.transform);
            if (enemyState == EnemyStates.ATTACKING) return;

            ResetSpottedSound();
            ResetAttackTime();
            enemyState = EnemyStates.ATTACKING;
            minChaseDistance = originalChaseDistance;
            return;
        }

        if (Vector3.Distance(transform.position, target.transform.position) <= minChaseDistance)
        {
            // If is comes from attackinig tate
            if (enemyState == EnemyStates.ATTACKING)
            {
                timeBetweenAttackToChaseGap += Time.deltaTime;
                if (timeBetweenAttackToChaseGap >= minTimeBetweenAttackToChaseGap)
                {
                    enemyState = EnemyStates.CHASING;
                    ResetAttackTime();
                }
                return;
            }
            // otherwise
            enemyState = EnemyStates.CHASING;
            return;
        }

        if (enemyState == EnemyStates.PATROLLING) return;
        enemyState = EnemyStates.PATROLLING;
        timePatrolling = maxTimePatrolling;
        ResetSpottedSound();
    }

    private void Attack()
    {
        if (enemyState == EnemyStates.ATTACKING)
        {
            enemyAnimationsController.SetIsWalking(false);
            enemyAnimationsController.SetIsRunning(false);
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;

            timeBetweenAttacks += Time.deltaTime;
            if (timeBetweenAttacks >= minTimeBetweenAttacks)
            {
                ChangeAttackAnimations();
                enemySounds.PlayAttackSound();
                ResetAttackTime();
            }
        }
    }

    private void Chase()
    {
        if (enemyState == EnemyStates.CHASING)
        {
            if (!playedSpottedSound)
            {
                // Play spotted sound
                enemySounds.PlaySpottedSound();
                playedSpottedSound = true;
            }

            navMeshAgent.isStopped = false;
            navMeshAgent.speed = runningSpeed;
            ChangeChaseAnimations();

            navMeshAgent.SetDestination(target.transform.position);
        }
    }

    private void Patrol()
    {
        if (enemyState == EnemyStates.PATROLLING)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = walkingSpeed;
            ChangePatrolAnimations();

            timePatrolling += Time.deltaTime;
            if (timePatrolling < maxTimePatrolling) return;

            // Find a location at any cost
            SetNewDestination();
            ResetPatrolTime();
        }
    }

    private void ChangePatrolAnimations()
    {
        enemyAnimationsController.SetIsRunning(false);
        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimationsController.SetIsWalking(true);
            return;
        }
        enemyAnimationsController.SetIsWalking(false);
    }  
    private void ChangeChaseAnimations()
    {
        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimationsController.SetIsRunning(true);
            return;
        }
        enemyAnimationsController.SetIsRunning(false);
    }

    private void ChangeAttackAnimations()
    {
        enemyAnimationsController.TriggerWantToAttack();
    }
    private void SetNewDestination()
    {
        Vector3 randomPoint = (UnityEngine.Random.insideUnitSphere * maxPatrollingDistance) - new Vector3(minPatrollingDistance, minPatrollingDistance, minPatrollingDistance);
        Vector3 newDestination = transform.position + randomPoint;

        NavMeshHit hit;
        NavMesh.SamplePosition(newDestination, out hit, maxPatrollingDistance, -1);
        navMeshAgent.SetDestination(hit.position);
        while (!navMeshAgent.isOnNavMesh)
        {
            NavMesh.SamplePosition(newDestination, out hit, maxPatrollingDistance, -1);
            navMeshAgent.SetDestination(hit.position);
        }
    }

    private void ResetPatrolTime()
    {
        timePatrolling = 0f;
    }
    private void ResetAttackTime()
    {
        timeBetweenAttacks = 0f;
        timeBetweenAttackToChaseGap = 0f;
    }
    private void ResetSpottedSound()
    {
        playedSpottedSound = false;
    }

    public void EnableDamageTriggerBySphere()
    {
        damageTriggerBySphere.EnableDamageTriggerBySphere();
    }public void DisableDamageTriggerBySphere()
    {
        damageTriggerBySphere.DisableDamageTriggerBySphere();
    }
}
