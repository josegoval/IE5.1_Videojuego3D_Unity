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
    // Enemy features and functionalities
    private EnemyStates enemyState;
    private Vector3 previousPosition;
    private float timePatrolling;
    public float maxTimePatrolling = 2f;
    public float maxPatrollingDistance = 200f;
    public float minPatrollingDistance = 5f;
        // Mobility
    public float walkingSpeed = 2f;
    public float runningSpeed = 5f;

    private void Awake()
    {
        enemyAnimationsController = GetComponent<EnemyAnimationsController>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyState = EnemyStates.PATROLLING;
        timePatrolling = maxTimePatrolling;
    }

    // Update is called once per frame
    void Update()
    {
        Patrol();
        SetIdleIfItIsNotMoving();
    }

    private void SetIdleIfItIsNotMoving()
    {
        if (enemyState != EnemyStates.IDLE && navMeshAgent.velocity != Vector3.zero)
        {
            enemyAnimationsController.SetIsRunning(false);
            enemyAnimationsController.SetIsWalking(false);
            navMeshAgent.speed = 0f;
        }
    }

    private void Patrol()
    {
        if (enemyState == EnemyStates.PATROLLING)
        {
            timePatrolling += Time.deltaTime;
            if (timePatrolling < maxTimePatrolling) return;

            enemyAnimationsController.SetIsWalking(true);
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = walkingSpeed;

            // Find a location at any cost
            SetNewDestination();

            ResetPatrolTime();
        }
    }

    private void SetNewDestination()
    {
        Vector3 randomPoint = (UnityEngine.Random.insideUnitSphere * maxPatrollingDistance) - new Vector3(minPatrollingDistance, minPatrollingDistance, minPatrollingDistance);

        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, maxPatrollingDistance, -1);
        navMeshAgent.SetDestination(hit.position);
        while (!navMeshAgent.isOnNavMesh)
        {
            NavMesh.SamplePosition(randomPoint, out hit, maxPatrollingDistance, -1);
            navMeshAgent.SetDestination(hit.position);
        }
    }

    private void ResetPatrolTime()
    {
        timePatrolling = 0;
    }
}
