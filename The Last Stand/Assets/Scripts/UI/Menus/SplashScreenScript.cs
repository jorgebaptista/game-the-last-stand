using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreenScript : MonoBehaviour
{
    [SerializeField]
    private GameObject initialInputText;

    [SerializeField]
    private GameObject gameLogo;

    [SerializeField]
    private GameObject mainMenu;

    private bool hasClicked = false;

    private void Update()
    {
        if (!hasClicked)
        {
            if (Input.anyKeyDown)
            {
                OpenSplashScreen();
            }
        }
    }

    private void OpenSplashScreen()
    {
        hasClicked = true;

        gameLogo.GetComponent<Animator>().SetTrigger("Pull");
        initialInputText.SetActive(false);
    }

    public void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        gameObject.SetActive(false);

        //blurImage.material.SetFloat("_Size", 0);
    }
}
