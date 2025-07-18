using UnityEngine;

public class OrcArmoredPOV : MonoBehaviour
{
    [SerializeField] private float delayToForgetTarget = 1f;

    private OrcArmored orcArmored;
    private Coroutine forgetTargetCoroutine;
    float speed = 0f;


    private void Awake()
    {
        orcArmored = GetComponentInParent<OrcArmored>();
        if (orcArmored == null)
        {
            Debug.LogError("Orc Armored component not found in parent object.");
        }
        speed = orcArmored.enemyData.speed;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            orcArmored.SetNewTarget(collision.transform);
            orcArmored.enemyData.speed = speed * 1.5f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);
            orcArmored.SetNewTarget(collision.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            orcArmored.enemyData.speed = speed;
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);
            orcArmored.StartForgetTargetCoroutine(delayToForgetTarget);
        }
    }
}
