using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testtt : MonoBehaviour
{
    [SerializeField]
    private GameObject item;

    private int poolIndex;

	// Use this for initialization
	void Start ()
    {
		
	}

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            poolIndex = PoolManagerScript.instance.PreCache(item);
            print(poolIndex);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            GameObject shit = PoolManagerScript.instance.GetCachedPrefab(poolIndex);

            if (shit != null)
            {
                shit.transform.position = transform.position;
                shit.transform.rotation = transform.rotation;
                shit.SetActive(true);
            }
        }
    }
}
