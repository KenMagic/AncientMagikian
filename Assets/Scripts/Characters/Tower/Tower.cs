using UnityEngine;

public class Tower : MonoBehaviour, IDamagable
{
    [SerializeField] private float maxHealth = 1000f;
    private float currentHealth;

    [SerializeField] private Animator animator; // Gắn animator nếu có animation hurt

    private HealthBar healthBar;

    private void Start()
    {
        healthBar = GetComponentInChildren<HealthBar>();
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // Trigger animation Hurt nếu có
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }

        if (currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Tower destroyed!");
        GameController.Instance.GameOver();
    }
}
