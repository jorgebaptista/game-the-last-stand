using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollParentScript : MonoBehaviour
{
    [Header("Scrolling")]
    [Space]
    [SerializeField]
    protected float scrollSpeed = 0.001f;

    [Header("Direction")]
    [Space]
    [SerializeField]
    protected bool beginToRight = true;

    protected Vector2 currentPos;
    
    private void FixedUpdate()
    {
        transform.Translate(new Vector2(scrollSpeed, 0));
    }
}
