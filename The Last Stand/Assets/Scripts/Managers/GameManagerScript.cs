using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    private int currentMoney = 0;

    public static GameManagerScript instance;

    //DEBUG
    public bool gameIsOn = false;

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
    public int GetMoney()
    {
        return currentMoney;
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
