using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [Header("PoolManager Settings")]
    [Space]
    [SerializeField]
    private Transform bulletPool;
    [SerializeField]
    private Transform enemyPool;

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject enemyPrefab;

    [Space]
    [SerializeField]
    private int initialNumberOfBullets = 3;
    [SerializeField]
    private int initialNumberofEnemies = 10;

    [Space]
    [SerializeField]
    private bool canExpandBulletPool;
    [SerializeField]
    private bool canExpandEnemyPool;

    [Header("PoolManager Properties")]
    [Space]
    private int numberOfBulletsInPool;
    private int numberOfEnemiesInPool;

    private GameObject bulletInPool;
    private GameObject enemyInPool;

    private GameObject[] allEnemiesInPool;

    private void Start()
    {
        //**************************
        int currentSortingOrder = 0;

        for (int instantiatedBullets = 0; instantiatedBullets < initialNumberOfBullets; ++instantiatedBullets)
        {
            Instantiate(bulletPrefab, bulletPool);
        }
        for (int instantiatedEnemies = 0; instantiatedEnemies < initialNumberofEnemies; ++instantiatedEnemies)
        {
            //*******************************            
            GameObject InstantiatedEnemy = Instantiate(enemyPrefab, enemyPool);
            InstantiatedEnemy.GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder;
            currentSortingOrder++;
        }
    }

    public GameObject GetAvaiableBullet()
    {
        numberOfBulletsInPool = bulletPool.childCount;

        for (int bulletIndex = 0; bulletIndex < numberOfBulletsInPool; ++bulletIndex)
        {
            bulletInPool = bulletPool.GetChild(bulletIndex).gameObject;
            if (!bulletInPool.activeInHierarchy)
            {
                return bulletInPool;
            }
        }

        if (canExpandBulletPool)
        {
            bulletInPool = Instantiate(bulletPrefab, bulletPool);
        }
        return null;
    }
    public GameObject GetAvaiableEnemy()
    {
        numberOfEnemiesInPool = enemyPool.childCount;

        for (int enemyIndex = 0; enemyIndex < numberOfEnemiesInPool; ++enemyIndex)
        {
            enemyInPool = enemyPool.GetChild(enemyIndex).gameObject;
            if (!enemyInPool.activeInHierarchy)
            {
                return enemyInPool;
            }
        }

        if (canExpandEnemyPool)
        {
            enemyInPool = Instantiate(enemyPrefab, enemyPool);
        }
        return null;
    }

    public GameObject[] GetAllEnemiesInPool()
    {
        allEnemiesInPool = new GameObject[enemyPool.childCount];

        for (int enemyIndex = 0; enemyIndex < numberOfEnemiesInPool; ++enemyIndex)
        {
            allEnemiesInPool[enemyIndex] = enemyPool.GetChild(enemyIndex).gameObject; 
        }

        return allEnemiesInPool;
    }



}
