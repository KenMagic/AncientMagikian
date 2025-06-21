using UnityEngine;

public class ChasePlayer : MonoBehaviour
{
    public GameObject tower; // Reference to the player GameObject
    public float chaseSpeed = 3f; // Speed at which the enemy chases the player
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is assigned and within a certain distance
        if (tower != null)
        {
            Chase();
        }
    }

    void Chase()
    {
        if (tower != null)
        {
            // Calculate the direction towards the tower
            Vector2 direction = (tower.transform.position - transform.position).normalized;
            // Move the enemy towards the tower
            transform.position = Vector2.MoveTowards(transform.position, tower.transform.position, chaseSpeed * Time.deltaTime);
        }
    }
    
}
