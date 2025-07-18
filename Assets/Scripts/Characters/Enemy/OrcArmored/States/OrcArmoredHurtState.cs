using UnityEngine;

public class OrcArmoredHurtState : IState
{
    private Animator anim;
    private OrcArmored orcArmored;

    public OrcArmoredHurtState(Animator anim, OrcArmored orcArmored)
    {
        this.anim = anim;
        this.orcArmored = orcArmored;
    }

    public void OnEnter()
    {
        if (orcArmored.enemyData.health <= 0)
        {
            anim.SetTrigger("isDeath");
            orcArmored.HideAfterDelay(1f);
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
