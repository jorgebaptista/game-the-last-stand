using UnityEngine;

public abstract class EnemyMeleeScript : EnemyScript
{
    [Header("Attack Settings")]
    [Space]
    [SerializeField]
    protected BoxCollider2D attackTrigger;

    public virtual void ToggleAttackTrigger(bool enabled)
    {
        attackTrigger.enabled = enabled;
    }

    protected override void ResetStats()
    {
        base.ResetStats();
        attackTrigger.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null) damageable.TakeDamage(currentDamage);
    }
}