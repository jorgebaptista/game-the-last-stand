using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [Header("Player Projectile Settings")]
    [Space]
    [SerializeField]
    private float damage = 50f;
    
    private void OnBecameInvisible()
    {
        Dismiss();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyScript>().TakeDamage(damage);
        }

        Dismiss();
    }

    private void Dismiss()
    {
        gameObject.SetActive(false);
    }
}
