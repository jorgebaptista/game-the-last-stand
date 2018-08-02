using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScript : MonoBehaviour
{
    private SceneManagerScript gameSceneManager;

    private void Awake()
    {
        gameSceneManager = FindObjectOfType<SceneManagerScript>();
    }
    public void PlayGame ()
    {
        gameSceneManager.LoadScene("Prototype");
    }
    public void QuitGame ()
    {
        Application.Quit();        
    }
}
