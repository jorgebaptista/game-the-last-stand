using System.Collections;
using UnityEngine;

public class BallistaScript : MonoBehaviour, IDamageable
{
    #region Variables
    [Header("Settings")]
    [Space]
    [SerializeField]
    private float life = 100f;
    [SerializeField]
    private float damage = 50f;

    [Space]
    [SerializeField]
    private int ammo = 4;
    [SerializeField]
    private float fireRate = 0.5f;
    [SerializeField]
    private float reloadTimer = 2f;
    [SerializeField]
    private float shootForce = 800f;

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

    [Header("References")]
    [Space]
    [SerializeField]
    private Transform ballistaHead;

    private int currentAmmo, boltPoolID;

    private float currentLife;
    private float pivotOffset = 0.31f;
    private float baseTimer;

    private bool isAlive, isReloading;
    private bool isFacingRight = true;

    private SpriteRenderer mySpriteRenderer;

    private LevelManagerScript levelManager;
    private UIManagerScript uIManager;
    private PoolManagerScript poolManager;
    #endregion

    private void Awake()
    {
        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        levelManager = gameController.GetComponentInChildren<LevelManagerScript>();
        uIManager = gameController.GetComponentInChildren<UIManagerScript>();
        poolManager = gameController.GetComponentInChildren<PoolManagerScript>();

        mySpriteRenderer = ballistaHead.GetComponent<SpriteRenderer>();

        isAlive = true;

        currentLife = life;
        currentAmmo = ammo;
        boltPoolID = poolManager.PreCache(boltPrefab);
        
        UpdateUI();
    }

    private void Update()
    {
        if (isAlive && !levelManager.buildMode && !levelManager.isPaused)
        {
            if ((Input.GetButtonDown("Shoot")) && Time.time > baseTimer + fireRate && !isReloading) Shoot();

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ballistaHead.position;

            float angle = Mathf.Atan2(mousePosition.y - pivotOffset, mousePosition.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            ballistaHead.rotation = instantRotation ? Quaternion.Slerp(ballistaHead.rotation, targetRotation, rotationSpeed * Time.deltaTime) : targetRotation;

            if ((Input.mousePosition.x < Camera.main.WorldToScreenPoint(transform.position).x && isFacingRight)
                || Input.mousePosition.x > Camera.main.WorldToScreenPoint(transform.position).x && !isFacingRight)
            {
                Flip();
            }
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
    private void Shoot()
    {
        baseTimer = Time.time;
        GameObject bolt = poolManager.GetCachedPrefab(boltPoolID);

        bolt.GetComponent<ProjectileScript>().ResetStats(damage);
        bolt.transform.position = shootPoint.position;
        bolt.transform.rotation = shootPoint.rotation;
        bolt.SetActive(true);
        bolt.GetComponent<Rigidbody2D>().AddForce(shootPoint.right * shootForce);

        --currentAmmo;

        if (currentAmmo == 0)
        {
            StartCoroutine(Reload());
        }
        UpdateUI();
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTimer);
        currentAmmo = ammo;
        UpdateUI();
        isReloading = false;
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
}