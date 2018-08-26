using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    [Header("HUD")]
    [Space]
    [SerializeField]
    private float lifeBarSpeed = 1f;
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
    [SerializeField]
    private Text[] timerText;

    [Header("Pause Menu")]
    [Space]
    [SerializeField]
    private GameObject pauseCanvas;
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]
    private GameObject[] pauseExtraMenus;

    [Header("References")]
    [Space]
    [SerializeField]
    private LevelManagerScript levelManagerScript;

    private void Awake()
    {
        levelManagerScript = levelManagerScript ?? GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) TogglePauseScreen();
    }

    public void TogglePauseScreen()
    {
        if (!levelManagerScript.isPaused) pauseCanvas.SetActive(true);
        else
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
    }

    public void UpdatePlayerLifeBar(float lifePointsPercentage)
    {
        StopCoroutine("UpdatePlayerLifeBarImage");
        StartCoroutine("UpdatePlayerLifeBarImage", lifePointsPercentage);
    }
    private IEnumerator UpdatePlayerLifeBarImage(float lifePointsPercentage)
    {
        while(lifePointsPercentage != lifeBarImage.fillAmount)
        {
            lifeBarImage.fillAmount = Mathf.MoveTowards(lifeBarImage.fillAmount, lifePointsPercentage, Time.deltaTime * lifeBarSpeed);
        }
        yield return null;
    }

    public void UpdateAmmoImages(int currentAmmo)
    {
        for (int i = 0; i < ammoImages.Length; i++)
        {
            if (i < currentAmmo)
            {
                ammoImages[i].gameObject.SetActive(true);
            }
            else
            {
                ammoImages[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateMoneyText(int money)
    {
        moneyText.text = money.ToString();
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }
    public void UpdateTimer(int time)
    {
        for(int i = 0; i < timerText.Length; ++i)
        {
            timerText[i].text = time.ToString();
        }
    }
}