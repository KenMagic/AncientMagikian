using UnityEngine;

public class OrcBossKenIdleState : IState
{
    private Animator animator;

    public OrcBossKenIdleState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        animator.SetBool("isMoving", true);
    }

    public void OnUpdate()
    {
        // Implement idle logic here
    }
}
