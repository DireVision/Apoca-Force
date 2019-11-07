using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

/* This script is used to regulate the different activities that are happening in the game.
 * All actions should be passed through here to check for legal actions. (eg. If placing a tower, it should be passed into a state to
 * enable and disable certain functions)
 * 
 * Use delegates (namely Functions & Actions) to broadcast information to other gameobjects (eg. Towers, Enemies)
 * eg. public static Func<int, string> currentHealth;
 * Then the GameManager will listen and make the right decision.
 * */
public class GameManager : MonoBehaviour
{
    public enum PhaseState
    {
        Prep, Action
    };
    public static PhaseState currentPhase;

    Text phaseUI;

    public bool unitDeployed;
    public float prepTimer = 30f;
    public Text timerText;
    public static Action EndGame;

    private void OnEnable()
    {
        //Subscribe to the wave completed action
        WaveManager.waveCompleted += () => StartCoroutine("PrepCounter");

        phaseUI = GameObject.Find("Phase Name").GetComponent<Text>();
    }

    private void Start()
    {
        currentPhase = PhaseState.Prep;
        StartCoroutine(PrepCounter());
    }

    void Update()
    {
        if (playerBaseManager.baseHealth <= 0)
        {
            Debug.Log("You lose!");
            Time.timeScale = 0;
            EndGame();
        }
    }

    IEnumerator PrepCounter()
    {
        FindObjectOfType<WaveManager>().enabled = false;
        phaseUI.text = "Prep Phase";

        //While timer is not up and max units to move are up.
        for (float timer = prepTimer; timer >= 0; timer -= Time.deltaTime)
        {
            if (unitDeployed)
            {
                break;
            }
            else
            {
                timerText.text = ((int)timer).ToString();
                yield return null;
            }
        }
 
        //Move over to action phase
        Debug.Log("Prep Phase is over, proceeding to Action Phase...");
        phaseUI.text = "Action Phase";
        timerText.text = "";
        currentPhase = PhaseState.Action;
        FindObjectOfType<WaveManager>().enabled = true;
    }
}
