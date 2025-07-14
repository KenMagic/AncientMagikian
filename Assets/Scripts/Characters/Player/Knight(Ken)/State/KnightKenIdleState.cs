using UnityEngine;
public class KnightKenIdleState : IState
{
    private Animator animator;

    public KnightKenIdleState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        Debug.Log("KnightKenIdleState: OnEnter");
        animator.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        animator.SetBool("isMoving", true);
    }

    public void OnUpdate()
    {
        // Logic for idle state can be added here
    }
}