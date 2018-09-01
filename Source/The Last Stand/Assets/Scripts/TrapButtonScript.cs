using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrapButtonScript : MonoBehaviour
{
    [Header("Trap Settings")]
    [Space]
    [SerializeField]
    private int price = 500;
    [SerializeField]
    private Text priceText;

    [Space]
    [SerializeField]
    private TrapType trapType;

    [Space]
    [SerializeField]
    private Sprite activeSprite;
    [SerializeField]
    private Sprite inactiveSprite;

    [Space]
    [SerializeField]
    private Image myImage;

    private LevelManagerScript levelManager;
    private UIManagerScript uIManager;
    private TrapManagerScript trapManager;

    private void Awake()
    {
        trapManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<TrapManagerScript>();
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<UIManagerScript>();

        priceText.text = price.ToString();
    }

    private void OnEnable()
    {
        if (levelManager.currentMoney >= price) myImage.sprite = activeSprite;
        else myImage.sprite = inactiveSprite;
    }

    public void HighlightTraps()
    {
        if(levelManager.currentMoney >= price)
        {
            uIManager.CollapseTrapMenu(false);
            trapManager.HighlightSpots(trapType);
        }
    }
}