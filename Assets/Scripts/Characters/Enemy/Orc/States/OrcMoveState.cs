using UnityEngine;

public class OrcMoveState : IState
{
    private Animator anim;
    private Orc orc;
    private Transform target;

    public OrcMoveState(Animator anim, Orc orc, Transform target)
    {
        this.anim = anim;
        this.orc = orc;
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
        Vector3 dir = (target.position - orc.transform.position).normalized;
        orc.transform.position += dir * orc.enemyData.speed * Time.deltaTime;

        orc.transform.localScale = new Vector3(
            dir.x > 0 ? 1f : -1f,
            1f,
            1f
        );
    }

    public void SetTarget()
    {
        target = orc.target;
    }

    #region private methods

    #endregion
}
