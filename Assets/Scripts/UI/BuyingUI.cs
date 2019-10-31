using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingUI : MonoBehaviour
{
    public GameObject buyTurretUi;
    public bool isActive;

    public Vector3 receivedPos;

    public GameObject turret1;
    public GameObject turret2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            buyTurretUi.SetActive(true);
        }
        else
        {
            buyTurretUi.SetActive(false);
        }
        buyTurretUi.transform.position = Camera.main.WorldToScreenPoint(receivedPos);
    }
    public void Activate(bool isActivated,Vector3 pos)
    {
        isActive = isActivated;
        receivedPos = pos;
    }
    public void buyTurret1()
    {
        if (playerBaseManager.coins >= turret1.GetComponent<TowerLevel>().cost)
        {
            Instantiate(turret1, receivedPos + Vector3.up, Quaternion.identity);
            playerBaseManager.coins -= turret1.GetComponent<TowerLevel>().cost;
            isActive = false;
        }
    }
    public void buyTurret2()
    {
        if (playerBaseManager.coins >= turret2.GetComponent<TowerLevel>().cost)
        {
            Instantiate(turret2, receivedPos + Vector3.up, Quaternion.identity);
            playerBaseManager.coins -= turret2.GetComponent<TowerLevel>().cost;
            isActive = false;
        }
    }

}
