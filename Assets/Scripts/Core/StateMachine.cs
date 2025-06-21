using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState currentState;

    public void SetState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit();
        }

        currentState = newState;
        currentState.OnEnter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate();
        }
    }
}