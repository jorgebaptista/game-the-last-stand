using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    private float lifePoints = 100f;

    [Space]
    [SerializeField]
    private int ammo = 4;
    [SerializeField]
    private float reloadTime = 1.5f;
    [SerializeField]
    private float shootingForce = 1000f;

    [Header("References")]
    [Space]
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private GameObject bulletPrefab;

    private bool isAlive;
    private bool isReloading;

    private float currentLifePoints;
    private float reloadCooldown;

    private int currentAmmo;
    private int bulletPoolIndex;

    private GameObject bullet;

    private void Awake()
    {
        isAlive = true;

        currentLifePoints = lifePoints;
        currentAmmo = ammo;
    }

    private void Start()
    {
        UpdateUILife();
        UpdateUIAmmo();

        bulletPoolIndex = PoolManagerScript.instance.PreCache(bulletPrefab);
    }

    private void Update()
    {
        if (isAlive && !GameManager.instance.isPaused)
        {
            if (isReloading)
            {

            }
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

    //public void TakeDamage(float damage)
    //{
    //    if (isAlive == true)
    //    {
    //        currentLifePoints -= damage;
    //        if (currentLifePoints <= 0)
    //        {
    //            currentLifePoints = 0;
    //        }
    //        UIManagerScript.instance.UpdatePlayerLifeBar(currentLifePoints / lifePoints);
    //        if (currentLifePoints == 0)
    //        {
    //            Die();
    //        }
    //    }
    //}

    //private void Die()
    //{
    //    isAlive = false;
    //    //*********************
    //}

    private void Shoot()
    {
        bullet.transform.position = shootPoint.position;
        bullet.transform.rotation = shootPoint.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Rigidbody2D>().AddForce(shootPoint.up * shootingForce);

        --currentAmmo;
        UpdateUIAmmo();

        if (currentAmmo == 0)
        {
            isReloading = true;
            reloadCooldown = Time.time + reloadTime;
        }
    }
    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = ammo;
        UpdateUIAmmo();
        isReloading = false;
    }

    private void UpdateUILife()
    {
        UIManagerScript.instance.UpdatePlayerLifeBar(currentLifePoints / lifePoints);
    }
    private void UpdateUIAmmo()
    {
        UIManagerScript.instance.UpdateAmmoImages(currentAmmo);
    }
}
