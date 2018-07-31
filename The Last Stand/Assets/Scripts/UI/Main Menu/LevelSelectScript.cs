using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private Animator logoAnimator;
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject blurImage;

    public void OpenMainMenu()
    {
        logoAnimator.SetTrigger("PushIn");
        mainMenu.SetActive(true);
        blurImage.SetActive(true);
    }

}
