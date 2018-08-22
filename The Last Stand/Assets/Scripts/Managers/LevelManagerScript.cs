using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    [Header("General")]
    [Space]
    [SerializeField]
    private float buildTime = 30f;

    [Header("General - Debug")]
    [Space]
    public int currentMoney = 0;

    public static LevelManagerScript instance;

    [HideInInspector]
    public bool buildMode = true;
    private float baseTimer;

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
        ToggleBuildMode(true);
    }

    private void FixedUpdate()
    {
        if (buildMode)
        {
            if (Time.time > baseTimer) ToggleBuildMode(false);
            else UIManagerScript.instance.UpdateTimer(Mathf.CeilToInt(baseTimer - Time.time));
        }
    }

    public void ToggleBuildMode(bool toggle)
    {
        buildMode = toggle;

        if (buildMode) baseTimer = buildTime + Time.time;
        //else StartWave();
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
