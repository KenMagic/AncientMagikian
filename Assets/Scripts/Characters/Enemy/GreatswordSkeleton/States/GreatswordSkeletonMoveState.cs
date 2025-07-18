using UnityEngine;

public class GreatswordSkeletonMoveState : IState
{
    private Animator animator;
    private GreatswordSkeleton greatswordSkeleton;
    private Transform target;
    public GreatswordSkeletonMoveState(Animator animator, GreatswordSkeleton greatswordSkeleton, Transform target)
    {
        this.animator = animator;
        this.greatswordSkeleton = greatswordSkeleton;
        this.target = target;
    }
    public void OnEnter()
    {
        animator.SetBool("isMoving", true);
        SetTarget();
    }
    public void OnExit()
    {
        animator.SetBool("isMoving", false);
        SetTarget();
    }
    public void OnUpdate()
    {
        SetTarget();
        Vector3 dir = (target.position - greatswordSkeleton.transform.position).normalized;
        greatswordSkeleton.transform.position += dir * greatswordSkeleton.enemyData.speed * Time.deltaTime;
        greatswordSkeleton.transform.localScale = new Vector3(
            dir.x > 0 ? 1f : -1f,
            1f,
            1f
        );
    }
    public void SetTarget()
    {
        target = greatswordSkeleton.target;
    }

}
