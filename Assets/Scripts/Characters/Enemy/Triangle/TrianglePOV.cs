using UnityEngine;

public class TrianglePOV : MonoBehaviour
{
    public Transform Target;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Triangle enemyMovement = GetComponentInParent<Triangle>();
            enemyMovement.SetTarget(collider.transform);
        }
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Triangle enemyMovement = GetComponentInParent<Triangle>();
            enemyMovement.SetTarget(Target);
        }
    }
    
}