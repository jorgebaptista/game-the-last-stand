using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaScript : MonoBehaviour
{
    [Header("Player Settings")]
    [Space]
    [SerializeField]
    private float lifePoints = 100f;

    [Header("Attack Settings")]
    [Space]
    [SerializeField]
    private int ammo = 4;
    [SerializeField]
    private float reloadTime = 1.5f;
    [SerializeField]
    private float shootingForce = 1000f;
    [SerializeField]
    private Transform shootingPoint;

    [Header("Player Properties")]
    [Space]
    private bool isAlive;
    private bool isReloading;

    private float currentLifePoints;
    private int currentAmmo;

    private Vector2 mouseDirection;
    private GameObject bullet;

    [Header("Other")]
    [Space]
    private PoolManager poolManager;

    private void Start()
    {
        poolManager = FindObjectOfType<PoolManager>();

        isAlive = true;
        currentLifePoints = lifePoints;
        currentAmmo = ammo;

        UpdateLifeBarUI();
        UpdateAmmoUI();
    }
    private void Update()
    {
        if (isAlive && !GameManagerScript.isPaused)
        {
            if ((Input.GetButtonDown("Fire1")) && (!isReloading) && (currentAmmo > 0))
            {
                bullet = poolManager.GetAvaiableBullet();
                if (bullet != null)
                {
                    Shoot();
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isAlive == true)
        {
            currentLifePoints -= damage;
            if (currentLifePoints <= 0)
            {
                currentLifePoints = 0;
            }
            UpdateLifeBarUI();
            if (currentLifePoints == 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        isAlive = false;
        //*********************
    }
    private void UpdateLifeBarUI()
    {
        float lifePointsFactor = currentLifePoints / lifePoints;
        UIManagerScript.instance.UpdatePlayerLifeBar(lifePointsFactor);
    }

    private void Shoot()
    {
        bullet.transform.position = shootingPoint.position;
        bullet.transform.rotation = shootingPoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(shootingPoint.up * shootingForce);

        currentAmmo--;
        UpdateAmmoUI();

        if (currentAmmo == 0)
        {
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammo;
        UpdateAmmoUI();
        isReloading = false;
    }
    private void UpdateAmmoUI()
    {
        UIManagerScript.instance.UpdateAmmoImages(currentAmmo);
    }
}
