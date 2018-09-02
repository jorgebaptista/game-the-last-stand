using UnityEngine;

public abstract class EnemyMeleeScript : EnemyScript
{
    [Header("Attack Settings")]
    [Space]
    [SerializeField]
    protected BoxCollider2D attackTrigger;

    protected IDamageable damageable;

    public void ToggleAttackTrigger(bool enabled)
    {
        attackTrigger.enabled = enabled;
        if (!enabled)
        {
            isAttacking = false;
            baseTimer = Time.time;
        }
    }

    protected override void ResetStats()
    {
        base.ResetStats();
        attackTrigger.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Palisade"))
        {
            damageable = collision.GetComponent<IDamageable>();
        }
    }
}