using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    public Rigidbody2D rb;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }
    // Handle player movement
    void HandleMovement()
    {
        if (Keyboard.current.wKey.isPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, moveSpeed);
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -moveSpeed);
        }
        else if (Keyboard.current.aKey.isPressed)
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop movement if no keys are pressed
        }
    }
}
