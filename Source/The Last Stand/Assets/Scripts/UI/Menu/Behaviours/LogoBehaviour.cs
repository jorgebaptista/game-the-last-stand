using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoBehaviour : StateMachineBehaviour
{
    private MenuManagerScript menuManager;
    private bool isDone;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isDone = false;
        menuManager = menuManager ?? GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<MenuManagerScript>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        if (!isDone && animatorStateInfo.normalizedTime > 0.99)
        {
            isDone = true;
            menuManager.OpenMenu(true);
        }
    }
}
