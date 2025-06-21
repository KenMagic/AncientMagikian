// State for the state machine

public interface IState
{
    void OnEnter();
    void OnExit();
    void OnUpdate();
}