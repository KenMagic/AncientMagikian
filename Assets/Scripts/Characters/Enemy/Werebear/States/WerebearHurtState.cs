using UnityEngine;

public class WerebearHurtState : IState
{
    private Animator animator;
    private Werebear werebear;

    public WerebearHurtState(Animator animator, Werebear werebear)
    {
        this.animator = animator;
        this.werebear = werebear;
    }

    public void OnEnter()
    {
        if (werebear.enemyData.health <= 0)
        {
            animator.SetTrigger("isDeath");
            werebear.HideAfterDelay(1f);
        }
        else
        {
            animator.SetTrigger("isHurt");
        }
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
