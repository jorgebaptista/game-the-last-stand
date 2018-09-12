using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    #region Variables
    [Header("HUD")]
    [Space]
    [SerializeField]
    private float lifeBarSpeed = 5f;
    [SerializeField]
    private Image lifeBarImage;

    [Space]
    [SerializeField]
    private Image[] ammoImages;

    [Space]
    [SerializeField]
    private Text moneyText;
    [Space]
    [SerializeField]
    private MoneyAnimation moneyAnimation;

    [System.Serializable]
    private struct MoneyAnimation
    {
        public GameObject gameObject;
        public Text symbol;
        public Text text;
    }

    private int moneyGathered;

    [Space]
    [SerializeField]
    private Text waveText;

    [Header("Build Mode")]
    [Space]
    [SerializeField]
    private GameObject buildModeCanvas;

    [SerializeField]
    private Text timerText;

    [Space]
    [SerializeField]
    private GameObject trapButtons;
    [SerializeField]
    private GameObject[] trapTooltips;
    [SerializeField]
    private GameObject trapBackButton;

    [Header("Pause Menu")]
    [Space]
    [SerializeField]
    private GameObject pauseCanvas;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject[] pauseExtraMenus;

    private LevelManagerScript levelManager;
    private TrapManagerScript trapManager;
    private BallistaScript ballistaScript;
    private TrapButtonScript[] trapButtonScripts;
    #endregion

    private void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
        trapManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<TrapManagerScript>();
        ballistaScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BallistaScript>();
        trapButtonScripts = FindObjectsOfType<TrapButtonScript>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (trapBackButton.activeInHierarchy) CollapseTrapMenu();
            else TogglePauseScreen();
        }
    }

    #region (HUD) - Heads Up Display
    public void UpdateLifeBar(float lifePointsPercentage)
    {
        StopCoroutine("UpdateLifeBarImage");
        StartCoroutine("UpdateLifeBarImage", lifePointsPercentage);
    }

    private IEnumerator UpdateLifeBarImage(float lifePointsPercentage)
    {
        while(lifePointsPercentage != lifeBarImage.fillAmount)
        {
            lifeBarImage.fillAmount = Mathf.MoveTowards(lifeBarImage.fillAmount, lifePointsPercentage, Time.deltaTime * lifeBarSpeed);
        }
        yield return null;
    }

    public void UpdateAmmoImages(int currentAmmo)
    {
        for (int i = 0; i < ammoImages.Length; i++) ammoImages[i].gameObject.SetActive(i < currentAmmo);
    }

    public void UpdateMoneyText(int money, int moneyGained, bool positive)
    {
        moneyText.text = money.ToString();

        if (positive)
        {
            moneyAnimation.symbol.text = "+";
            moneyAnimation.symbol.color = Color.green;
        }
        else
        {
            moneyAnimation.symbol.text = "-";
            moneyAnimation.symbol.color = Color.red;
        }

        if (moneyAnimation.gameObject.activeInHierarchy) moneyGathered += moneyGained;
        else moneyGathered = moneyGained;

        moneyAnimation.text.text = moneyGathered.ToString();
        moneyAnimation.gameObject.SetActive(true);
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }
    #endregion

    #region Build Mode UI
    public void ToggleBuildModeUI(bool toggle)
    {
        buildModeCanvas.SetActive(toggle);
        ballistaScript.ShowRepairCanvas(toggle);
    }

    public void UpdateBuildModeTimer(int time)
    {
        timerText.text = time.ToString();
    }

    public void CollapseTrapMenu(bool enabled = true)
    {
        trapBackButton.SetActive(!enabled);
        trapButtons.SetActive(enabled);

        if (enabled)
        {
            foreach (GameObject tooltip in trapTooltips) tooltip.SetActive(false);
            trapManager.HideTrapSpots();
            ballistaScript.ShowRepairCanvas();
        }
    }

    public void UpdateTrapMenu()
    {
        foreach (TrapButtonScript trapButtonScript in trapButtonScripts)
        {
            trapButtonScript.UpdateButton();
        }
    }
    #endregion

    #region Pause
    public void TogglePauseScreen()
    {
        if (!levelManager.isPaused) pauseCanvas.SetActive(true);
        else CollapsePauseScreens();
    }

    private void CollapsePauseScreens()
    {
        for (int i = 0; i < pauseExtraMenus.Length; ++i)
        {
            if (pauseExtraMenus[i].activeInHierarchy)
            {
                pauseExtraMenus[i].SetActive(false);
                pauseMenu.SetActive(true);
                return;
            }
        }
        pauseCanvas.SetActive(false);
    }
    #endregion

    #region Scene Manager Calls
    public void LoadScene(string scene)
    {
        SceneManagerScript.instance.LoadScene(scene);
    }

    public void RestartScene()
    {
        SceneManagerScript.instance.RestartScene();
    }

    public void WinGame()
    {
        SceneManagerScript.instance.LoadMenuAndCredits();
    }
    #endregion
}