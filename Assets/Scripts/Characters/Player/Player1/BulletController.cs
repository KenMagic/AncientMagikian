using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 moveDirection;

    public GameObject explosionPrefab;

    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;  // ✅ Đảm bảo là vector đơn vị
    }

    void Update()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject); // Hủy đạn
        }
        // Tạo hiệu ứng nổ
        
    }
}
