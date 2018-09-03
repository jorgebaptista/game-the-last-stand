using UnityEngine;

public class SplashScreenScript : MonoBehaviour
{
    [SerializeField]
    private string menuMusic = "Menu_Music";

    private void Start()
    {
        SceneManagerScript.instance.LoadScene("Menu", false);

        AudioManagerScript.instance.PlaySound(menuMusic, name);
    }

    private void Update()
    {
        if (SceneManagerScript.instance.GetAsyncSceneState() && Input.anyKeyDown)
        {
            SceneManagerScript.instance.ActivateAsyncScene();
        }
    }
}
