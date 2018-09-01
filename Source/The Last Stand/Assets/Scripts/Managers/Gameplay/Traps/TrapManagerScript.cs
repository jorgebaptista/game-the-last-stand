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
    [Space]
    [SerializeField]
    private CraftList[] craftList;

    [System.Serializable]
    private struct TrapList
    {
        public TrapType trapType;

        [Space]
        public Sprite hotspotSprite;
        public GameObject prefab;

        [Space]
        public int price;
        public bool isCraftable;
    }

    [System.Serializable]
    private struct CraftList
    {
        public TrapType trapType;
        public TrapType trapType2;

        [Space]
        public Sprite hotspotSprite;
        public GameObject prefab;

        [Space]
        public bool isCraftable;
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
            TrapSpotScript trapScript = trapSpots[i].GetComponent<TrapSpotScript>();

            TrapMode trapMode;
            if (trapType == TrapType.Palisade) trapMode = TrapMode.Defense;
            else trapMode = TrapMode.Offense;

            if (trapScript.trapMode == trapMode)
            {
                if (trapScript.isEmpty)
                {
                    for (int j = 0; j < trapList.Length; ++j)
                    {
                        if (trapList[j].trapType == trapType)
                        {
                            trapScript.HighLight(trapType, trapList[j].hotspotSprite, 
                                trapList[j].prefab, trapList[j].price,trapList[j].isCraftable);
                            break;
                        }
                    }
                }
                else if (trapScript.isCraftable)
                {
                    for (int j = 0; j < craftList.Length; ++j)
                    {
                        if (trapScript.trapType == craftList[j].trapType && trapType == craftList[j].trapType2)
                        {
                            for (int k = 0; k < trapList.Length; ++k)
                            {
                                if (trapList[k].trapType == trapType)
                                {
                                    trapScript.HighLight(trapType, craftList[j].hotspotSprite, 
                                        craftList[j].prefab, trapList[k].price, craftList[j].isCraftable);
                                    break;
                                }
                            }
                        }
                        else if (trapScript.trapType == craftList[j].trapType2 && trapType == craftList[j].trapType)
                        {
                            for (int k = 0; k < trapList.Length; ++k)
                            {
                                if (trapList[k].trapType == trapType)
                                {
                                    trapScript.HighLight(trapType, craftList[j].hotspotSprite, 
                                        craftList[j].prefab, trapList[k].price, craftList[j].isCraftable);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void HideTrapSpots()
    {
        for (int i = 0; i < trapSpots.Length; ++i)
        {
            trapSpots[i].GetComponent<TrapSpotScript>().HideSpotLight();
        }
    }

    public int GetPrice(TrapType trapType)
    {
        int price = 0;

        foreach (TrapList trap in trapList)
        {
            if (trapType == trap.trapType)
            {
                price = trap.price;
                break;
            }
        }

        return price;
    }
}