using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class KnightKenAttackState : IState
{
    private Animator animator;

    public KnightKenAttackState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        AudioManager.Instance.PlayMelee();
        animator.SetTrigger("Attack");
        animator.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        
    }

}