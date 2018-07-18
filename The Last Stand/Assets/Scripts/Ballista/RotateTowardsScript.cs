using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    private bool hasRotationSpeed = false;
    [SerializeField]
    private float rotationSpeed = 100f;

    [Space]
    [SerializeField]
    private float pivotOffset = 0.29f;

    [Header("Limits")]
    [Space]
    [SerializeField]
    private bool hasRotationLimit = false;

    [Space]
    [SerializeField]
    private float maxRotationLeft = 220f;
    [SerializeField]
    private float maxRotationRight = 320f;


    [Header("Rotation Properties")]
    private Vector2 faceTo;
    private Vector3 vector_difference;

    private void Update()
    {
        if (Time.timeScale == 1 && GameManagerScript.instance.gameIsOn == true)        
        {
            faceTo = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float angle = Mathf.Atan2(faceTo.y - pivotOffset, faceTo.x) * Mathf.Rad2Deg;
            Quaternion currentRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 currentAngleRotation = currentRotation.eulerAngles;

            transform.localEulerAngles = currentAngleRotation;


            //transform.eulerAngles = new Vector3(0, 0, 200f);
        }
    }
}
