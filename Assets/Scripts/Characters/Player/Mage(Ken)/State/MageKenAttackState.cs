using UnityEngine;
public class MageKenAttackState : IState
{
    private MageKen mageKen;

    public MageKenAttackState(MageKen mageKen)
    {
        this.mageKen = mageKen;
    }

    public void OnEnter()
    {
        mageKen.animator.SetTrigger("Attack");
        mageKen.animator.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        mageKen.isAttacking = false;
        mageKen.animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        // Logic for attack state update can be added here if needed
    }
}