using UnityEngine;

public class MenuSceneryLevelBehaviour : StateMachineBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    [Tooltip("Will level be focused?")]
    private bool focusLevel;
    [SerializeField]
    private LevelSelectMode levelSelectMode;

    private MenuManagerScript menuManager;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        menuManager = menuManager ?? GameObject.FindGameObjectWithTag("GameController").GetComponentInChildren<MenuManagerScript>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime > 0.99f) menuManager.FocusLevel(focusLevel, levelSelectMode);
    }
}
