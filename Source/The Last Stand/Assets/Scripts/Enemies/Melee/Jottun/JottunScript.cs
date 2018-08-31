using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class JottunScript : EnemyMeleeScript
{
    private BallistaScript ballistaScript;

    public void DoubleAttack()
    {
        if (ballistaScript != null) ballistaScript.TakeDamage(currentDamage);
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
            ballistaScript = ballistaScript ?? collision.GetComponent<BallistaScript>();
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