using UnityEngine;

public class WerebearAttackRange : MonoBehaviour
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
        if (collision.CompareTag("Player"))
        {
            werebear.SetAttackState();
        }
        if (collision.CompareTag("Tower"))
        {
            werebear.SetAttackState();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            werebear.SetAttackState();
        }
        if (collision.CompareTag("Tower"))
        {
            werebear.SetAttackState();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            werebear.SetMoveState();
        }
    }
}
