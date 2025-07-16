using UnityEngine;

public class OrcRiderHurtState : IState
{
    private Animator anim;
    private OrcRider orcRider;

    public OrcRiderHurtState(Animator anim, OrcRider orcRider)
    {
        this.anim = anim;
        this.orcRider = orcRider;
    }

    public void OnEnter()
    {
        anim.SetTrigger("isHurt");
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
        orcRider.DamegeTaken();
        if (orcRider.enemyData.health <= 0)
        {
            anim.SetTrigger("Death");
            orcRider.HideAfterDelay(0.5f);
        }
    }
    #endregion
}
