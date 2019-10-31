using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

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
    /// Target turret is trying to shoot at
    /// </summary>
    [Tooltip("Target turret is trying to shoot at")]
    public Transform target;

    public string enemyTag = ("Enemy");

    public GameObject bullet;
    public Transform firePoint;

    public float fireColdDown;
    TurretManager turretManager;

    public Tile theTile;
    int finalDamage;

    public bool isClickedOn;

    public NavMeshAgent agent;


    private void Start()
    {
        //InvokeRepeating("CheckEnemy", 0, 0.5f);
        turretManager = FindObjectOfType<TurretManager>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        CheckEnemy();
        if (isClickedOn || target == null)
        {
            //Debug.Log("12345");
            transform.rotation = Quaternion.identity;
            return;
        }
        else
        {
            Vector3 dir = target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10).eulerAngles;
            transform.rotation = Quaternion.Euler(0, rotation.y, 0);

            if (fireColdDown <= 0)
            {
                Shoot();
                fireColdDown = 1f / fireRate;
            }
            fireColdDown -= Time.deltaTime;
        }
    }

    void Shoot() 
    {
        GameObject bulletPrefab= (GameObject)Instantiate(bullet, firePoint.position, firePoint.rotation);
        Bullet bulletRef = bulletPrefab.GetComponent<Bullet>();

        if(bulletRef != null)
        {
            bulletRef.Seek(target, CheckTileBonus()); 
        }
    }

    private void OnDrawGizmos() // Draw a range
    {
        Gizmos.color = new Color(1,0,0,0.2f);
        Gizmos.DrawWireSphere(transform.position, range);
    }

    void CheckEnemy() //To check whether enemy is in range by checking the tag and the distance
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (shortestDistance <= range && nearestEnemy != null)
        {
            target = nearestEnemy.transform;
            print("Target is in range");
        }
        else
        {
            target = null;
            print("Target is out of range");
        }

    }

    //Raycast down to check tile
    int CheckTileBonus()
    {
        //Check tile status...
        RaycastHit hit;
        Ray detectTile = new Ray(transform.position, Vector3.down);
        Collider tileCollider;
        if (Physics.Raycast(detectTile, out hit, 1))
        {
            if (hit.collider.tag == "Tile")
            {
                tileCollider = hit.collider;
                theTile = tileCollider.GetComponent<Tile>();

                //... then calculate Damage
                if (theTile.tileBonus != 0)
                {
                    finalDamage = damage * theTile.tileBonus;
                    return finalDamage;
                }
                else
                {
                    Debug.Log("Tower Tile Bonus is equal to zero. Check the tile the tower is on.");
                    return 1;
                }
            }
            else
            {
                Debug.Log("Error: Tower is not detecting a valid tile. Check the raycast settings.");
                return 1;
            }
        }
        else return 1;
    }

    public Text turretName;

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
        set { levelData.description = value; }
    }

    /// <summary>
    /// Gets the tower upgrade description (if available)
    /// </summary>
    public string upgradeDescription
    {
        get { return levelData.upgradeDescription; }
        set { levelData.upgradeDescription = value; }
    }

    /// <summary>
    /// Gets the buy-in cost
    /// </summary>
    public int cost
    {
        get { return levelData.buyInCost; }
        set { levelData.buyInCost = value; }
    }

    /// <summary>
    /// Gets the sell value
    /// </summary>
    public float sell
    {
        get { return levelData.sell; }
        set { levelData.sell = value; }
    }

    /// <summary>
    /// Gets the max health
    /// </summary>
    public float upgradeCost
    {
        get { return levelData.upgradeCost; }
        set { levelData.upgradeCost = value; }
    }

    public int repoCost
    {
        get { return levelData.repoCost; }
        set { levelData.repoCost = value; }
    }

    /// <summary>
    /// Gets the tower sprite
    /// </summary>
    public Sprite icon
    {
        get { return levelData.icon; }
        set { levelData.icon = value; }
    }

    public float range
    {
        get { return levelData.range; }
        set { levelData.range = value; }
    }

    public int damage
    {
        get { return levelData.damage; }
        set { levelData.damage = value; }
    }

    public float moveSpeed
    {
        get { return levelData.redeploySpeed; }
        set { levelData.redeploySpeed = value; }
    }

    public float fireRate
    {
        get { return levelData.fireRate; }
        set { levelData.fireRate = value; }
    }
}
