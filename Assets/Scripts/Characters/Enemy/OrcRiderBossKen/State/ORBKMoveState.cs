using UnityEngine;
public class ORBKMoveState : IState
{
    private Animator animator;
    private OrcRiderBossKen orc;

    public ORBKMoveState(Animator animator)
    {
        this.animator = animator;
        this.orc = animator.GetComponent<OrcRiderBossKen>();
    }

    public void OnEnter()
    {
        animator.SetBool("isMoving", true);
    }

    public void OnExit()
    {
        animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        MoveTowardsTarget();
    }

    private void MoveTowardsTarget()
    {
        GameObject target = orc.target;

        // Flip the orc based on the target's position
        if (target.transform.position.x < orc.transform.position.x)
        {
            orc.transform.localScale = new Vector3(-Mathf.Abs(orc.transform.localScale.x), orc.transform.localScale.y, orc.transform.localScale.z);
        }
        else
        {
            orc.transform.localScale = new Vector3(Mathf.Abs(orc.transform.localScale.x), orc.transform.localScale.y, orc.transform.localScale.z);
        }

        // Move towards the target
        orc.transform.position = Vector2.MoveTowards(orc.transform.position, target.transform.position, orc.enemyData.speed * Time.deltaTime);
    }
}