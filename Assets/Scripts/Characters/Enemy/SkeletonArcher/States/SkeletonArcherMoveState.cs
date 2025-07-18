using UnityEngine;

public class SkeletonArcherMoveState : IState
{
    private Animator anim;
    private SkeletonArcher skeletonArcher;
    private Transform target;
    public SkeletonArcherMoveState(Animator anim, SkeletonArcher skeletonArcher, Transform target)
    {
        this.anim = anim;
        this.skeletonArcher = skeletonArcher;
        this.target = target;
    }

    public void OnEnter()
    {
        anim.SetBool("isMoving", true);
        SetTarget();
    }

    public void OnExit()
    {
        anim.SetBool("isMoving", false);
        SetTarget();
    }

    public void OnUpdate()
    {
        SetTarget();
        Vector3 dir = (target.position - skeletonArcher.transform.position).normalized;
        skeletonArcher.transform.position += dir * skeletonArcher.enemyData.speed * Time.deltaTime;

        skeletonArcher.transform.localScale = new Vector3(
            dir.x > 0 ? 1f : -1f,
            1f,
            1f
        );
    }

    public void SetTarget()
    {
        target = skeletonArcher.target;
    }
}
