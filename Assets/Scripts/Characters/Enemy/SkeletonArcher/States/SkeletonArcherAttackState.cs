using System.Collections;
using UnityEngine;

public class SkeletonArcherAttackState : IState
{
    private Animator anim;
    private SkeletonArcher skeletonArcher;
    private float attackCooldown;
    private float cooldownTimer = 0f;

    public SkeletonArcherAttackState(Animator anim, SkeletonArcher skeletonArcher, float attackCooldown)
    {
        this.anim = anim;
        this.skeletonArcher = skeletonArcher;
        this.attackCooldown = attackCooldown;
    }
    public void OnEnter()
    {
        anim.SetBool("isMoving", false);
        if (cooldownTimer < 0f)
        {
            ShootArrow();
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
            ShootArrow();
            cooldownTimer = attackCooldown;
        }
    }

    #region private methods

    private void ShootArrow()
    {
        skeletonArcher.StartCoroutine(DelayedShoot()); 
    }

    private IEnumerator DelayedShoot()
    {
        anim.SetTrigger("isAttack");            
        yield return new WaitForSeconds(0.7f);    
        skeletonArcher.ShootArrow();            
    }

    #endregion
}
