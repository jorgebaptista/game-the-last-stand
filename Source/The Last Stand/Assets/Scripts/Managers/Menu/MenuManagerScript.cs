using UnityEngine;

public enum LevelSelectMode {Campaign, Survival}

public class MenuManagerScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    private float logoPullDelay = .05f;
    [SerializeField]
    private Animator logoAnimator;

    [Space]
    [SerializeField]
    private GameObject blurImage;

    [Space]
    private string menuMusic = "Menu_Music";

    [Header("Level Select Settings")]
    [Space]
    [SerializeField]
    private GameObject campaignLevelSelect;
    [SerializeField]
    private GameObject survivalLevelSelect;

    [Space]
    [SerializeField]
    private GameObject buttonsCampaign;
    [SerializeField]
    private GameObject levelRegionCampaign;
    [Space]
    [SerializeField]
    private GameObject buttonsSurvival;
    [SerializeField]
    private GameObject levelRegionSurvival;

    [Header("Collapse Menus")]
    [Space]
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject[] linkedMainMenus;

    [Space]
    [SerializeField]
    private GameObject extrasMenu;
    [SerializeField]
    private GameObject[] linkedExtrasMenus;

    private bool logoPulled;
    private LevelSelectMode levelSelectMode;

    private void Update()
    {
        if (!logoPulled && Time.timeSinceLevelLoad > logoPullDelay)
        {
            logoAnimator.SetTrigger("Pull");
            logoPulled = true;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (IsMenuActive(linkedExtrasMenus)) CollapseMenus(extrasMenu, linkedExtrasMenus, mainMenu);
            else CollapseMenus(mainMenu, linkedMainMenus);
        }
    }

    private void Start()
    {
        if (!AudioManagerScript.instance.CheckMusicPlaying()) AudioManagerScript.instance.PlaySound(menuMusic, name);
    }

    public void OpenMenu(bool intro = false)
    {
        mainMenu.SetActive(true);
        if (!intro)
        {
            blurImage.SetActive(true);
            logoAnimator.SetTrigger("PushIn");
        }
    }

    public void OpenLevelSelect(LevelSelectMode mode)
    {
        if (mode == LevelSelectMode.Campaign) campaignLevelSelect.SetActive(true);
        else survivalLevelSelect.SetActive(true);
    }

    public void FocusLevel(bool focus, LevelSelectMode mode)
    {
        if (mode == LevelSelectMode.Campaign)
        {
            if (focus) levelRegionCampaign.SetActive(true);
            else buttonsCampaign.SetActive(true);
        }
        else
        {
            if (focus) levelRegionSurvival.SetActive(true);
            else buttonsSurvival.SetActive(true);
        }
    }

    #region Collapse Menus
    private bool IsMenuActive(GameObject[] linkedMenus)
    {
        for (int i = 0; i < linkedMenus.Length; ++i)
        {
            if (linkedMenus[i].activeInHierarchy) return true;
        }
        return false;
    }

    private void CollapseMenus(GameObject targetMenu, GameObject[] linkedMenus, GameObject root = null)
    {
        for (int i = 0; i < linkedMenus.Length; ++i)
        {
            if (linkedMenus[i].activeInHierarchy)
            {
                linkedMenus[i].SetActive(false);
                targetMenu.SetActive(true);
                return;
            }
        }
        if (root != null)
        {
            targetMenu.SetActive(false);
            root.SetActive(true);
        }
    }
    #endregion

    #region SceneManager
    public void LoadLevel(string sceneName)
    {
        SceneManagerScript.instance.LoadScene(sceneName);
    }
    public void Quit()
    {
        SceneManagerScript.instance.QuitGame();
    }
    #endregion
}