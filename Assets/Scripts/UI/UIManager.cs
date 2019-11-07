using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text coins;
    public Image healthbar1;
    public Image healthbar2;
    public Image healthbar3;
    public Image healthbar4;

    public GameObject loseUi;
    public bool isLoseUiActive;

    void OnEnable()
    {
        GameManager.EndGame += () =>
        {
            isLoseUiActive = true;
        };
    }

    // Update is called once per frame
    void Update()
    {
        coins.text = "Coins: " + playerBaseManager.coins;
        
        //This is very VERY inefficient, update later when the health is settled down.
        if (playerBaseManager.baseHealth > 4)
        {
            healthbar1.fillAmount = 1;
            healthbar2.fillAmount = 1;
            healthbar3.fillAmount = 1;
            healthbar4.fillAmount = 1;
        }
        else if (playerBaseManager.baseHealth < 4)
        {
            healthbar1.fillAmount = 1;
            healthbar2.fillAmount = 1;
            healthbar3.fillAmount = 1;
            healthbar4.fillAmount = 0;

            if (playerBaseManager.baseHealth < 3)
            {
                healthbar3.fillAmount = 0;
                
                if (playerBaseManager.baseHealth < 2)
                {
                    healthbar2.fillAmount = 0;

                    if (playerBaseManager.baseHealth < 1)
                        healthbar1.fillAmount = 0;
                }
            }
        }

        if (isLoseUiActive)
        {
            loseUi.SetActive(true);
        }
        else
        {
            loseUi.SetActive(false);
        }
    }
    public void ReloadScene()
    {
        SceneManager.LoadScene("TestLevel");
    }
}
