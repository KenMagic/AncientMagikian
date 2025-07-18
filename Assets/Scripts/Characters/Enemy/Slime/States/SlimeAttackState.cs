using UnityEngine;

public class SlimeAttackState : IState
{
    private Animator anim;
    private Slime slime;
    private float attackCooldown; // Cooldown time between attacks

    private float cooldownTimer = 0f; // Timer to track cooldown
    public SlimeAttackState(Animator anim, Slime slime, float attackCooldown)
    {
        this.anim = anim;
        this.slime = slime;
        this.attackCooldown = attackCooldown;
    }
    public void OnEnter()
    {
        anim.SetBool("isMoving", false);
        if (cooldownTimer < 0f)
        {
            Attack();
        }
    }

    public void OnExit()
    {
        anim.SetBool("isMoving", true);
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
            anim.SetTrigger("isSuprise");
            slime.Suprise(slime.target.gameObject, slime.enemyData.attackDamage * 5);
        }
        else
        {
            anim.SetTrigger("isAttack");
            slime.DealDamage(slime.target.gameObject);
        }
    }
    #endregion
}
