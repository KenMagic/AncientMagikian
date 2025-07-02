using UnityEngine;

public class OrcMoveState : IState
{
    private Animator anim;
    private Orc orc;
    public OrcMoveState(Animator anim, Orc orc)
    {
        this.anim = anim;
        this.orc = orc;
    }
    public void OnEnter()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isMoving", true);
    }
    public void OnUpdate()
    {
        if (orc.target == null) return;

        Vector3 dir = (orc.target.position - orc.transform.position).normalized;
        orc.transform.position += dir * orc.enemyData.speed * Time.deltaTime;

        orc.transform.localScale = new Vector3(
            dir.x > 0 ? 0.6f : -0.6f,
            0.6f,
            1f
        );
    }
    public void OnExit()
    {
        Debug.Log("Exiting Orc Move State");
        anim.SetBool("isMoving", true);
        anim.SetBool("isAttacking", false);
    }
}