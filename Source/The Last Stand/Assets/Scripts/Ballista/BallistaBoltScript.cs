using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaBoltScript : ProjectileScript
{
    protected override void FixedUpdate()
    {
        Vector3 myCameraPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (myCameraPosition.x < -0.1f || myCameraPosition.x > 1.1f) Dismiss();

        base.FixedUpdate();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActive && collision.CompareTag("Enemy")) collision.GetComponent<EnemyScript>().TakeDamage(currentDamage);

        base.OnTriggerEnter2D(collision);
    }
}
