using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagerScript : MonoBehaviour
{
    [Header("Attack Settings")]
    [Space]
    [SerializeField]
    private float damage;

    [Header("Properties")]
    private BallistaScript ballistaScript;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tower"))
        {
            if (ballistaScript == null)
            {
                ballistaScript = other.GetComponentInChildren<BallistaScript>();
            }
            ballistaScript.TakeDamage(damage);            
        }
    }
}
