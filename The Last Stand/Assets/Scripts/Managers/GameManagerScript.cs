using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static int currentMoney = 0;

    public static bool isPaused = false;

    public static GameManagerScript instance;

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

    public void UpdateMoney(int money)
    {
        currentMoney += money;
        UIManagerScript.instance.UpdateMoneyText(currentMoney);
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
