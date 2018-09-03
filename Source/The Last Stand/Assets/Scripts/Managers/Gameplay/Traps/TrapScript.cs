using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    protected Transform checkAreaA;
    [SerializeField]
    protected Transform checkAreaB;
    [SerializeField]
    protected LayerMask enemyLayerMask;

    [Space]
    [SerializeField]
    private string[] trapSound;

    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        myAnimator.SetBool("Is Active", Physics2D.OverlapArea(checkAreaA.position, checkAreaB.position, enemyLayerMask));
    }

    private void PlaySound()
    {
        AudioManagerScript.instance.PlaySound(trapSound[Random.Range(0, trapSound.Length)], name);
    }
}