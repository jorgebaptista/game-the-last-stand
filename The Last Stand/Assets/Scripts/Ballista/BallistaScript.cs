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
    private Transform shootPoint;

    [Header("Player Properties")]
    [Space]
    private bool isAlive;
    private bool isReloading;

    private float currentLifePoints;
    private int currentAmmo;

    private Vector2 mouseDirection;
    //************************************
    [SerializeField]
    private GameObject bulletPrefab;
    private GameObject bullet;

    private int bulletPoolIndex;

    private void Start()
    {
        isAlive = true;
        currentLifePoints = lifePoints;
        currentAmmo = ammo;

        UpdateLifeBarUI();
        UpdateAmmoUI();

        //*******
        bulletPoolIndex = PoolManagerScript.instance.PreCache(bulletPrefab, 2);
    }
    private void Update()
    {
        if (isAlive && !GameManagerScript.isPaused)
        {
            if ((Input.GetButtonDown("Fire1")) && (!isReloading) && (currentAmmo > 0))
            {
                bullet = PoolManagerScript.instance.GetCachedPrefab(bulletPoolIndex);
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
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.up * shootingForce);

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
