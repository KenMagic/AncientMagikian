using UnityEngine;

public class WerebearHitbox : MonoBehaviour
{
    private Werebear werebear;

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
        if (collision.CompareTag("PlayerAttack"))
        {
            werebear.SetHurtState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            werebear.SetAttackState();
        }
        werebear.SetMoveState();
    }
}
