using UnityEngine;

public class SlimeAttackRange : MonoBehaviour
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
        if (collision.CompareTag("Player"))
        {
            slime.SetAttackState();
        }
        if (collision.CompareTag(tag: "Tower"))
        {
            slime.SetAttackState();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            slime.SetAttackState();
        }
        if (collision.CompareTag(tag: "Tower"))
        {
            slime.SetAttackState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            slime.SetMoveState();
        }
    }
}
