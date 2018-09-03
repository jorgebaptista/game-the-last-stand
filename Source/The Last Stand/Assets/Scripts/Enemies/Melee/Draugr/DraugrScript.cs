using System.Collections;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DraugrScript : EnemyMeleeScript
{
    [Header("Draugr Settings")]
    [Space]
    [SerializeField]
    private float pushBackForce = 200f;

    [Space]
    [SerializeField]
    private float checkRadius = 0.1f;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;

    private bool isGrounded;

    protected override void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        myAnimator.SetBool("Is Grounded", isGrounded);

        if (isGrounded)
        {
            base.FixedUpdate();
        }
    }

    protected override void Die()
    {
        StartCoroutine(WaitUntilGrounded());
    }

    private IEnumerator WaitUntilGrounded()
    {
        yield return new WaitUntil(() => isGrounded);
        base.Die();
    }

    public void DoAttack()
    {
        if (damageable != null)
        {
            damageable.TakeDamage(currentDamage);
            AudioManagerScript.instance.PlaySound(attackSound, name);
        }
        else Debug.LogError("Ballista Script not assigned on OnTriggerEnter2D");
    }

    private void PushBack()
    {
        myRigidBody2D.AddForce(new Vector2(-pushBackForce * transform.right.x, pushBackForce));
    }

    #region Debug
#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawSolidDisc(groundCheck.position, Vector3.forward, checkRadius);
        Handles.DrawLine(eyes.position, eyeSight.position);
    }
#endif
    #endregion
}