using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallistaScript : MonoBehaviour, IDamageable
{
    #region Variables
    [Header("Settings")]
    [Space]
    [SerializeField]
    private float life = 100f;
    [SerializeField]
    private float maxDamage = 100f;
    [SerializeField]
    private float minDamage = 20f;

    [Space]
    [SerializeField]
    private int ammo = 4;
    [SerializeField]
    private float fireRate = 0.25f;
    [SerializeField]
    private float reloadTimer = 2f;
    [SerializeField]
    private float maxShootForce = 1200f;
    [SerializeField]
    private float minShootForce = 500f;

    [Space]
    [SerializeField]
    private bool instantRotation = false;
    [SerializeField]
    private float rotationSpeed = 100f;

    [Header("Attack Settings")]
    [Space]
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private GameObject boltPrefab;

    [Header("Repair Settings")]
    [Space]
    [SerializeField]
    [Range(0, 1)]
    private float repairPercent = .25f;
    public int repairPrice = 200;

    [Space]
    [SerializeField]
    private GameObject repairCanvas;
    [SerializeField]
    private RepairButtonScript repairButton;

    [Header("Reload Bar")]
    [Space]
    [SerializeField]
    private Image reloadBarImage;
    [SerializeField]
    private GameObject reloadBarCanvas;

    [Header("Sounds")]
    [Space]
    [SerializeField]
    private string attackSound = "Ballista_Attack";
    [SerializeField]
    private string reloadSound = "Ballista_Reload";

    [Header("References")]
    [Space]
    [SerializeField]
    private Transform ballistaHead;

    private int currentAmmo, boltPoolID;

    private float currentLife;
    private float currentDamage;
    private float currentShootForce;

    private float pivotOffset = 0.31f;

    private bool isAlive, canShoot, isReloading, isCharging;
    private bool isFacingRight = true;

    private SpriteRenderer mySpriteRenderer;
    private Animator myAnimator;

    private LevelManagerScript levelManager;
    private UIManagerScript uIManager;
    private PoolManagerScript poolManager;
    private WaveManagerScript waveManager;
    #endregion

    private void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        levelManager = gameController.GetComponentInChildren<LevelManagerScript>();
        uIManager = gameController.GetComponentInChildren<UIManagerScript>();
        poolManager = gameController.GetComponentInChildren<PoolManagerScript>();
        waveManager = gameController.GetComponentInChildren<WaveManagerScript>();

        mySpriteRenderer = ballistaHead.GetComponent<SpriteRenderer>();
        myAnimator = ballistaHead.GetComponent<Animator>();

        isAlive = true;

        currentLife = life;
        currentAmmo = ammo;
        boltPoolID = poolManager.PreCache(boltPrefab, 2);

        canShoot = true;
        
        UpdateUI();
    }

    private void Update()
    {
        if (isAlive && !levelManager.buildMode && !levelManager.isPaused && waveManager.waveActive)
        {
            if (canShoot && !isReloading)
            {
                if (Input.GetButton("Shoot"))
                {
                    myAnimator.SetBool("Is Charging", true);
                    isCharging = true;
                }
                if (Input.GetButtonUp("Shoot"))
                {
                    myAnimator.SetBool("Is Charging", false);
                    isCharging = false;
                }

                if (Input.GetButtonDown("Reload") && currentAmmo < ammo && !isCharging) StartCoroutine(Reload());
            }

            #region Rotation
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ballistaHead.position;

            float angle = Mathf.Atan2(mousePosition.y - pivotOffset, mousePosition.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            ballistaHead.rotation = !instantRotation ? Quaternion.Slerp(ballistaHead.rotation, targetRotation, rotationSpeed * Time.deltaTime) : targetRotation;

            if ((Input.mousePosition.x < Camera.main.WorldToScreenPoint(transform.position).x && isFacingRight)
                || Input.mousePosition.x > Camera.main.WorldToScreenPoint(transform.position).x && !isFacingRight)
            {
                Flip();
            }
            #endregion
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        mySpriteRenderer.flipY = !mySpriteRenderer.flipY;

        Vector2 shootPointPosition = shootPoint.localPosition;
        shootPointPosition.y *= -1;
        shootPoint.localPosition = shootPointPosition;
    }

    #region Attack
    public void SetShootStats(float chargePercent)
    {
        currentDamage = maxDamage * chargePercent > minDamage ? maxDamage * chargePercent : minDamage;
        currentShootForce = maxShootForce * chargePercent > minShootForce ? maxShootForce * chargePercent : minShootForce;
    }

    public void Shoot()
    {
        canShoot = false;

        GameObject bolt = poolManager.GetCachedPrefab(boltPoolID);

        bolt.GetComponent<ProjectileScript>().ResetStats(currentDamage);
        bolt.transform.position = shootPoint.position;
        bolt.transform.rotation = shootPoint.rotation;
        bolt.SetActive(true);
        bolt.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * currentShootForce);

        AudioManagerScript.instance.PlaySound(attackSound, name);

        --currentAmmo;

        if (currentAmmo == 0) StartCoroutine(Reload());
        else Invoke("ToggleCanShoot", fireRate);

        UpdateUI();
    }

    private void ToggleCanShoot()
    {
        canShoot = true;
        myAnimator.SetTrigger("Ready");
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        float timeToReload = reloadTimer - (reloadTimer * ((float)currentAmmo / ammo));
        float timer = Time.time + timeToReload;
        reloadBarCanvas.SetActive(true);

        while (timer > Time.time)
        {
            reloadBarImage.fillAmount = Mathf.Clamp01((timer - Time.time) / timeToReload);
            yield return null;
        }

        AudioManagerScript.instance.PlaySound(reloadSound, name);

        reloadBarCanvas.SetActive(false);
        currentAmmo = ammo;
        UpdateUI();
        isReloading = false;
        canShoot = true;
        myAnimator.SetTrigger("Ready");
    }
    #endregion

    #region Life
    public void TakeDamage(float damage)
    {
        if (isAlive == true)
        {
            currentLife -= damage;
            if (currentLife <= 0)
            {
                currentLife = 0;
                Die();
            }

            UpdateUI();
        }
    }

    private void Die()
    {
        isAlive = false;
        levelManager.GameOver();
    }

    private void UpdateUI()
    {
        uIManager.UpdateLifeBar(currentLife / life);
        uIManager.UpdateAmmoImages(currentAmmo);
    }
    #endregion

    #region Repair
    public void ShowRepairCanvas(bool show = true)
    {
        if (show && currentLife < life) repairCanvas.SetActive(true);
        else repairCanvas.SetActive(false);
    }

    public void Repair()
    {
        if (levelManager.currentMoney >= repairPrice)
        {
            levelManager.UpdateMoney(-repairPrice);
            repairButton.UpdateButton();

            currentLife += (life * repairPercent);
            if (currentLife > life) currentLife = life;
            UpdateUI();
            ShowRepairCanvas();
            uIManager.UpdateTrapMenu();
        }
    }
    #endregion
}