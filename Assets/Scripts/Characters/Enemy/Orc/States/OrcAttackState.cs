using System.Collections;
using UnityEngine;

public class OrcAttackState : IState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator anim;
    private Orc orc;

    private float attackCooldown = 1.0f; // Cooldown time between attacks
    private float cooldownTimer = 0f;
    private string targetType = "isAttackTower"; // Default attack type, can be set dynamically
    public OrcAttackState(Animator anim, Orc orc, float attackCooldown, string targetType)
    {
        this.anim = anim;
        this.orc = orc;
        this.attackCooldown = orc.enemyData.attackCooldown;
        this.cooldownTimer = 0f;
        this.targetType = targetType;
    }

    public void OnEnter()
    {
        Debug.Log("Orc Attack State Entered:" + attackCooldown);
        Debug.Log("checkMOve - attack state - ENTER");
        anim.SetBool("isMoving", false);
        anim.SetTrigger(targetType);

        cooldownTimer = 0f;
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
            SetAttackAnimation(targetType);
            Attack();
            cooldownTimer = attackCooldown;
        }
    }

    #region private methods
    private void SetAttackAnimation(string targetType)
    {
        anim.SetTrigger(targetType);
    }

    private void Attack()
    {
        Debug.Log("Orc is attacking!");
        orc.DealDamage();
    }
    #endregion
}
