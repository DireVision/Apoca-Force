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

            //Press mouse 2 to reset
            if (Input.GetMouseButtonDown(2))
            {
                towerUiSub.SetActive(false);
                isSelected = false;

                theTurretMnager.currentTurret.GetComponent<BoxCollider>().enabled = true;
                theTurretMnager.currentTurret = null;
            }
        }
        else
        {
            towerUiSub.SetActive(false);
        }    
    }

    public void Upgrade()
    {
        if(playerBaseManager.coins > costToUpgrade)
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
            //playerBaseManager.coins -= costToReposition;
            Debug.Log("Reposition!");
            theTurretMnager.isTeleporting = true;
        }
        else
        {
            Debug.Log("Not enough to reposition...");
        }

        towerUiSub.SetActive(false);
    }

    public void DeductMoney() //temporary fix for deducting money only when repositioning
    {
        playerBaseManager.coins -= costToReposition;
    }
}
