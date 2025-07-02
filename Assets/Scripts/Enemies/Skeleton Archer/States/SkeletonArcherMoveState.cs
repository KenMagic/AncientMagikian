using UnityEngine;

public class SkeletonArcherMoveState : IState
{
    private readonly Animator animator;
    private readonly SkeletonArcher skeletonArcher;
    public SkeletonArcherMoveState(Animator animator, SkeletonArcher skeletonArcher)
    {
        this.animator = animator;
        this.skeletonArcher = skeletonArcher;
    }
    public void Enter()
    {
        animator.SetBool("isMoving", true);
        //skeletonArcher.stateMachine.SetTarget(skeletonArcher.target);
    }
    public void Update()
    {
        if (skeletonArcher.IsInRange())
        {
            skeletonArcher.stateMachine.SetState(new SkeletonArcherAttackState(animator, skeletonArcher));
        }
        else
        {
            skeletonArcher.MoveTowardsTarget();
        }
    }
    public void Exit()
    {
        animator.SetBool("isMoving", false);
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