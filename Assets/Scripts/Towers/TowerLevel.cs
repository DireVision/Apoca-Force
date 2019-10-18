using UnityEngine;

/* This script grabs the reference of the ScriptableObject and gets all the data on Initialisation. Any behaviour involving the scipts
 * should go in here and not in TowerLevelData.cs
 * */

/// <summary>
/// An individual level of a tower
/// </summary>
[DisallowMultipleComponent]
public class TowerLevel : MonoBehaviour
{  
    /// <summary>
    /// Reference to scriptable object with level data on it
    /// </summary>
    public TowerLevelData levelData;

    /// <summary>
    /// Gets the tower description
    /// </summary>
    public string description
    {
        get { return levelData.description; }
    }

    /// <summary>
    /// Gets the tower upgrade description (if available)
    /// </summary>
    public string upgradeDescription
    {
        get { return levelData.upgradeDescription; }
    }

    /// <summary>
    /// Gets the cost value
    /// </summary>
    public int cost
    {
        get { return levelData.cost; }
    }

    /// <summary>
    /// Gets the sell value
    /// </summary>
    public int sell
    {
        get { return levelData.sell; }
    }

    /// <summary>
    /// Gets the max health
    /// </summary>
    public int maxHealth
    {
        get { return levelData.maxHealth; }
    }

    /// <summary>
    /// Gets the starting health
    /// </summary>
    public int startingHealth
    {
        get { return levelData.startingHealth; }
    }

    /// <summary>
    /// Gets the tower sprite
    /// </summary>
    public Sprite icon
    {
        get { return levelData.icon; }
    }
}
