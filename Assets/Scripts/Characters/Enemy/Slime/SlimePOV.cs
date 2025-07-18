using UnityEngine;

public class SlimePOV : MonoBehaviour
{
    [SerializeField] private float delayToForgetTarget = 1f;

    private Slime slime;
    private Coroutine forgetTargetCoroutine;
    float speed = 0f;


    private void Awake()
    {
        slime = GetComponentInParent<Slime>();
        if (slime == null)
        {
            Debug.LogError("Slime component not found in parent object.");
        }
        speed = slime.enemyData.speed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            slime.SetNewTarget(collision.transform);
            slime.enemyData.speed = speed * 1.5f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);
            slime.SetNewTarget(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            slime.enemyData.speed = speed;
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);
            slime.StartForgetTargetCoroutine(delayToForgetTarget);
        }
    }
}
