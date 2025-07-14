using UnityEngine;

public class OrcHitbox : MonoBehaviour
{
    private Orc orc;
    private string currentAttackState = "";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        orc = GetComponentInParent<Orc>();

        if (orc == null)
        {
            Debug.LogError("❌ Không tìm thấy component Orc trong OrcPOV! Check hierarchy.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag(tag:"Player"))
        {
            Debug.Log("Orc hitbox: Player vào vùng tấn công");
            orc.SetAttackState("isAttackPlayer");
            orc.DealDamage();
            currentAttackState = "isAttackPlayer"; // Cập nhật trạng thái tấn công hiện tại
        }

        if (collision.CompareTag(tag:"Tower"))
        {
            Debug.Log("Orc hitbox: Tower vào vùng tấn công");
            orc.SetAttackState("isAttackTower");
            orc.DealDamage();
            currentAttackState = "isAttackTower"; // Cập nhật trạng thái tấn công hiện tại
        }

        if (collision.CompareTag(tag: "PlayerAttack"))
        {
            Debug.Log("Orc hitbox: PlayerAttack vào vùng tấn công");
            orc.SetHurtState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!string.IsNullOrEmpty(currentAttackState) && collision.CompareTag("PlayerAttack"))
        {
            orc.SetAttackState(currentAttackState); // Giữ nguyên trạng thái tấn công khi rời khỏi hitbox
            currentAttackState = ""; // Reset trạng thái tấn công
            return;
        }
        orc.SetMoveState(); // Quay lại di chuyển khi không còn mục tiêu
    }
}
