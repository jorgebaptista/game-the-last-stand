using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class JottunScript : EnemyMeleeScript
{
    public void DoAttack()
    {
        if (damageable != null)
        {
            damageable.TakeDamage(currentDamage / 2);
            AudioManagerScript.instance.PlaySound(attackSound, name);
        }
        else Debug.LogError("Ballista Script not assigned on OnTriggerEnter2D");
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