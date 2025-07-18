using UnityEngine;

public class KnightKenUltimateState : IState
{
    private Animator animator;

    public KnightKenUltimateState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("Untimate");
        GameObject boost = Resources.Load<GameObject>("VFX/Buff/AttackBoost");
        StatusUIManager.Instance.SpawnStatusUI(animator.gameObject, boost);
    }

    public void OnExit()
    {
        animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        //performed in animation events
    }
}