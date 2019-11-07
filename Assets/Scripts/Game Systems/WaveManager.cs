using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Enemy Class containing parameters
[Serializable]
public class Enemy
{
    public enum EnemyType
    {
        Normal, Fast
    };
    public EnemyType enemyToSpawn;

    public int timeToSpawn;
}

/* This script holds the different Enemies that should spawn and sets a time for them to spawn. This is basically the whole entire
 * wave in one script.
 * */

public class WaveManager : MonoBehaviour
{
    /// <summary>
    /// When a wave is finished, subscribe to this.
    /// </summary>
    public static Action waveCompleted;

    /// <summary>
    /// This list holds all the enemies that will spawn this wave.
    /// </summary>
    public List<Enemy> enemyList = new List<Enemy>();

    public GameObject normalEnemy;
    public GameObject fastEnemy;

    void OnEnable()
    {
        StartCoroutine("SpawnEnemy");
    }

    //Spawn Enemy Every spawnCD seconds
    IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            yield return new WaitForSeconds(enemyList[i].timeToSpawn);

            if (enemyList[i] == null)
            {
                Debug.Log("Warning: Enemy List at index" + i + "is incomplete.");
            }
            else
            {
                switch(enemyList[i].enemyToSpawn)
                {
                    case Enemy.EnemyType.Normal:
                        Instantiate(normalEnemy, transform.position, Quaternion.identity);
                        break;

                    case Enemy.EnemyType.Fast:
                        Instantiate(fastEnemy, transform.position, Quaternion.identity);
                        break;
                }
                
            }
        }

        WaveEnd();
    }

    //Set action = null for failsafe when no extra stuff is to be done.
    void WaveEnd()
    {
        //Check for a receiver
        if (waveCompleted != null)
        {
            waveCompleted();
        }
    }
}
