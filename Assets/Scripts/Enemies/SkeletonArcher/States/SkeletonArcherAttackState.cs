using UnityEngine;

public class SkeletonArcherAttackState : IState
{
    private Animator anim;
    private SkeletonArcher archer;
    private float attackCooldown;
    private float lastAttackTime;

    public SkeletonArcherAttackState(Animator anim, SkeletonArcher archer, float attackCooldown = 1f)
    {
        this.anim = anim;
        this.archer = archer;
        this.attackCooldown = attackCooldown;
    }

    public void OnEnter()
    {
        Debug.Log("Entering Skeleton Archer Attack State");
        anim.SetBool("isAttacking", true);
        anim.SetBool("isMoving", false);
        lastAttackTime = Time.time - attackCooldown; // Shoot immediately
    }

    public void OnUpdate()
    {
        if (archer.target == null) return;

        float distance = Vector2.Distance(archer.transform.position, archer.target.position);
        if (distance > archer.distanceToTarget)
        {
            // Nếu mục tiêu rời khỏi tầm bắn → chuyển về MoveState
            archer.stateMachine.SetState(new SkeletonArcherMoveState(anim, archer));
            return;
        }

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            ShootArrow();
            lastAttackTime = Time.time;
        }
    }

    private void ShootArrow()
    {
        Debug.Log("Skeleton Archer shoots!");

        // Trigger animation
        anim.SetTrigger("attack");

        // Gọi logic spawn arrow ở archer
        archer.ShootArrow();
    }

    public void OnExit()
    {
        Debug.Log("Exiting Skeleton Archer Attack State");
        anim.SetBool("isAttacking", false);
        anim.SetBool("isMoving", true);
    }
}
