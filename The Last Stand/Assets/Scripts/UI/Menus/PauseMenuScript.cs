using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{
    private void OnEnable()
    {       
        Time.timeScale = 0f;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
