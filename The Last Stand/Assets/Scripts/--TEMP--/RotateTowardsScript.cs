using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    private bool faceToMouse = false;
    [SerializeField]
    private Transform target = null;

    [Space]
    [SerializeField]
    private float pivotOffset = 0.19f;
    [SerializeField]
    private Transform shootPoint;

    [Header("Speed")]
    [Space]
    [SerializeField]
    private bool hasRotationSpeed = false;
    [SerializeField]
    private float rotationSpeed = 100f;

    private Vector3 faceTo;
    private bool isFacingRight = true;
    private SpriteRenderer mySpriteRenderer;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!LevelManagerScript.instance.isPaused)
        {
            Vector3 faceTo = faceToMouse == true ? Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position : target.position;

            float angle = Mathf.Atan2(faceTo.y - pivotOffset, faceTo.x) * Mathf.Rad2Deg;
            Quaternion currentRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 currentAngleRotation = currentRotation.eulerAngles;

            transform.localEulerAngles = currentAngleRotation;

            if ((Input.mousePosition.x < Camera.main.WorldToScreenPoint(transform.position).x && isFacingRight) 
                || Input.mousePosition.x > Camera.main.WorldToScreenPoint(transform.position).x && !isFacingRight)
            {
                Flip();
            }
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        mySpriteRenderer.flipY = !mySpriteRenderer.flipY;

        Vector2 shootPointPos = shootPoint.localPosition;
        shootPointPos.y *= -1;
        shootPoint.localPosition = shootPointPos;
    }
}
