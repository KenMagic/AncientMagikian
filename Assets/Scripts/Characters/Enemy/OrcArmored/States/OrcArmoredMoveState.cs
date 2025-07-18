using UnityEngine;

public class OrcArmoredMoveState : IState
{
    private Animator anim;
    private OrcArmored orcArmored;
    private Transform target;

    public OrcArmoredMoveState(Animator anim, OrcArmored orcArmored, Transform target)
    {
        this.anim = anim;
        this.orcArmored = orcArmored;
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
        Vector3 dir = (target.position - orcArmored.transform.position).normalized;
        orcArmored.transform.position += dir * orcArmored.enemyData.speed * Time.deltaTime;

        orcArmored.transform.localScale = new Vector3(
            dir.x > 0 ? 1f : -1f,
            1f,
            1f
        );
    }

    public void SetTarget()
    {
        target = orcArmored.target;
    }

}
