using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerUI : MonoBehaviour
{
    public GameObject towerUiSub;

    int costToUpgrade;
    int costWhenSold;
    public int costToReposition;

    public bool isSelected = false;

    public TurretManager theTurretMnager;

    public GameObject nextUpgrade;


    // Start is called before the first frame update
    void Start()
    {
        theTurretMnager = FindObjectOfType<TurretManager>();
        costToUpgrade = (int)GetComponent<TowerLevel>().upgradeCost;
        costWhenSold = (int)GetComponent<TowerLevel>().sell;
        costToReposition = (int)GetComponent<TowerLevel>().repoCost;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 uipos = Camera.main.WorldToScreenPoint(this.transform.position);
        towerUiSub.transform.position = uipos;

        if (isSelected)
        {
            towerUiSub.SetActive(true);
        }
        else
        {
            towerUiSub.SetActive(false);
        }    
    }

    public void Upgrade()
    {
        if(playerBaseManager.coins > costToUpgrade && costToUpgrade != 0)
        {
            if (nextUpgrade != null)
            {
                playerBaseManager.coins -= costToUpgrade;
                Debug.Log("Upgraded!");
                towerUiSub.SetActive(false);

                Instantiate(nextUpgrade, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("Tower is at max level");
            }
        }
        else
        {
            Debug.Log("Not enough to upgrade!");
        }
    }

    public void Sell()
    {
        playerBaseManager.coins += costWhenSold;
        Destroy(gameObject);

        towerUiSub.SetActive(false);
    }


    public void Reposition()
    {
        if (playerBaseManager.coins > costToReposition)
        {
            playerBaseManager.coins -= costToReposition;
            Debug.Log("Reposition!");
            theTurretMnager.isTeleporting = true;
        }
        else
        {
            Debug.Log("Not enough to reposition...");
        }

        towerUiSub.SetActive(false);
    }
}
