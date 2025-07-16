//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;

//public class PlayerHealth : MonoBehaviour
//{
//    private int currentHealth;
//    private bool isDead = false;

//    private Animator animator;
//    private PlayerStats playerStats;

//    public Image healthBar; // Thanh máu trên UI

//    void Start()
//    {
//        animator = GetComponent<Animator>();
//        playerStats = GetComponent<PlayerStats>();

//        if (playerStats == null)
//        {
//            Debug.LogError("PlayerStats not found on Player!");
//            return;
//        }

//        currentHealth = playerStats.maxHealth;
//        UpdateHealthBar();
//    }

//    public void TakeDamage(int damage)
//    {
//        if (isDead) return;

//        currentHealth -= damage;

//        if (currentHealth > 0)
//        {
//            animator.SetTrigger("Hurt");
//            StartCoroutine(ResetHurtTriggerAfterDelay());
//        }
//        else
//        {
//            currentHealth = 0;
//            Die();
//        }

//        UpdateHealthBar();
//    }

//    public void Heal(int amount)
//    {
//        if (isDead) return;

//        currentHealth += amount;
//        if (currentHealth > playerStats.maxHealth)
//        {
//            currentHealth = playerStats.maxHealth;
//        }

//        UpdateHealthBar();
//    }

//    private void UpdateHealthBar()
//    {
//        if (healthBar != null && playerStats != null)
//        {
//            healthBar.fillAmount = (float)currentHealth / playerStats.maxHealth;
//        }
//    }

//    private void Die()
//    {
//        isDead = true;
//        animator.SetTrigger("Die");
//        UpdateHealthBar();

//        // Vô hiệu hóa chuyển động và bắn
//        var move = GetComponent<Player1Movement>();
//        if (move != null) move.enabled = false;

//        var shoot = GetComponent<PlayerShoot>();
//        if (shoot != null) shoot.enabled = false;

//        Rigidbody2D rb = GetComponent<Rigidbody2D>();
//        if (rb != null) rb.linearVelocity = Vector2.zero;

//        StartCoroutine(WaitAndHandleDeath());
//    }

//    IEnumerator WaitAndHandleDeath()
//    {
//        yield return new WaitForSeconds(2f);
//        Debug.Log("Người chơi đã chết!");
//        // TODO: Load lại scene hoặc hiển thị menu thua
//    }

//    IEnumerator ResetHurtTriggerAfterDelay()
//    {
//        yield return new WaitForSeconds(0.5f);
//        animator.ResetTrigger("Hurt");
//    }
//}


using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    private int currentHealth;
    private bool isDead = false;

    private Animator animator;
    private PlayerStats playerStats;

    public Image healthBar; // Thanh máu trên UI

    [Header("Tự động trừ máu định kỳ")]
    public float healthReduceInterval = 10f;  // thời gian (giây) giữa mỗi lần trừ máu
    public int damagePerInterval = 1;          // lượng máu trừ mỗi lần

    void Start()
    {
        animator = GetComponent<Animator>();
        playerStats = GetComponent<PlayerStats>();

        if (playerStats == null)
        {
            Debug.LogError("PlayerStats not found on Player!");
            return;
        }

        currentHealth = playerStats.maxHealth;
        UpdateHealthBar();

        // Bắt đầu coroutine trừ máu định kỳ
        StartCoroutine(ReduceHealthPeriodically());
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            StartCoroutine(ResetHurtTriggerAfterDelay());
        }
        else
        {
            currentHealth = 0;
            Die();
        }

        UpdateHealthBar();
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth += amount;
        if (currentHealth > playerStats.maxHealth)
        {
            currentHealth = playerStats.maxHealth;
        }

        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null && playerStats != null)
        {
            healthBar.fillAmount = (float)currentHealth / playerStats.maxHealth;
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        UpdateHealthBar();

        var move = GetComponent<Player1Movement>();
        if (move != null) move.enabled = false;

        var shoot = GetComponent<PlayerShoot>();
        if (shoot != null) shoot.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        StartCoroutine(WaitAndHandleDeath());
    }

    IEnumerator WaitAndHandleDeath()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Người chơi đã chết!");
        // TODO: Load lại scene hoặc hiển thị menu thua
    }

    IEnumerator ResetHurtTriggerAfterDelay()
    {
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("Hurt");
    }

    IEnumerator ReduceHealthPeriodically()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(healthReduceInterval);
            TakeDamage(damagePerInterval);
        }
    }
}
