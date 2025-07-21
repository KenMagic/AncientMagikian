using UnityEngine;

public class BulletControllerKen : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 10f;
    public float maxDistance = 30f;

    private Vector2 moveDirection;
    private bool hasDirection = false;
    private Vector3 startPosition;

    public GameObject explosionPrefab;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction.normalized;
        startPosition = transform.position;
        hasDirection = true;
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    void Update()
    {
        if (!hasDirection) return;

        // Di chuyển
        transform.position += (Vector3)(moveDirection * speed * Time.deltaTime);

        // Kiểm tra khoảng cách
        float traveledDistance = Vector3.Distance(startPosition, transform.position);
        if (traveledDistance >= maxDistance)
        {
            Debug.Log("FireBall destroyed: exceeded max distance.");
            FireBallPool.Instance.ReturnObject(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hitbox"))
        {
            var target = collision.GetComponentInParent<IDamagable>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                AudioManager.Instance?.PlayBlast();
                AudioManager.Instance?.PlayHit();
            }

            FireBallPool.Instance.ReturnObject(gameObject);
        }
    }
}
