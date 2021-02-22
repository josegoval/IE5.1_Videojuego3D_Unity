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
    public float walkingSpeed = 1f;
    public float runningSpeed = 4f;

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
    }

    private void Patrol()
    {
        if (enemyState == EnemyStates.PATROLLING)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.speed = walkingSpeed;

            timePatrolling += Time.deltaTime;
            if (timePatrolling < maxTimePatrolling) return;

            // Find a location at any cost
            SetNewDestination();
            ResetPatrolTime();
            ChangePatrolAnimations();
        }
    }

    private void ChangePatrolAnimations()
    {
        if (navMeshAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnimationsController.SetIsWalking(true);
            return;
        }
        enemyAnimationsController.SetIsWalking(false);
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
        timePatrolling = 0;
    }
}
