using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public Transform Target;
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            EnemyMovement enemyMovement = GetComponentInParent<EnemyMovement>();
            enemyMovement.SetTarget(collider.transform);
        }
    }
    public void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            EnemyMovement enemyMovement = GetComponentInParent<EnemyMovement>();
            enemyMovement.SetTarget(Target);
        }
    }
    
}