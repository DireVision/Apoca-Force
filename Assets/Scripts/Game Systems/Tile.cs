using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Tile : MonoBehaviour
{
    public Transform selectedTurret;
    TurretManager turretManager;
    TowerLevel towerLevel;

    public bool isLock;
    public bool canBuy;
    Renderer cubeRenderer;

    public int tileBonus;

    public bool isTeleporting;

    Vector3 tilePosition;

    // Start is called before the first frame update
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        isLock = false;
        turretManager = FindObjectOfType<TurretManager>();
        towerLevel = FindObjectOfType<TowerLevel>();
    }

    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, 2)) // check if there is turret above the tile
        {
            if (hit.transform.tag == "Player")
            {
                isLock = true;
            }
        }
        else
        {
            isLock = false;
        }
    }

    private void OnMouseOver() // TO check the position of the tile and place the turret on top if mouse move to the tile
    {
        if (isLock)
        {
            return;
        }
        else
        {
            if (turretManager.currentTurret != null && turretManager.isTeleporting)
            {
                //cubeRenderer.material.SetColor("_Color", Color.green);
                //InstantTeleport();
                //MovingTurret();
            }
        }
    }
    private void OnMouseExit()
    {
        cubeRenderer.material.SetColor("_Color", Color.white);
    }

    void InstantTeleport()
    {
        cubeRenderer.material.SetColor("_Color", Color.green);
        print("is not lock");
        turretManager.currentTurret.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
    }
}
