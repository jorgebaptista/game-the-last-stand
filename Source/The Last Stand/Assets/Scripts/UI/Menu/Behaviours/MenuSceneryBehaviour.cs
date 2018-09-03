using UnityEngine;

public class MenuSceneryBehaviour : StateMachineBehaviour
{
    private MenuManagerScript menuManager;
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        menuManager = menuManager ?? GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<MenuManagerScript>();
        menuManager.OpenMenu();
    }
}
