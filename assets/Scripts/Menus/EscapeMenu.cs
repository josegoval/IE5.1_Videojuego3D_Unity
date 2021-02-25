using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeMenu : MonoBehaviour
{
    public GameObject menu;
    public PlayerController playerController;
    public MouseController mouseController;

    // Update is called once per frame
    void Update()
    {
        TryScapeMenu();
    }

    private void TryScapeMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu.SetActive(true);
            mouseController.enabled = false;
            playerController.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
        }
    }

    public void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
        menu.SetActive(false);
        mouseController.enabled = true;
        playerController.enabled = true;
    }
}
