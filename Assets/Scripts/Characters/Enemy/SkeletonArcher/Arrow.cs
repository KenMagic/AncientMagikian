using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 1f;
    public float maxDistance = 30f;
    [SerializeField] public EnemySO enemyData;

    public Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 direction;
    private bool hasDirection = false;

    public void SetTarget(Vector3 position)
    {
        startPosition = transform.position;
        targetPosition = position; // 👈 Gán cái này trước khi dùng nó!

        direction = (targetPosition - startPosition).normalized;
        hasDirection = true;

        // Xoay mũi tên theo hướng bay
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }


    // Update is called once per frame
    void Update()
    {
        if (!hasDirection) return;

        float distanceThisFrame = speed * Time.deltaTime;
        transform.position += direction * distanceThisFrame;

        float traveledDistance = Vector3.Distance(startPosition, transform.position);
        if (traveledDistance >= maxDistance)
        {
            Debug.Log("Arrow destroyed: exceeded max distance.");
            ArrowPool.Instance.ReturnObject(gameObject);
        }
    }

    public void DealDamage(GameObject gameObject)
    {
        gameObject.GetComponent<IDamagable>()?.TakeDamage(enemyData.attackDamage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Tower"))
        {
            Debug.Log($"Arrow collided with: {collision.gameObject.name}");
            ArrowPool.Instance.ReturnObject(gameObject);
            DealDamage(collision.gameObject);
            return;
        }
    }
}
