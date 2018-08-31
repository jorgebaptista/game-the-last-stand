using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaBoltScript : ProjectileScript
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) collision.GetComponent<EnemyScript>().TakeDamage(currentDamage);

        base.OnTriggerEnter2D(collision);
    }
}
