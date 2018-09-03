using UnityEngine;

public class MenuSceneryLevelSelectBehaviour : StateMachineBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    private LevelSelectMode levelSelectMode;

    private MenuManagerScript menuManager;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        menuManager = menuManager ?? GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<MenuManagerScript>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.99) menuManager.OpenLevelSelect(levelSelectMode);
    }
}
