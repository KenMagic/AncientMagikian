using UnityEngine;

public class OrcArmoredHitbox : MonoBehaviour
{
    private OrcArmored orcArmored;

    private void Awake()
    {
        orcArmored = GetComponentInParent<OrcArmored>();
        if (orcArmored == null)
        {
            Debug.LogError("OrcArmored component not found in parent object.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            orcArmored.SetHurtState();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerAttack"))
        {
            orcArmored.SetAttackState();
        }
        orcArmored.SetMoveState();
    }
}
