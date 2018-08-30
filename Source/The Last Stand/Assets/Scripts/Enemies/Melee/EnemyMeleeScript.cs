using UnityEngine;

public abstract class EnemyMeleeScript : EnemyScript
{
    [Header("Attack Settings")]
    [Space]
    [SerializeField]
    private BoxCollider2D attackTrigger;

    protected override void ResetStats()
    {
        base.ResetStats();
        attackTrigger.enabled = false;
    }

    protected override void Attack()
    {
        isAttacking = true;
        myAnimator.SetTrigger("Attack");
    }

    public virtual void ToggleAttackTrigger(bool enabled)
    {
        attackTrigger.enabled = enabled;

        Debug.LogWarning("Unfinished Script.");
        //if (!enabled)
        //{
        //    isAttacking = false;
        //    baseTimer = Time.time;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null) damageable.TakeDamage(currentDamage); 
    }
}