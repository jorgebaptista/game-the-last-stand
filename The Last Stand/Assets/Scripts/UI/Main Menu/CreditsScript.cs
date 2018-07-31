using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    [SerializeField]
    private GameObject creditsScreen;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            creditsScreen.SetActive(false);
        }
    }

}
