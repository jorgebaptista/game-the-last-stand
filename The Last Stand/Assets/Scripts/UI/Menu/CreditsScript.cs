using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            gameObject.SetActive(false);
        }
    }
}