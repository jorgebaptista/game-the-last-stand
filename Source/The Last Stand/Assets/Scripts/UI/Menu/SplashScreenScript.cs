using UnityEngine;

public class SplashScreenScript : MonoBehaviour
{
    private void Start()
    {
        SceneManagerScript.instance.LoadScene("Menu", false);
    }

    private void Update()
    {
        if (SceneManagerScript.instance.GetAsyncSceneState() && Input.anyKeyDown)
        {
            SceneManagerScript.instance.ActivateAsyncScene();
        }
    }
}
