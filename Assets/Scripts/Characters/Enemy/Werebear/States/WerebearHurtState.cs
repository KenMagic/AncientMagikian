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
        if (werebear.currentHealth <= 0)
        {
            werebear.isDeath = true;
            animator.SetTrigger("isDeath");
            werebear.HideAfterDelay(1f);
            WerebearPool.Instance.ReturnObject(werebear.gameObject); 
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
