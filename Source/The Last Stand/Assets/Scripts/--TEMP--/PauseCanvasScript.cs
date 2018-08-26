using UnityEngine;

public class PauseCanvasScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private LevelManagerScript levelManagerScript;

    private void Awake()
    {
        levelManagerScript = levelManagerScript ?? GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
    }

    private void OnEnable()
    {
        levelManagerScript.isPaused = true;
    }
    private void OnDisable()
    {
        levelManagerScript.isPaused = false;
    }
}
