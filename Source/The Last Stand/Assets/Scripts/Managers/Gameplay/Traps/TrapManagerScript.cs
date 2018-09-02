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
    }

    private GameObject[] trapSpots;

    private void Awake()
    {
        trapSpots = GameObject.FindGameObjectsWithTag("Trap");
    }

    public void HighlightSpots(TrapType trapType)
    {
        foreach (GameObject trapSpot in trapSpots)
        {
            TrapSpotScript trapSpotScript = trapSpot.GetComponent<TrapSpotScript>();

            //can be optimized
            TrapMode trapMode;
            if (trapType == TrapType.Palisade) trapMode = TrapMode.Defense;
            else trapMode = TrapMode.Offense;

            if (trapSpotScript.trapMode == trapMode && trapSpotScript.isEmpty)
            {
                foreach (TrapList trap in trapList)
                {
                    if (trap.trapType == trapType)
                    {
                        trapSpotScript.HighLight(trapType, trap.hotspotSprite, trap.prefab, trap.price, trap.isCraftable);
                        break;
                    }
                }
            }
            else if (trapSpotScript.isCraftable)
            {
                foreach (CraftList craft in craftList)
                {
                    if (trapSpotScript.trapType == craft.trapType && trapType == craft.trapType2 || 
                        trapSpotScript.trapType == craft.trapType2 && trapType == craft.trapType)
                    {
                        foreach (TrapList trap in trapList)
                        {
                            if (trap.trapType == trapType)
                            {
                                trapSpotScript.HighLight(trapType, craft.hotspotSprite, craft.prefab, trap.price, false);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    public void HideTrapSpots()
    {
        foreach (GameObject trapSpot in trapSpots)
        {
            trapSpot.GetComponent<TrapSpotScript>().HideSpotLight();
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