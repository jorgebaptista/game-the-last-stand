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
    private Text waveText;

    [Header("Build Mode")]
    [Space]
    [SerializeField]
    private Text timerText;

    [Header("Pause Menu")]
    [Space]
    [SerializeField]
    private GameObject pauseCanvas;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject[] pauseExtraMenus;

    private LevelManagerScript levelManager;
    #endregion

    private void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) TogglePauseScreen();
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

    public void UpdateMoneyText(int money)
    {
        moneyText.text = money.ToString();
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }
    #endregion

    public void UpdateBuildModeTimer(int time)
    {
        timerText.text = time.ToString();
    }

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
}