using UnityEngine;

public class WerebearPOV : MonoBehaviour
{
    [SerializeField] private float delayToForgetTarget = 1f;

    private Werebear werebear;
    private Coroutine forgetTargetCoroutine;

    private void Awake()
    {
        werebear = GetComponentInParent<Werebear>();
        if (werebear == null)
        {
            Debug.LogError("Werebear component not found in parent object.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            werebear.SetNewTarget(collision.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);
            werebear.SetNewTarget(collision.transform);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);
            werebear.StartForgetTargetCoroutine(delayToForgetTarget);
        }
    }
}
