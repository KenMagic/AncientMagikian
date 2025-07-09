using System.Collections;
using UnityEngine;

public class OrcAttackState : IState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator anim;
    private Orc orc;
    private float attackCooldown = 1.0f; // Cooldown time between attacks
    private int targetType; // 0 = Tower, 1 = Player

    private float cooldownTimer = 0f;

    public OrcAttackState(Animator anim, Orc orc, float attackCooldown, int targetType)
    {
        this.anim = anim;
        this.orc = orc;
        this.attackCooldown = orc.enemyData.attackCooldown;
        this.targetType = targetType;
        this.cooldownTimer = 0f;
    }

    public void OnEnter()
    {
        Debug.Log("Orc Attack State Entered: " + targetType + ":" + attackCooldown);
        anim.SetInteger("targetType", targetType);
        anim.SetBool("isMoving", false);
        cooldownTimer = 0f;
    }

    public void OnExit()
    {
        anim.SetInteger("targetType", 0);
        anim.SetBool("isMoving", true);
    }

    public void OnUpdate()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer < 0f)
        {
            SetAttackAnimation(targetType);
            Attack();
        }
        else
        {
            SetAttackAnimation(0);
        }
    }

    #region private methods
    private void SetAttackAnimation(int targetType)
    {
        anim.SetInteger("targetType", targetType);
    }

    private void Attack()
    {
        Debug.Log("Orc is attacking!");
        orc.DealDamage();
        cooldownTimer = attackCooldown;
    }
    #endregion
}
