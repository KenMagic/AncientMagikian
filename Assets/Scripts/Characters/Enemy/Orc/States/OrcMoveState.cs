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
        Debug.Log("OrcMoveState: OnEnter");
        anim.SetBool("isMoving", true);
    }

    public void OnExit()
    {
        anim.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        Vector3 dir = (target.position - orc.transform.position).normalized;
        orc.transform.position += dir * orc.enemyData.speed * Time.deltaTime;

        orc.transform.localScale = new Vector3(
            dir.x > 0 ? 1f : -1f,
            1f,
            1f
        );
    }

    #region private methods

    #endregion
}
