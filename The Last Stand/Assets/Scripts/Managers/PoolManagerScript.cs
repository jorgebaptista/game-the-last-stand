using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManagerScript : MonoBehaviour
{
    private int childIndex = 0;

    private List<GameObject> prefabList = new List<GameObject>();

    public static PoolManagerScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public int PreCache(GameObject prefab, int initialAmmount = 4)
    {
        int poolIndex = childIndex++;
        prefabList.Insert(poolIndex ,prefab);

        int currentSortingOrder = 0;

        new GameObject(prefab.name + " Pool").transform.parent = transform;

        for (int i = 0; i < initialAmmount; ++i)
        {
            GameObject cachedPrefab = Instantiate(prefab, transform.GetChild(poolIndex));
            cachedPrefab.GetComponent<SpriteRenderer>().sortingOrder = currentSortingOrder++;
        }

        return poolIndex;
    }

    public GameObject GetCachedPrefab(int poolIndex)
    {
        Transform pool = transform.GetChild(poolIndex);
        GameObject cachedPrefab;

        for (int i = 0; i < pool.childCount; ++i)
        {
            cachedPrefab = pool.GetChild(i).gameObject;

            if (!cachedPrefab.activeInHierarchy)
            {
                return cachedPrefab;
            }
        }

        cachedPrefab = Instantiate(prefabList[poolIndex], pool);
        cachedPrefab.GetComponent<SpriteRenderer>().sortingOrder = pool.childCount - 1;

        return cachedPrefab;
    }
}
