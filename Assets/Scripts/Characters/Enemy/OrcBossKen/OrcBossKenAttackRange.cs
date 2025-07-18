using Unity.VisualScripting;
using UnityEngine;

public class OrcBossKenAttackRange : MonoBehaviour
{
    OrcBossKen orcBossKen;

    private void Start()
    {
        orcBossKen = GetComponentInParent<OrcBossKen>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter: " + other.name);
        if (other.CompareTag("Player"))
        {
            orcBossKen.isInAttackRange = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            orcBossKen.isInAttackRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            orcBossKen.isInAttackRange = false;
        }
    }
}
