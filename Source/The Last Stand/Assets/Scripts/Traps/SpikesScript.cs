using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpikesScript : TrapScript
{
    [Header("Spikes Settings")]
    [Space]
    [SerializeField]
    private float damage = 10f;

    private Collider2D[] enemyColliders;

    private void Attack()
    {
        enemyColliders = Physics2D.OverlapAreaAll(checkAreaA.position, checkAreaB.position, enemyLayerMask);

        foreach (Collider2D enemyCollider in enemyColliders)
        {
            enemyCollider.GetComponent<EnemyScript>().TakeDamage(damage);
        }
    }

    #region Debug
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawLine(checkAreaA.position, checkAreaB.position);
    }
#endif
    #endregion
}