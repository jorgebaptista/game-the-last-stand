using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class JottunScript : EnemyMeleeScript
{
    private IDamageable damageable;

    public void DoubleAttack()
    {
        if (damageable != null)
        {
            damageable.TakeDamage(currentDamage);
            damageable = null;
        }
        else Debug.LogError("Ballista Script not assigned on OnTriggerEnter2D");
    }

    public void DisableAttack()
    {
        isAttacking = false;
        baseTimer = Time.time;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            damageable = collision.GetComponent<IDamageable>();
            base.OnTriggerEnter2D(collision);
        }
    }

    #region Debug
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawLine(eyes.position, eyeSight.position);
    }
#endif
    #endregion
}