using UnityEngine;
public class MageKenUltimateState : IState
{
    private MageKen mageKen;

    public MageKenUltimateState(MageKen mageKen)
    {
        this.mageKen = mageKen;
    }

    public void OnEnter()
    {
        Debug.Log("MageKenUltimateState: OnEnter");
        mageKen.buffManager.ApplyBuff(new VoHanMaLucBuff(mageKen.ultimateSkill.Duration));
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}