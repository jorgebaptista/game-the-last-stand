using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapButtonScript : MonoBehaviour
{
    [Header("Trap Settings")]
    [Space]
    [SerializeField]
    private TrapType trapType;
    
    [Space]
    [SerializeField]
    private Text priceText;
    [SerializeField]
    private Text highlightPriceText;

    [Space]
    [SerializeField]
    private Sprite activeSprite;
    [SerializeField]
    private Sprite inactiveSprite;

    [Space]
    [SerializeField]
    private Image myImage;

    private int price;

    private LevelManagerScript levelManager;
    private UIManagerScript uIManager;
    private TrapManagerScript trapManager;
    private BallistaScript ballistaScript;

    private void Awake()
    {
        trapManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<TrapManagerScript>();
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<UIManagerScript>();
        ballistaScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BallistaScript>();

        price = trapManager.GetPrice(trapType);

        priceText.text = price.ToString();
    }

    private void Start()
    {
        Invoke("UpdateButton", 0.01f);
    }

    private void OnEnable()
    {
        UpdateButton();
    }

    public void UpdateButton()
    {
        myImage.sprite = levelManager.currentMoney >= price ? activeSprite : inactiveSprite;
        priceText.color = levelManager.currentMoney >= price ? Color.white : Color.red;
    }

    public void HighlightTraps()
    {
        if(levelManager.currentMoney >= price)
        {
            highlightPriceText.text = price.ToString();
            uIManager.CollapseTrapMenu(false);
            trapManager.HighlightSpots(trapType);

            ballistaScript.ShowRepairCanvas(false);
        }
    }
}