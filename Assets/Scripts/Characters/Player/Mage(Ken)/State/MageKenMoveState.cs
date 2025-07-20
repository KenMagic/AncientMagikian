using UnityEngine;
public class MageKenMoveState : IState
{
    private MageKen mageKen;

    private float moveSpeed;

    public MageKenMoveState(MageKen mageKen)
    {
        this.mageKen = mageKen;
    }

    public void OnEnter()
    {
        moveSpeed = mageKen.GetMoveSpeed();
        mageKen.animator.SetBool("isMoving", true);
    }

    public void OnExit()
    {
        mageKen.animator.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        if (mageKen.IsStunned)
        {
            mageKen.animator.SetBool("isMoving", false);
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
            mageKen.transform.localScale = new Vector3(-1, 1, 1); // Flip character to face left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Move(Vector3.right, moveSpeed);
            mageKen.transform.localScale = new Vector3(1, 1, 1); // Face right
        }
    }
    // Logic for movement can be added here, such as updating position based on input
    public void Move(Vector3 direction, float speed)
    {
        // Example movement logic
        Vector3 newPosition = mageKen.transform.position + direction * speed * Time.deltaTime;
        mageKen.transform.position = newPosition;

        // Update scale based on direction
        if (direction.x > 0)
        {
            mageKen.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            mageKen.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}