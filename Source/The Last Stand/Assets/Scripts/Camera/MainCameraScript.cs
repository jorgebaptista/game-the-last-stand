using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    [Header("Target Aspect Ratio")]
    [Space]
    [SerializeField]
    private float widthRatio = 16.0f;
    [SerializeField]
    private float heightRatio = 9.0f;

    private void Start()
    {

        float targetAspectRatio = widthRatio / heightRatio;
        float currentAspectRatio = (float)Screen.width / Screen.height;
        
        float scaleheight = currentAspectRatio / targetAspectRatio;

        Camera thisCamera = GetComponent<Camera>();
        
        if (scaleheight < 1.0f)
        {
            Rect rect = thisCamera.rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            thisCamera.rect = rect;
        }
        else 
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = thisCamera.rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            thisCamera.rect = rect;
        }
    }
}
