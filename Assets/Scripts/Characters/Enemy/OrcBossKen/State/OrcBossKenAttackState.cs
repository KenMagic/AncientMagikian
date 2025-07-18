using UnityEngine;

public class OrcBossKenAttackState : IState
{
    private Animator animator;

    public OrcBossKenAttackState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetTrigger("Attack");
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
        // Implement attack logic here
    }
}
