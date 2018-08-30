using UnityEngine;

public class PauseScript : MonoBehaviour
{
    private LevelManagerScript levelManagerScript;

    private void Awake()
    {
        levelManagerScript = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
    }

    private void OnEnable()
    {
        levelManagerScript.TogglePause(true);
    }
    private void OnDisable()
    {
        levelManagerScript.TogglePause(false);
    }
}
