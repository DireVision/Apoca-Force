using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class TurretManager : MonoBehaviour
{

    public Transform currentTurret;
    public bool isClick;

    public bool isTeleporting;

    public BuyingUI buyingUi;

    // Start is called before the first frame update
    void Start()
    {
        isClick = false;
        isTeleporting = false;
        buyingUi = FindObjectOfType<BuyingUI>();
    }

    // Update is called once per frame
    void Update()
    {
        SelectTurret();

        if (currentTurret == null)
        {
            isClick = false;
        }
        if (currentTurret != null && isTeleporting)
        {
            if (currentTurret.GetComponent<TowerLevel>().agent.hasPath == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    currentTurret.transform.LookAt(new Vector3(hit.point.x, currentTurret.GetComponent<TowerLevel>().transform.position.y, hit.point.z));
                }
            }
            MovingTurret();
        }
    }

    public void SelectTurret() // leftClick to select and rightClick to place turret
    {
        if (Input.GetMouseButtonDown(0)) // select turret if mouse move over the turret
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Player" && isClick == false)
                {
                    isClick = true;
                    //hit.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    hit.transform.GetComponent<BoxCollider>().enabled = false;

                    currentTurret = hit.transform;
                    currentTurret.GetComponent<TowerUI>().isSelected = true;
                    currentTurret.GetComponent<TowerLevel>().isClickedOn = true;
                    print("I clicked on turret");
                }

                if (hit.transform.tag == "Tile")
                {
                    Tile theTile;
                    theTile = hit.transform.GetComponent<Tile>();
                    if (theTile.isLock == false && theTile.canBuy == true)
                    {
                        buyingUi.Activate(true, theTile.transform.position);
                    }
                }
            }
        }
        /*
        else if (Input.GetMouseButtonDown(1)) // place the turret
        {
            if (currentTurret != null)
            {
                currentTurret.transform.GetComponent<BoxCollider>().enabled = true;
                currentTurret.GetComponent<TowerUI>().isSelected = false;
                currentTurret.GetComponent<TowerLevel>().isClickedOn = false;
                currentTurret = null;
                isClick = false;
                isTeleporting = false;
            }
            buyingUi.Activate(false, Vector3.zero);
        }
        */
    }
    void MovingTurret()
    {
        TowerLevel selectedTurret = currentTurret.GetComponent<TowerLevel>();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                //Assign destination
                //Vector3 targetedPosition = new Vector3(hit.transform.position.x, hit.transform.position.y + 1, hit.transform.position.z);
                Vector3 targetedPosition = hit.point;
                selectedTurret.agent.SetDestination(targetedPosition);
                //goto fix;
                currentTurret.GetComponent<TowerUI>().isSelected = false;            //This 2 lines
                currentTurret.GetComponent<TowerUI>().DeductMoney();

                if (currentTurret != null)
                {
                    currentTurret.transform.GetComponent<BoxCollider>().enabled = true;
                    currentTurret.GetComponent<TowerUI>().isSelected = false;
                    currentTurret.GetComponent<TowerLevel>().isClickedOn = false;
                    currentTurret = null;
                    isClick = false;
                    isTeleporting = false;
                }
            }
        }
        /*
        fix:
        //Fix shit up
        if (Time.timeScale >= 1.0f && selectedTurret.agent.hasPath)
        {
            NavMeshHit navhit;
            float maxAgentTravelDistance = Time.deltaTime * selectedTurret.agent.speed;

            //If at the end of path, stop agent.
            if (
                selectedTurret.agent.SamplePathPosition(NavMesh.AllAreas, maxAgentTravelDistance, out navhit) ||
                selectedTurret.agent.remainingDistance <= selectedTurret.agent.stoppingDistance
               )
            {
                //Stop agent
                selectedTurret.agent.updatePosition = true;
                selectedTurret.transform.position = selectedTurret.agent.nextPosition;
            }
            //Else, move the actor and manually update the agent pos
            else
            {
                selectedTurret.agent.updatePosition = false;
                selectedTurret.transform.position = new Vector3(navhit.position.x, navhit.position.y, navhit.position.z);
                selectedTurret.agent.nextPosition = selectedTurret.transform.position; //Set simulated agent to the gameObject's position
                Debug.Log("Agent is moving");
            }
        }
        */
    }
}