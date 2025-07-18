using UnityEngine;
public class OrcBossKenAbilityState : IState
{
    private Animator animator;
    private OrcBossKen orcBoss;

    public OrcBossKenAbilityState(Animator animator)
    {
        this.animator = animator;
        this.orcBoss = animator.GetComponent<OrcBossKen>();
    }

    public void OnEnter()
    {
        animator.SetTrigger("Ability");
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
