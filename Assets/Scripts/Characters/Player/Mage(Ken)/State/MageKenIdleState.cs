using UnityEngine;
public class MageKenIdleState : IState
{
    private MageKen mageKen;

    public MageKenIdleState(MageKen mageKen)
    {
        this.mageKen = mageKen;
    }

    public void OnEnter()
    {
        Debug.Log("MageKenIdleState: OnEnter");
        mageKen.animator.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        mageKen.animator.SetBool("isMoving", true);
    }

    public void OnUpdate()
    {
        // Logic for idle state can be added here
    }
}