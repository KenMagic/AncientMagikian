using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    private Animator animator;
    private bool isDead = false;

    // Tham chiếu đến Image của thanh máu
    public Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHealthBar();
        StartCoroutine(ReduceHealthPeriodicallyEvery3Seconds()); // Bắt đầu coroutine giảm máu mỗi 3 giây
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
            StartCoroutine(ResetHurtTriggerAfterDelay()); // Chờ và reset trigger Hurt
            UpdateHealthBar();
        }
        else
        {
            Die();
        }
    }

    private void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Cập nhật fill amount dựa trên máu hiện tại (từ 0 đến 1)
            healthBar.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        UpdateHealthBar(); // Cập nhật thành 0 khi chết

        // Vô hiệu hóa di chuyển và bắn
        var move = GetComponent<Player1Movement>();
        if (move != null) move.enabled = false;

        var shoot = GetComponent<PlayerShoot>();
        if (shoot != null) shoot.enabled = false;

        // Dừng vận tốc nếu có Rigidbody2D
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.linearVelocity = Vector2.zero;

        StartCoroutine(WaitAndHandleDeath());
    }

    IEnumerator WaitAndHandleDeath()
    {
        yield return new WaitForSeconds(2f); // Chờ animation Die kết thúc

        Debug.Log("Người chơi đã chết!");
        // TODO: Tải lại scene hoặc hiển thị menu thua
    }

    IEnumerator ResetHurtTriggerAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Chờ 0.5 giây (thời lượng animation Hurt)
        animator.ResetTrigger("Hurt"); // Reset trigger Hurt sau khi animation hoàn thành
    }

    IEnumerator ReduceHealthPeriodicallyEvery3Seconds()
    {
        while (!isDead) // Tiếp tục giảm máu cho đến khi chết
        {
            yield return new WaitForSeconds(3f); // Chờ 3 giây
            TakeDamage(1); // Gọi hàm TakeDamage để giảm 1 máu
        }
    }
}