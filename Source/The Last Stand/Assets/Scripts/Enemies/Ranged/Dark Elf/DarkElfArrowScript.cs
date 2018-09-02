using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkElfArrowScript : ProjectileScript
{
    private void OnBecameInvisible()
    {
        Dismiss();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Palisade")) collision.GetComponent<IDamageable>().TakeDamage(currentDamage);

        base.OnTriggerEnter2D(collision);
    }
}