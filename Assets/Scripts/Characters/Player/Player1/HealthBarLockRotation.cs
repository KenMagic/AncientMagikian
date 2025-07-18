using UnityEngine;

public class HealthBarLockRotation : MonoBehaviour
{
    private Quaternion originalRotation;

    void Start()
    {
        // Ghi lại rotation ban đầu của thanh máu
        originalRotation = transform.rotation;
    }

    void LateUpdate()
    {
        // Cho thanh máu luôn quay thẳng hướng camera (nếu dùng Canvas World Space)
        transform.rotation = Quaternion.identity;
    }

}
