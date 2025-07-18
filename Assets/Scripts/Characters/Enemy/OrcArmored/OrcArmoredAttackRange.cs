using UnityEngine;

public class OrcArmoredAttackRange : MonoBehaviour
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
        Debug.Log("Target" + collision.tag);

        if (collision.CompareTag("Player"))
        {
            orcArmored.SetAttackState();
        }
        if (collision.CompareTag("Tower"))
        {
            orcArmored.SetAttackState();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            orcArmored.SetAttackState();
        }
        if (collision.CompareTag("Tower"))
        {
            orcArmored.SetAttackState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            orcArmored.SetMoveState();
        }
    }
}
