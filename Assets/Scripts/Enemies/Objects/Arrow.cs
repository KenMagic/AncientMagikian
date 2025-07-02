using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    private Transform target;

    public void SetTarget(Transform target)
    {
        this.target = target;

        // Xoay mũi tên về phía target
        Vector3 dir = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Di chuyển thẳng tới target
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            Debug.Log("Arrow hit " + target.name);
            Destroy(gameObject); // hoặc tạo hiệu ứng, gây damage
        }
    }
}
