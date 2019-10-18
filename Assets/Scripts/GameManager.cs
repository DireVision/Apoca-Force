using System;
using UnityEngine;

/* This script is used to regulate the different activities that are happening in the game.
 * All actions should be passed through here to check for legal actions. (eg. If placing a tower, it should be passed into a state to
 * enable and disable certain functions)
 * 
 * Use delegates (namely Functions & Actions) to broadcast information to other gameobjects (eg. Towers, Enemies)
 * eg. public static Func<int, string> currentHealth;
 * */
public class GameManager : MonoBehaviour
{
    public static Action<bool> waveCompleted;
    public Action<bool> waveStart;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
