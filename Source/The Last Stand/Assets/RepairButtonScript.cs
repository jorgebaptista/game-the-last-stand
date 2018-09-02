using UnityEngine;
using UnityEngine.UI;

public class RepairButtonScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    private Image buttonImage;

    [Space]
    [SerializeField]
    private Sprite activeSprite;
    [SerializeField]
    private Sprite inactiveSprite;

    [Space]
    [SerializeField]
    private Text priceText;

    private int price;

    private LevelManagerScript levelManager;
    private BallistaScript ballistaScript;

    private void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
        ballistaScript = GetComponentInParent<BallistaScript>();

        price = ballistaScript.repairPrice;
    }

    private void OnEnable()
    {
        UpdateButton();
    }

    public void UpdateButton()
    {
        buttonImage.sprite = levelManager.currentMoney >= price ? activeSprite : inactiveSprite;
        priceText.color = levelManager.currentMoney >= price ? Color.white : Color.red;
    }
}