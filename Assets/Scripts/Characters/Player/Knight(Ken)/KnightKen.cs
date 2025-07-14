
using UnityEngine;

public class KnightKen : MonoBehaviour
{
    [SerializeField]
    private GameObject attackHitbox;
    private Animator animator;

    private StateMachine stateMachine;

    IState idleState;
    IState moveState;
    IState attackState;
    void Start()
    {
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine();

        // Initialize and set up states
        idleState = new KnightKenIdleState(animator);
        moveState = new KnightKenMoveState(animator);
        attackState = new KnightKenAttackState(animator);
        animator.SetBool("isMoving", false);
        stateMachine.SetState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Example for attack input
        {
            stateMachine.SetState(attackState);
        }
        // Handle movement input and state transitions here
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            stateMachine.SetState(moveState);
        }

        else
        {
            stateMachine.SetState(idleState);
        }

        stateMachine.Update();
    }
    // control wasd 

}

