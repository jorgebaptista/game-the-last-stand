using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogoBehaviour : StateMachineBehaviour
{
    private SplashScreenScript splashScreenScript;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (splashScreenScript == null)
        {
            splashScreenScript = FindObjectOfType<SplashScreenScript>();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        splashScreenScript.OpenMainMenu();
    }
}
