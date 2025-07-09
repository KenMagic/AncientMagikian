using UnityEngine;

public class OrcHurtState : IState
{
    private Animator anim;
    private Orc orc;

    public OrcHurtState(Animator anim, Orc orc)
    {
        this.anim = anim;
        this.orc = orc;
    }

    public void OnEnter()
    {
        anim.SetTrigger("Hurt");
        TakeDamage();

    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }

    #region private methods
    public void TakeDamage()
    {
        Debug.Log("Orc is taking damage!");
        orc.DamegeTaken();
        if (orc.enemyData.health <= 0)
        {
            anim.SetTrigger("Death");
            orc.HideAfterDelay(0.5f);
        }
    }
    #endregion
}
