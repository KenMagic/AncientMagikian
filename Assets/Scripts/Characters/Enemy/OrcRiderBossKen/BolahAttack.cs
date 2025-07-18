using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
public class BolahAttack : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint; // Point where the Bolah will be spawned

    private GameObject thrower;

    private GameObject target;

    void Update()
    {
        Vector2 direction = (target.transform.position - spawnPoint.position).normalized;
        this.gameObject.transform.position += (Vector3)direction * 10f * Time.deltaTime; // Adjust speed as neede
    }
    // bolah flies towards the player
    public void PerformBolahAttack(GameObject target, GameObject thrower)
    {
        spawnPoint = thrower.transform;
        this.thrower = thrower;
        this.target = target;
        if (target == null || spawnPoint == null)
        {
            Debug.LogWarning("Target or spawn point is not set for BolahAttack.");
            return;
        }
    }

    //collision with the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IBuffable buffable = other.GetComponent<IBuffable>();
            if (buffable != null)
            {
                buffable.ApplyBuff(new StunDebuff()); // Apply speed buff as an example
            }
            thrower.GetComponent<OrcRiderBossKen>().bolahHit = true; // Set the bolah hit flag to true
            Destroy(gameObject); // Destroy the Bolah after hitting the player
        }
    }
}