using Unity.VisualScripting;
using UnityEngine;

public class BulletControllerKen : MonoBehaviour
{
    public float speed = 10f;
    private Vector2 moveDirection;

    public GameObject explosionPrefab;
    public float damage = 10f;

    public void SetDirection(Vector2 dir)
    {
        moveDirection = dir.normalized;  // ✅ Đảm bảo là vector đơn vị
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }
    void Start()
    {
        Destroy(this.gameObject,3f);
    }
    void Update()
    {
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Hitbox"))
    //    {
    //        if (explosionPrefab != null)
    //        {
    //            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
    //        }

    //        Destroy(gameObject); // Hủy đạn
    //    }
    //    // Tạo hiệu ứng nổ

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            var target = collision.GetComponent<IDamagable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }


}
