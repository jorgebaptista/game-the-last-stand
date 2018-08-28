using System.Collections.Generic;
using UnityEngine;

public class PoolManagerScript : MonoBehaviour
{
    private int childIndex = 0;
    private List<CachedPrefab> cacheList = new List<CachedPrefab>();

    private struct CachedPrefab
    {
        public GameObject prefab;
        public bool sortLayerOrder;
    }

    public int PreCache(GameObject prefab, int initialAmmount = 10, bool sortLayerOrder = true)
    {
        if (prefab == null) Debug.LogError("Pool Manager Precache Method called without prefab argument.");

        foreach (CachedPrefab cachedPrefab in cacheList)
        {
            if (prefab == cachedPrefab.prefab) return cacheList.IndexOf(cachedPrefab);
        }       

        int poolIndex = childIndex++;

        CachedPrefab cache;
        cache.prefab = prefab;
        cache.sortLayerOrder = sortLayerOrder;

        cacheList.Insert(poolIndex, cache);

        new GameObject(prefab.name + " Pool").transform.parent = transform;

        for (int i = 0; i < initialAmmount; ++i)
        {
            GameObject cachedPrefab = Instantiate(prefab, transform.GetChild(poolIndex));

            if (sortLayerOrder)
            {
                SpriteRenderer cachedSpriteRenderer = cachedPrefab.GetComponent<SpriteRenderer>();
                if (cachedSpriteRenderer != null) cachedSpriteRenderer.sortingOrder = i;
            }

            cachedPrefab.SetActive(false);
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

        cachedPrefab = Instantiate(cacheList[poolIndex].prefab, pool);
        if (cacheList[poolIndex].sortLayerOrder)
        {
            SpriteRenderer cachedSpriteRenderer = cachedPrefab.GetComponent<SpriteRenderer>();
            if (cachedSpriteRenderer != null) cachedSpriteRenderer.sortingOrder = pool.childCount - 1;
        }

        return cachedPrefab;
    }
}