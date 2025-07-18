using UnityEngine;
public class ORBKAbilityState : IState
{
    private Animator animator;

    public ORBKAbilityState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetTrigger("Ability");
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        // Ability state logic can be added here if needed
    }
}