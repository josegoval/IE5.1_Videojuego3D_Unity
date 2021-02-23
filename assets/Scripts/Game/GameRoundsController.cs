using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoundsController : MonoBehaviour
{
    // Singleton
    public static GameRoundsController singleton;
    // Game state
    [Header("Game states")]
    public int gameRound = 1;
    [HideInInspector]
    public bool gameOver = false;
    // TODO: Game UI
    // Game behaviour
    private int zombiesToCompleteRound = 0;
    // Zombies
    [Header("Zombie Behaviour")]
    public int totalZombiesInTheInitialRound = 7;
    public float secondsBetweenZombieSpawns = 4f;
    public int totalZombiesToIncrementPerRound = 7;
    public int howManyZombiesToSpawnAtOnce = 4;
    public int zombiesIncrementToSpawnAtOnce = 2;
    public GameObject[] zombiePrefabs;
    private int zombiesToSpawn;
    private int zombiesToSpawnAtOnce;
    // Wanderers
    [Header("Wanderer Behaviour")]
    public int totalWanderersInTheInitialRound = 1;
    public float secondsBetweenWanderSpawns = 3f;
    public int totalWanderersToIncrementPerRound = 2;
    public int howManyWanderersToSpawnAtOnce = 1;
    public int wanderersIncrementToSpawnAtOnce = 1;
    public GameObject[] wandererPrefabs;
    private int wanderersToSpawn;
    private int wanderersToSpawnAtOnce;
    // SpawnPoints
    [Header("Spawn Points")]
    [SerializeField]
    private Transform[] zombieSpawnPoints;
    [SerializeField]
    private Transform[] wandererSpawnPoints;
    // References
    [HideInInspector]
    public HealthSystem[] playersHealth;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    void Start()
    {
        StartNextRound();
    }

    public void TryToFinishTheGame()
    {
        if (playersHealth.Length > 0)
        {
            foreach (HealthSystem playerHealthSystem in playersHealth)
            {
                // if there are players alive, continue
                if (!playerHealthSystem.isDead)
                {
                    return;
                }
            }
        }
        // If there are no players finish the game
        FinishTheGame();
    }

    private void FinishTheGame()
    {
        StopAllCoroutines();
        StopAllEnemies();
        // TODO: display endscreen
        // TODO: display buttons to try again or exit
    }

    private void StopAllEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTags.ENEMY_TAG);

        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<EnemyController>().enabled = false;
        }
    }

    void ResetGame()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToReset);
    }

    public void TryToCompleteRound() {
        if (zombiesToCompleteRound <= 0)
        {
            gameRound++;
            StartNextRound();
        }
    }

    private void StartNextRound()
    {
        int roundMultiplier = gameRound - 1;
        // Zombies
        zombiesToSpawn = totalZombiesInTheInitialRound + roundMultiplier * totalZombiesToIncrementPerRound;
        zombiesToSpawnAtOnce = howManyZombiesToSpawnAtOnce + roundMultiplier * zombiesIncrementToSpawnAtOnce;
        // Wanderers
        wanderersToSpawn = totalWanderersInTheInitialRound + roundMultiplier * totalWanderersToIncrementPerRound;
        wanderersToSpawnAtOnce = howManyWanderersToSpawnAtOnce + roundMultiplier * wanderersIncrementToSpawnAtOnce;
        // Round requirements
        zombiesToCompleteRound = zombiesToSpawn;
        // Start spawning
        SpawnZombies();
        SpawnWanderers();
    }

    private void SpawnZombies()
    {
        StartCoroutine(SpawnInRandomPoint(zombiesToSpawn, zombiesToSpawnAtOnce, zombiePrefabs, zombieSpawnPoints, secondsBetweenZombieSpawns));
    }

    private void SpawnWanderers()
    {
        StartCoroutine(SpawnInRandomPoint(wanderersToSpawn, wanderersToSpawnAtOnce, wandererPrefabs, wandererSpawnPoints, secondsBetweenWanderSpawns));
    }

    IEnumerator SpawnInRandomPoint(int enemiesToSpawn, int enemiesToSpawnAtOnce,  GameObject[] prefabs, Transform[] spawnPoints, float timeBetweenSpawns)
    {
        int enemiesLeft = enemiesToSpawn;
        for (int i = 0; i < (enemiesToSpawn/enemiesToSpawnAtOnce + 1); i++)
        {
            // First spawn automatically
            for (int j = 0; j < enemiesToSpawnAtOnce; j++)
            {
                // Instantiate a random prefab in a random point
                Instantiate(
                    prefabs[UnityEngine.Random.Range(0, prefabs.Length - 1)], 
                    spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Length - 1)].position, 
                    Quaternion.identity
                );
                enemiesLeft--;
                // If we're done stop spawning
                if (enemiesLeft <= 0)
                {
                    yield break;
                }
            }

            // Then wait for each bulk spawn
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }
}
