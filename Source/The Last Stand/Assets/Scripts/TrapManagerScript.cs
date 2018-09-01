using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapType
{
    Palisade,
    Spikes,
    Tar
}

public enum TrapMode
{
    Defense,
    Offense
}

public class TrapManagerScript : MonoBehaviour
{
    [Header("Trap Settings")]
    [Space]
    [SerializeField]
    private TrapList[] trapList;

    [System.Serializable]
    private struct TrapList
    {
        public TrapType trapType;
        public Sprite hotspotSprite;
        public GameObject prefab;
    }

    private GameObject[] trapSpots;

    private void Awake()
    {
        trapSpots = GameObject.FindGameObjectsWithTag("Trap");
    }

    public void HighlightSpots(TrapType trapType)
    {
        for (int i = 0; i < trapSpots.Length; ++i)
        {
            TrapScript trapScript = trapSpots[i].GetComponent<TrapScript>();

            TrapMode trapMode;
            if (trapType == TrapType.Palisade) trapMode = TrapMode.Defense;
            else trapMode = TrapMode.Offense;

            if (trapScript.trapMode == trapMode && trapScript.isAvaiable)
            {
                for (int j = 0; j < trapList.Length; ++j)
                {
                    if (trapList[j].trapType == trapType)
                    {
                        trapSpots[i].GetComponent<SpriteRenderer>().sprite = trapList[j].hotspotSprite;
                        break;
                    }
                }
            }
        }
    }

    public void HideTrapSpots()
    {
        for (int i = 0; i < trapSpots.Length; ++i)
        {
            trapSpots[i].GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}