using UnityEngine;

public class SlimeHurtState : IState
{
    private Animator anim;
    private Slime slime;
    public SlimeHurtState(Animator anim, Slime slime)
    {
        this.anim = anim;
        this.slime = slime;
    }
    public void OnEnter()
    {

        if (slime.currentHealth <= 0)
        {
            slime.isDeath = true;
            anim.SetTrigger("isDeath");
            slime.HideAfterDelay(1f);
            SlimePool.Instance.ReturnObject(slime.gameObject);
        }
        else
        {
            anim.SetTrigger("isHurt");
        }
        
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
