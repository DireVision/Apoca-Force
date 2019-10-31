using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthEnemy : MonoBehaviour
{
    public int damageToDo;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndZone")
        {
            Debug.Log("Enemy reached base");
            playerBaseManager.baseHealth -= damageToDo;
            Destroy(this.gameObject);
        }
    }
}
