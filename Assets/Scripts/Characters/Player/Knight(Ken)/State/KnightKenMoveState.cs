using UnityEngine;
public class KnightKenMoveState : IState
{
    private Animator animator;

    private float moveSpeed;

    public KnightKenMoveState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        moveSpeed = animator.GetComponent<KnightKen>().GetMoveSpeed();
        animator.SetBool("isMoving", true);
    }

    public void OnExit()
    {
        animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        if (animator.GetComponent<KnightKen>().IsStunned)
        {
            animator.SetBool("isMoving", false);
            return; // Do not process movement if stunned
        }
        if (Input.GetKey(KeyCode.W))
        {
            Move(Vector3.up, moveSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            Move(Vector3.down, moveSpeed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            Move(Vector3.left, moveSpeed);
            animator.transform.localScale = new Vector3(-1, 1, 1); // Flip character to face left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.right, moveSpeed);
            animator.transform.localScale = new Vector3(1, 1, 1); // Face right
        }
    }
    // Logic for movement can be added here, such as updating position based on input
    public void Move(Vector3 direction, float speed)
    {
        // Example movement logic
        Vector3 newPosition = animator.transform.position + direction * speed * Time.deltaTime;
        animator.transform.position = newPosition;

        // Update scale based on direction
        if (direction.x > 0)
        {
            animator.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            animator.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}