using UnityEngine;
public class KnightKenBlockState : IState
{
    private Animator animator;

    public KnightKenBlockState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        Debug.Log("KnightKenBlockState: OnEnter");
        animator.SetTrigger("Block");
        animator.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        Debug.Log("KnightKenBlockState: OnExit");
        animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
    }
}