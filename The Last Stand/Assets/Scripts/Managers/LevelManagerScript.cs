using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    [Header("General")]
    [Space]
    [SerializeField]
    private float buildModeTime = 30f;

    [Header("General - Debug")]
    [Space]
    [SerializeField]
    public int currentMoney = 0;
    [Space]
    [SerializeField]
    public bool buildMode = true;

    [HideInInspector]
    public bool isPaused = false;
    public static LevelManagerScript instance;

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

    private void Start()
    {
        
    }

    public void IncreaseMoney(int money)
    {
        currentMoney += money;
        UIManagerScript.instance.UpdateMoneyText(currentMoney);
    }
    public void DecreaseMoney(int cost)
    {
        //*******************
    }

    public void GameOver()
    {
        //*******************
    }
    public void Victory()
    {
        //*******************
    }
}
