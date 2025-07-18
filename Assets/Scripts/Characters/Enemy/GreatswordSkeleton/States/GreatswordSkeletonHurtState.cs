using UnityEngine;

public class GreatswordSkeletonHurtState : IState
{
    private Animator animator;
    private GreatswordSkeleton greatswordSkeleton;
    public GreatswordSkeletonHurtState(Animator animator, GreatswordSkeleton greatswordSkeleton)
    {
        this.animator = animator;
        this.greatswordSkeleton = greatswordSkeleton;
    }
    public void OnEnter()
    {
        if (greatswordSkeleton.enemyData.health <= 0)
        {
            animator.SetTrigger("isDeath");
            greatswordSkeleton.HideAfterDelay(1f);
        }
        else
        {
            animator.SetTrigger("isHurt");
        }
    }
    public void OnExit()
    {
    }
    public void OnUpdate()
    {
    }

}
