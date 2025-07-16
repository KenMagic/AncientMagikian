using UnityEngine;

public class OrcHitbox : MonoBehaviour
{
    private Orc orc;
    private string currentAttackState = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        orc = GetComponentInParent<Orc>();

        if (orc == null)
        {
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(tag:"Player"))
        {
            orc.SetAttackState("isAttackPlayer");
            currentAttackState = "isAttackPlayer";
        }

        if (collision.CompareTag(tag:"Tower"))
        {
            orc.SetAttackState("isAttackTower");
            currentAttackState = "isAttackTower";
        }

        if (collision.CompareTag(tag: "PlayerAttack"))
        {
            orc.SetHurtState();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(tag: "Player"))
        {
            orc.SetAttackState("isAttackPlayer");
            currentAttackState = "isAttackPlayer";
        }

        if (collision.CompareTag(tag: "Tower"))
        {
            orc.SetAttackState("isAttackTower");
            currentAttackState = "isAttackTower";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!string.IsNullOrEmpty(currentAttackState) && collision.CompareTag("PlayerAttack"))
        {
            orc.SetAttackState(currentAttackState); 
            currentAttackState = ""; 
            return;
        }
        orc.SetMoveState();
    }
}
