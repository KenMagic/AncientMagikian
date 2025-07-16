using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class OrcPOV : MonoBehaviour
{
    [SerializeField] private float delayToForgetTarget = 1f;

    private Orc orc;
    private Coroutine forgetTargetCoroutine;

    private void Awake()
    {
        orc = GetComponentInParent<Orc>();

        if (orc == null)
        {
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            orc.SetNewTarget(collision.transform);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);
            orc.SetNewTarget(collision.transform);
            orc.StartForgetTargetCoroutine(delayToForgetTarget);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);

            orc.StartForgetTargetCoroutine(delayToForgetTarget);
        }
    }

    #region private methods

    #endregion

}