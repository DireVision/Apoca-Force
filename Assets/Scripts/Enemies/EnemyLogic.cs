using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyLogic : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform endPoint;

    [Header("Properties")]
    public int maxHealth;
    int health = 10;

    [Header("Economy")]
    public int coinsToGive;                

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    { 
        //Calculate a path for Nav Agent to move towards
        agent.destination = FindObjectOfType<playerBaseManager>().transform.position;

        if (health <= 0)
        {
            //Some functions
            playerBaseManager.coins += coinsToGive;

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bullet(Clone)")
        {
            var theBullet = other.GetComponent<Bullet>();
            health -= theBullet.damageToDeal;
        }
    }
}
