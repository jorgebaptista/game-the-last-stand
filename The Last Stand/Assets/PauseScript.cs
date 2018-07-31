using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0f;
        GameManagerScript.isPaused = true;
    }
    private void OnDisable()
    {
        Time.timeScale = 1f;
        GameManagerScript.isPaused = false;
    }
}
