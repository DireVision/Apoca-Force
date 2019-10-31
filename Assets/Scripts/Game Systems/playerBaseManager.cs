using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBaseManager : MonoBehaviour
{
    public static int coins;
    public int startingCoins;
    public int maxHealth;
    public static int baseHealth;

    // Start is called before the first frame update
    void Start()
    {
        baseHealth = maxHealth;
        coins = startingCoins;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
