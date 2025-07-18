using UnityEngine;
public class KnightKenAbilityState : IState
{
    private Animator animator;

    public KnightKenAbilityState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("Ability");
    }

    public void OnExit()
    {
        animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        
    }
}