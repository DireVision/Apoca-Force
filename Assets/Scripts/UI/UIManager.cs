using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text coins;
    public Text baseHealth;

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
        baseHealth.text = "Base Health: " + playerBaseManager.baseHealth;

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
