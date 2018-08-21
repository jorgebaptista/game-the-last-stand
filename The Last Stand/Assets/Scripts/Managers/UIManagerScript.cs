using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerScript : MonoBehaviour
{
    [Header("HUD - Lifebar")]
    [Space]
    [SerializeField]
    private Image lifeBarImage;
    [SerializeField]
    private float lifeBarSpeed = 1f;

    [Header("HUD - Ammo")]
    [Space]
    [Space]
    [SerializeField]
    private Image[] ammoImages;

    [Header("HUD - Wave")]
    [Space]
    [Space]
    [SerializeField]
    private Text waveText;
    [SerializeField]
    private Text[] timerText;

    [Header("HUD - Money")]
    [Space]
    [SerializeField]
    private Text moneyText;

    [Header("Pause Menu")]
    [Space]
    [SerializeField]
    private GameObject pauseCanvas;

    public static UIManagerScript instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (!LevelManagerScript.instance.isPaused)
            {
                pauseCanvas.SetActive(true);
            }
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