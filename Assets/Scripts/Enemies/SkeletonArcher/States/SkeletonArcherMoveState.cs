using UnityEngine;

public class SkeletonArcherMoveState : IState
{
    private Animator anim;
    private SkeletonArcher archer;

    public SkeletonArcherMoveState(Animator anim, SkeletonArcher archer)
    {
        this.anim = anim;
        this.archer = archer;
    }

    public void OnEnter()
    {
        anim.SetBool("isAttacking", false);
        anim.SetBool("isMoving", true);
    }

    public void OnUpdate()
    {
        if (archer.target == null) return;

        float distance = Vector2.Distance(archer.transform.position, archer.target.position);

        // Nếu trong tầm bắn → chuyển sang attack
        if (distance <= archer.distanceToTarget)
        {
            archer.stateMachine.SetState(new SkeletonArcherAttackState(anim, archer, archer.enemyData.attackCooldown));
            return;
        }

        // Nếu chưa đủ gần → tiếp tục tiến tới
        Vector3 dir = (archer.target.position - archer.transform.position).normalized;
        archer.transform.position += dir * archer.enemyData.speed * Time.deltaTime;

        archer.transform.localScale = new Vector3(
            dir.x > 0 ? 0.6f : -0.6f,
            0.6f,
            1f
        );
    }

    public void OnExit()
    {
        Debug.Log("Exiting SkeletonArcher Move State");
        anim.SetBool("isMoving", false);
        anim.SetBool("isAttacking", false);
    }
}
