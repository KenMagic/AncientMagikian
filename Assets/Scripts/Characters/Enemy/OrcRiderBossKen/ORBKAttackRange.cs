using Unity.VisualScripting;
using UnityEngine;

public class ORBKAttackRange : MonoBehaviour
{
    OrcRiderBossKen orcRiderBossKen;

    private void Start()
    {
        orcRiderBossKen = GetComponentInParent<OrcRiderBossKen>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter: " + other.name);
        if (other.CompareTag("Player"))
        {
            orcRiderBossKen.isInAttackRange = true;
        }
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            orcRiderBossKen.isInAttackRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            orcRiderBossKen.isInAttackRange = false;
        }
    }
}
