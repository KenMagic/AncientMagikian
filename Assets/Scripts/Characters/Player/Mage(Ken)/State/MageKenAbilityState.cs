using UnityEngine;
public class MageKenAbilityState : IState
{
    private MageKen mageKen;

    public MageKenAbilityState(MageKen mageKen)
    {
        this.mageKen = mageKen;
    }

    public void OnEnter()
    {
        mageKen.animator.SetTrigger("Ability");
        mageKen.animator.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        mageKen.animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        
    }
}