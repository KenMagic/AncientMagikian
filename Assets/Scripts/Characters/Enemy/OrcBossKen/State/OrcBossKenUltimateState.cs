using UnityEngine;
public class OrcBossKenUltimateState : IState
{
    private Animator animator;

    public OrcBossKenUltimateState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetTrigger("Ultimate");
    }

    public void OnExit()
    {
   }

    public void OnUpdate()
    {
        // Implement ultimate logic here
    }
}
