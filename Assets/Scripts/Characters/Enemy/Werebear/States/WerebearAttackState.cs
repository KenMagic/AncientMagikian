using UnityEngine;

public class WerebearAttackState : IState
{
    private readonly Animator animator;
    private readonly Werebear werebear;
    private readonly float attackCooldown;
    private float cooldownTimer = 0f; // Timer to track cooldown
    private float skillCondition = 100f;

    public WerebearAttackState(Animator animator, Werebear werebear, float attackCooldown)
    {
        this.animator = animator;
        this.werebear = werebear;
        this.attackCooldown = attackCooldown;
        skillCondition = werebear.enemyData.health * 0.35f;
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
            if (werebear.enemyData.health < skillCondition) 
            {
                Skill();
            }
            else
            {
                Attack();
            }
            cooldownTimer = attackCooldown;
        }
    }

    #region private methods
    private void Attack()
    {
        AudioManager.Instance.PlayMelee();
        animator.SetTrigger("isAttack");
        werebear.DealDamage(werebear.target.gameObject);
    }

    private void Skill()
    {

        animator.SetTrigger("isSkill");
        werebear.Skill(werebear.target.gameObject, werebear.enemyData.attackDamage * 3); 
    }
    #endregion
}

