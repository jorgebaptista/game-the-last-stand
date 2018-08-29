using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerScript : MonoBehaviour
{
    [Header("General")]
    [Space]
    [SerializeField]
    private float buildTimer = 30f;

    [Header("General - Debug")]
    [Space]
    public int currentMoney = 0;

    [HideInInspector]
    public bool isPaused;

    [HideInInspector]
    public bool buildMode = true;
    private float baseTimer;

    private UIManagerScript uIManager;
    private WaveManagerScript waveManager;

    private void Awake()
    {
        waveManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<WaveManagerScript>();
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<UIManagerScript>();
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
            else uIManager.UpdateWaveText(Mathf.CeilToInt(baseTimer - Time.time));

            Debug.LogWarning("Unfinished Script");
        }
    }

    public void ToggleBuildMode(bool toggle)
    {
        buildMode = toggle;

        if (buildMode) baseTimer = buildTimer + Time.time;
        else waveManager.StartWave();
    }

    public void TogglePause(bool toggle)
    {
        isPaused = toggle;
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void UpdateMoney(int money)
    {
        currentMoney += money;
        uIManager.UpdateMoneyText(currentMoney);
    }

    public void GameOver()
    {
        Debug.LogWarning("Unfinished Script");
        //*******************
    }
    public void GameWin()
    {
        Debug.LogWarning("Unfinished Script");
        //*******************
    }
}