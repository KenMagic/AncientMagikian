using UnityEngine;

public class SlimeHitbox : MonoBehaviour
{
    private Slime slime;

    private void Awake()
    {
        slime = GetComponentInParent<Slime>();
        if (slime == null)
        {
            Debug.LogError("Slime component not found in parent object.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            slime.SetAttackState();
        }
        slime.SetMoveState();
    }
}
