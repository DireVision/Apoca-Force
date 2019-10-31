using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    public float size;
    Vector3 truePos;
    private void Start()
    {
        GameObject[] target = GameObject.FindGameObjectsWithTag("Player"); 
    }
    private void LateUpdate() // any gameobject with player tag will snap to the grid;
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject tower in towers)
        {
            truePos.x = Mathf.Floor(tower.transform.position.x / size) * size;
            truePos.y = Mathf.Floor(tower.transform.position.y / size) * size;
            truePos.z = Mathf.Floor(tower.transform.position.z / size) * size;

            tower.transform.position = truePos;
        }
    }
}
