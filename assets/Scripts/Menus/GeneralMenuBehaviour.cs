using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralMenuBehaviour : MonoBehaviour
{
    // General
    public void StartGame()
    {
        SceneManager.LoadScene("1_RoundsSurvivalMap");
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    // Options
    public void ChangeQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }


}
