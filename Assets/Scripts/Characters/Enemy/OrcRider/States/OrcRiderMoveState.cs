using UnityEngine;

public class OrcRiderMoveState : IState
{
    private Animator anim;
    private OrcRider orcRider;
    private Transform target;

    public OrcRiderMoveState(Animator anim, OrcRider orcRider, Transform target)
    {
        this.anim = anim;
        this.orcRider = orcRider;
        this.target = target;
    }

    public void OnEnter()
    {
        anim.SetBool("isMoving", true);
    }

    public void OnExit()
    {
        anim.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        Vector3 dir = (target.position - orcRider.transform.position).normalized;
        orcRider.transform.position += dir * orcRider.enemyData.speed * Time.deltaTime;

        orcRider.transform.localScale = new Vector3(
            dir.x > 0 ? 1f : -1f,
            1f,
            1f
        );
    }
}
