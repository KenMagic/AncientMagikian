using UnityEngine;

public class SlimeMoveState : IState
{
    private Animator anim;
    private Slime slime;
    private Transform target;

    public SlimeMoveState(Animator anim, Slime slime, Transform target)
    {
        this.anim = anim;
        this.slime = slime;
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
        Vector3 dir = (target.position - slime.transform.position).normalized;
        slime.transform.position += dir * slime.enemyData.speed * Time.deltaTime;

        slime.transform.localScale = new Vector3(
            dir.x > 0 ? 1f : -1f,
            1f,
            1f
        );
    }

    public void SetTarget()
    {
        target = slime.target;
    }
}
