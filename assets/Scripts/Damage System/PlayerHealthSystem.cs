using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthSystem : HealthSystem
{
    // References
    private PlayerController playerController;
    private MouseController mouseController;
    private WeaponManager weaponManager;
    private PlayerAttack playerAttack;
    public InfoBar healthInfoBar;
    // Features
    public float resetGameTimeAfterDeath = 3f;
    public string sceneToReset = "0_MainScene";

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        mouseController = GetComponent<MouseController>();
        weaponManager = GetComponent<WeaponManager>();
        playerAttack = GetComponent<PlayerAttack>();
    }

    protected override void ChangeUIValues()
    {
        healthInfoBar.updateData(healthPoints, maxHealthPoints);
    }

    protected override void DyingBehaviour()
    {
        playerController.enabled = false;
        mouseController.enabled = false;
        weaponManager.getCurrentSelectedWeaponHandler().gameObject.SetActive(false);
        weaponManager.enabled = false;
        playerAttack.enabled = false;

        StopAllEnemies();

        Invoke("ResetGame", resetGameTimeAfterDeath);
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneToReset);
    }
}
