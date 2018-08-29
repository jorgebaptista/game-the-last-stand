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

    public override void ToggleAttackTrigger(bool enabled)
    {
        base.ToggleAttackTrigger(enabled);
        if (!enabled) PushBack();
    }

    public void DisableIsAttacking()
    {
        isAttacking = false;
    }

    public void PushBack()
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