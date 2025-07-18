using UnityEngine;

public class WerebearMoveState : IState
{
    private Animator animator;
    private Werebear werebear;
    private Transform target;
    public WerebearMoveState(Animator animator, Werebear werebear, Transform target)
    {
        this.animator = animator;
        this.werebear = werebear;
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
        Vector3 dir = (target.position - werebear.transform.position).normalized;
        werebear.transform.position += dir * werebear.enemyData.speed * Time.deltaTime;

        werebear.transform.localScale = new Vector3(
            dir.x > 0 ? 1f : -1f,
            1f,
            1f
        );
    }

    public void SetTarget()
    {
        target = werebear.target;
    }
}
