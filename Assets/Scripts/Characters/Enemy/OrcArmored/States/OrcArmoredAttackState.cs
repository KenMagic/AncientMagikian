using UnityEngine;

public class OrcArmoredAttackState : IState
{
    private OrcArmored orcArmored;
    private Animator animator;
    private float attackCooldown;

    private float cooldownTimer = 0f;
    public OrcArmoredAttackState(OrcArmored orcArmored, Animator animator, float attackCooldown)
    {
        this.orcArmored = orcArmored;
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
        Debug.Log($"orcArmored Attack Chance: {chance}");
        if (chance < 35)
        {
            animator.SetTrigger("isSkill");
            orcArmored.Skill(orcArmored.target.gameObject, 100);
        }
        else
        {
            animator.SetTrigger("isAttack");
            orcArmored.DealDamage(orcArmored.target.gameObject);
        }
    }
    #endregion
}
