using UnityEngine;

public class SkeletonArcherAttackState : IState
{
    private readonly Animator animator;
    private readonly SkeletonArcher skeletonArcher;
    public SkeletonArcherAttackState(Animator animator, SkeletonArcher skeletonArcher)
    {
        this.animator = animator;
        this.skeletonArcher = skeletonArcher;
    }
    public void Enter()
    {
        animator.SetBool("isAttacking", true);
        //skeletonArcher.stateMachine.SetTarget(skeletonArcher.target);
    }
    public void Update()
    {
        if (!skeletonArcher.IsInRange())
        {
            skeletonArcher.stateMachine.SetState(new SkeletonArcherMoveState(animator, skeletonArcher));
        }
    }
    public void Exit()
    {
        animator.SetBool("isAttacking", false);
    }

    public void OnEnter()
    {
        throw new System.NotImplementedException();
    }

    public void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}