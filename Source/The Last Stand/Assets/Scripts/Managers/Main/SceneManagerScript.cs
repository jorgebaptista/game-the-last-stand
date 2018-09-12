#if UNITY_EDITOR
    using UnityEditor;
#endif
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    [Header("LoadScreen Settings")]
    [Space]
    [SerializeField]
    private GameObject loadScreen;
    [SerializeField]
    private Image loadBar;

    [Header("Credits")]
    [Space]
    [SerializeField]
    private GameObject credits;

    public static SceneManagerScript instance;

    private AsyncOperation currentAsyncScene;

    private void Awake()
    {
        instance = instance ?? this;
    }

    #region LoadScene
    public void LoadScene(string sceneName)
    {
        loadScreen.SetActive(true);
        currentAsyncScene = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(LoadAsynchronously());
    }
    public void LoadScene(string sceneName, bool isImmediate)
    {
        loadScreen.SetActive(true);
        currentAsyncScene = SceneManager.LoadSceneAsync(sceneName);
        StartCoroutine(LoadAsynchronously(isImmediate));
    }

    private IEnumerator LoadAsynchronously(bool isImmediate = true)
    {
        AudioManagerScript.instance.StopAllSounds();
        currentAsyncScene.allowSceneActivation = false;

        while (currentAsyncScene.isDone)
        {
            float progress = currentAsyncScene.progress / .9f;
            loadBar.fillAmount = progress;
            yield return null;
        }

        if (!isImmediate) loadScreen.SetActive(false);
        else
        {
            currentAsyncScene.allowSceneActivation = true;
            yield return new WaitUntil(() => currentAsyncScene.isDone);
            loadScreen.SetActive(false);
        }
    }

    public bool GetAsyncSceneState()
    {
        return currentAsyncScene.progress >= 0.9f;
    }

    public void ActivateAsyncScene()
    {
        currentAsyncScene.allowSceneActivation = true;
    }
    #endregion

    public void RestartScene()
    {
        AudioManagerScript.instance.StopAllSounds();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        AudioManagerScript.instance.StopAllSounds();
        Application.Quit();

        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #endif
    }

    public void LoadMenuAndCredits()
    {
        credits.SetActive(true);
        AudioManagerScript.instance.StopAllSounds();
        SceneManager.LoadScene("Menu");
    }
}
