using UnityEngine;

public class OrcAttackState : IState
{
    private Animator anim;
    private Orc orc;
    private float attackCooldown;
    private float lastAttackTime;

    public OrcAttackState(Animator anim, Orc orc, float attackCooldown = 1f)
    {
        this.anim = anim;
        this.orc = orc;
        this.attackCooldown = attackCooldown;
    }

    public void OnEnter()
    {
        Debug.Log("Entering Orc Attack State");
        anim.SetBool("isAttacking", true);
        anim.SetBool("isMoving", false);
        lastAttackTime = Time.time - attackCooldown; // Attack immediately
    }

    public void OnUpdate()
    {
        if (orc.target == null) return;

        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        Debug.Log("Orc Attacks!");

        // Trigger animation
        anim.SetTrigger("attack");

        // Damage the target if it has Castle component
        Castle castle = orc.target.GetComponent<Castle>();
        if (castle != null)
        {
            castle.TakeDamage(orc.enemyData.attackDamage);
        }
    }

    public void OnExit()
    {
        Debug.Log("Exiting Orc Attack State");
        anim.SetBool("isAttacking", false);
    }
}
