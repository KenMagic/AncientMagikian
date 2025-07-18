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

        if (slime.enemyData.health <= 0)
        {
            anim.SetTrigger("isDeath");
            slime.HideAfterDelay(1f);
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
