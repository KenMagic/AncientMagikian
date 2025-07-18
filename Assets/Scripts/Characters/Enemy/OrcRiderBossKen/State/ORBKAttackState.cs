using UnityEngine;
public class ORBKAttackState : IState
{
    private Animator animator;

    public ORBKAttackState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetTrigger("Attack");
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        // Attack state logic can be added here if needed
    }
}