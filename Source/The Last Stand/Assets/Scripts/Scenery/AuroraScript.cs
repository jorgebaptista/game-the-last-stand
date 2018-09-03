using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuroraScript : MonoBehaviour
{
    [SerializeField]
    private float startDelay = 0.5f;

    private Animator myAnimator;

    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        Invoke("ActivateAnimator", startDelay);
    }

    private void ActivateAnimator()
    {
        myAnimator.enabled = true;
    }
}
