using UnityEngine;

public class GreatswordSkeletonAttackState : IState
{
    private GreatswordSkeleton greatswordSkeleton;
    private Animator animator;
    private float attackCooldown;

    private float cooldownTimer = 0f; // Timer to track cooldown

    public GreatswordSkeletonAttackState(GreatswordSkeleton greatswordSkeleton, Animator animator, float attackCooldown)
    {
        this.greatswordSkeleton = greatswordSkeleton;
        this.animator = animator;
        this.attackCooldown = attackCooldown;
    }

    public void OnEnter()
    {
        animator.SetBool("isMoving", false);
        if (cooldownTimer < 0f)
        {
            Attack();
        }
    }

    public void OnExit()
    {
        animator.SetBool("isMoving", true);
    }

    public void OnUpdate()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0f)
        {
            Attack();
            cooldownTimer = attackCooldown;
        }
    }

    #region private methods
    private void Attack()
    {
        int chance = Random.Range(0, 100);
        Debug.Log($"Slime Attack Chance: {chance}");
        if (chance < 10)
        {
            animator.SetTrigger("isCrit");
            greatswordSkeleton.Skill(greatswordSkeleton.target.gameObject, greatswordSkeleton.enemyData.attackDamage * 5);
        }
        else
        {
            animator.SetTrigger("isAttack");
            greatswordSkeleton.DealDamage(greatswordSkeleton.target.gameObject);
        }

        animator.SetTrigger("isAttack");
        greatswordSkeleton.DealDamage(greatswordSkeleton.target.gameObject);
    }
    #endregion
}
