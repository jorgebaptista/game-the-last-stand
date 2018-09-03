using UnityEngine;

public class TrapSpotScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    public TrapMode trapMode;
    [Space]
    [SerializeField]
    private float hoverTransparency = 0.5f;

    [Space]
    [SerializeField]
    private Transform trapSpot;

    [Space]
    [SerializeField]
    private string trapPlaceSound = "Trap_Place";

    private int currentPrice;

    [HideInInspector]
    public bool isEmpty, isCraftable;

    private bool isCurrentCraftable;

    [HideInInspector]
    public TrapType trapType;
    private TrapType selectedTrap;

    private Sprite currentHotspotSprite;

    private GameObject canvas;
    private GameObject currentTrapPrefab;

    private SpriteRenderer mySpriteRenderer;

    private LevelManagerScript levelManager;
    private UIManagerScript uIManager;

    private void Awake()
    {
        levelManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<LevelManagerScript>();
        uIManager = GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<UIManagerScript>();

        mySpriteRenderer = GetComponent<SpriteRenderer>();
        canvas = GetComponentInChildren<Canvas>().gameObject;

        isEmpty = true;
    }

    public void HighLight(TrapType trap, Sprite hotspotSprite, GameObject prefab, int price, bool craftable)
    {
        canvas.SetActive(true);

        selectedTrap = trap;
        currentHotspotSprite = hotspotSprite;

        mySpriteRenderer.sprite = currentHotspotSprite;
        mySpriteRenderer.color = new Color(1, 1, 1, hoverTransparency);

        currentTrapPrefab = prefab;

        currentPrice = price;
        isCurrentCraftable = craftable;
    }

    public void HideSpotLight()
    {
        canvas.SetActive(false);
        mySpriteRenderer.sprite = null;
    }

    public void OnHover(bool hovering)
    {
        mySpriteRenderer.color = hovering ? Color.white : new Color(1, 1, 1, hoverTransparency);

        if (!isEmpty && isCraftable)
        {
            trapSpot.GetChild(0).gameObject.SetActive(!hovering);
        }
    }

    public void BuildTrap()
    {
        if (isEmpty) isEmpty = false;
        else
        {
            Destroy(trapSpot.GetChild(0).gameObject);
        }

        trapType = selectedTrap;

        isCraftable = isCurrentCraftable;

        GameObject trap = Instantiate(currentTrapPrefab, trapSpot);
        trap.transform.position = transform.position;

        levelManager.UpdateMoney(-currentPrice);

        AudioManagerScript.instance.PlaySound(trapPlaceSound, name);

        uIManager.CollapseTrapMenu();
    }
}