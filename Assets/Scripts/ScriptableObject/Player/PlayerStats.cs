using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 5;
    public float damage = 1f;
    public float fireRate = 1f;
    public float moveSpeed = 10f;

    private PlayerHealth playerHealth;
    private PlayerShoot playerShoot;
    private Player1Movement playerMovement;

    void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerShoot = GetComponent<PlayerShoot>();
        playerMovement = GetComponent<Player1Movement>();
    }

    public void ApplyUpgrade(PlayerSO upgrade)
    {
        // Máu
        maxHealth += upgrade.extraHealth;
        if (upgrade.extraHealth > 0 && playerHealth != null)
        {
            playerHealth.Heal(upgrade.extraHealth);
        }

        // Sát thương
        damage += upgrade.extraDamage;

        // Tốc độ bắn
        fireRate *= upgrade.attackSpeedMultiplier;
        if (playerShoot != null)
        {
            playerShoot.UpdateFireRate(fireRate); // Gọi hàm cập nhật
        }

        // Tốc độ chạy
        moveSpeed *= upgrade.moveSpeedMultiplier;
        if (playerMovement != null)
        {
            playerMovement.UpdateMoveSpeed(moveSpeed); // Gọi hàm cập nhật
        }

        Debug.Log("Đã nâng cấp: " + upgrade.upgradeName);
    }
}
