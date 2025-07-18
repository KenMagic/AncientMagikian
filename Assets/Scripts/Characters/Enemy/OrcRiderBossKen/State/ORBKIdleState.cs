using UnityEngine;
public class ORBKIdleState : IState
{
    private Animator animator;

    public ORBKIdleState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        
    }
}