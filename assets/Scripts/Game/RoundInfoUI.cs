using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundInfoUI : MonoBehaviour
{
    private AudioSource audioSource;
    public Text roundText;
    public float roundTextDuration = 3f;
    public Text enemyCounter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ChangeEnemyCounter(int enemies)
    {
        enemyCounter.text = "Zombies: " + enemies.ToString();
    }

    public void ChangeRound(int round)
    {
        roundText.text = "ROUND " + round.ToString();
        roundText.gameObject.SetActive(true);
        audioSource.Play();
        StartCoroutine(DisableAfter(roundTextDuration, roundText.gameObject));
    }

    IEnumerator DisableAfter(float seconds, GameObject toDisable)
    {
        yield return new WaitForSeconds(seconds);
        toDisable.SetActive(false);
    }

}
