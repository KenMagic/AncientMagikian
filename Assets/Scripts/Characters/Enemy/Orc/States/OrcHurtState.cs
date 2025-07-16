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
        anim.SetTrigger("isHurt");
            }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }

    #region private methods

    #endregion
}
