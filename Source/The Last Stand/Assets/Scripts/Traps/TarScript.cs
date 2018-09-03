using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class TarScript : TrapScript
{
    [Header("Tar Settings")]
    [Space]
    [Range(0, 1)]
    [SerializeField]
    private float slowPercentage = 0.5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyScript>().SlowDown(true, slowPercentage);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyScript>().SlowDown(false);
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