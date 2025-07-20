
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera cameraMain;
    public Transform target;
    public float smoothSpeed = 0.125f;

    Vector3 offset = new Vector3(0, 0, -10);

    void Start()
    {
        if (cameraMain == null)
        {
            cameraMain = Camera.main;
        }
    }
    void Update()
    {
        FollowTarget();
    }
    void FollowTarget()
    {
        if (target == null) return;
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(cameraMain.transform.position, desiredPosition, smoothSpeed);
        cameraMain.transform.position = smoothedPosition;
    }

    public void SetTarget(Transform t)
    {
        this.target = t;
    }
}
