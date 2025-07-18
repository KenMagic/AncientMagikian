using UnityEngine;
public class ORBKUltimateState : IState
{
    private Animator animator;

    public ORBKUltimateState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetTrigger("Ultimate");
    }

    public void OnExit()
    {
        // Logic for exiting the ultimate state can be added here if needed
    }

    public void OnUpdate()
    {
        // Implement ultimate logic here if needed
    }
}