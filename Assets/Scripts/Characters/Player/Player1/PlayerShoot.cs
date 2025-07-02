using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject bulletPrefab;     // Kéo prefab Bullet vào đây
    public Transform firePoint;         // Kéo FirePoint vào đây
    public float bulletSpeed = 10f;

    public float specialCooldown = 3f;  // Thời gian hồi chiêu đặc biệt
    private float specialTimer = 0f;

    void Update()
    {
        // Đếm ngược thời gian hồi chiêu
        if (specialTimer > 0)
            specialTimer -= Time.deltaTime;

        // Bắn đạn thường khi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            ShootNormal();
        }

        // Bắn đạn 8 hướng khi nhấn Space và cooldown xong
        if (Input.GetKeyDown(KeyCode.Space) && specialTimer <= 0f)
        {
            ShootSpecial();
            specialTimer = specialCooldown;
        }
    }

    void ShootNormal()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.SetDirection(direction);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void ShootSpecial()
    {
        float[] angles = { 0, 45, 90, 135, 180, 225, 270, 315 };
        float spawnOffset = 1f; // Khoảng cách đẩy viên đạn ra khỏi firePoint

        foreach (float angle in angles)
        {
            float rad = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));

            Vector3 spawnPos = firePoint.position + (Vector3)(direction * spawnOffset);

            GameObject bullet = Instantiate(bulletPrefab, spawnPos, Quaternion.Euler(0f, 0f, angle));

            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController != null)
            {
                bulletController.SetDirection(direction);
            }
        }
    }

}
