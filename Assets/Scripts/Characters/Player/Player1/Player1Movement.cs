using UnityEngine;

public class Player1Movement : MonoBehaviour
{
    public float moveSpeed = 10.0f;

    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    void Start()
    {
        // Lấy các component trên GameObject
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    //void Update()
    //{
    //    // Nhận input từ bàn phím
    //    movement.x = Input.GetAxisRaw("Horizontal");
    //    movement.y = Input.GetAxisRaw("Vertical");

    //    // Kiểm tra xem có đang di chuyển không
    //    bool isMoving = movement != Vector2.zero;
    //    animator.SetBool("isMoving", isMoving);
    //}

    void Update()
    {
        // Nhận input từ bàn phím
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Kiểm tra xem có đang di chuyển không
        bool isMoving = movement != Vector2.zero;
        animator.SetBool("isMoving", isMoving);

        // Lật nhân vật theo hướng di chuyển ngang
        if (movement.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1); // Quay sang trái
        }
        else if (movement.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1); // Quay sang phải
        }
    }
    public void UpdateMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
    }

}
