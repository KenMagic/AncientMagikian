using UnityEngine;
public class KnightKenAttackState : IState
{
    private Animator animator;


    public KnightKenAttackState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        Debug.Log("KnightKenAttackState: OnEnter");
        animator.SetTrigger("Attack");
        animator.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        Debug.Log("KnightKenAttackState: OnExit");
        animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}