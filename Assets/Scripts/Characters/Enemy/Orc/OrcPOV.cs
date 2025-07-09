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
            Debug.LogError("❌ Không tìm thấy component Orc trong OrcPOV! Check hierarchy.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player vào vùng phát hiện (POV)");
            orc.SetNewTarget(collision.transform);
        }

        if (collision.CompareTag("Tower"))
        {
            Debug.Log("Vào vùng Tower (POV)");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player rời khỏi vùng (POV)");

            if (forgetTargetCoroutine != null)
                StopCoroutine(forgetTargetCoroutine);

            orc.StartForgetTargetCoroutine(delayToForgetTarget);
        }

        if (collision.CompareTag("Tower"))
        {
            Debug.Log("Rời vùng Tower (POV)");
            orc.SetMoveState(); // Quay lại di chuyển
        }
    }

    #region private methods

    #endregion

}