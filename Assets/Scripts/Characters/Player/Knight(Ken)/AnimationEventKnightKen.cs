using System.Collections;
using UnityEngine;

public class AnimationEventKnightKen : MonoBehaviour
{
    public GameObject attackHitbox;
    [SerializeField]
    private KnightKen knightKen;
    public void EnableAttackHitbox()
    {
        attackHitbox.SetActive(true);
        DealDamageToEnemiesInHitbox();
    }
    public void DisableAttackHitbox()
    {
        attackHitbox.SetActive(false);
        ResetAttackState();
    }
    public void ResetAttackState()
    {
        KnightKen knightKen = attackHitbox.GetComponentInParent<KnightKen>();
        if (knightKen != null)
        {
            knightKen.isAttacking = false;
        }
    }
    public void DealDamageToEnemiesInHitbox()
    {
        var colliders = GetEnemiesInHitbox();
        if (colliders == null) return;

        foreach (var col in colliders)
        {
            if (col.CompareTag("Hitbox")) 
            {
                var damagable = col.GetComponentInParent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(knightKen.GetAttackDamage());
                    AudioManager.Instance.PlayHit();
                }
                
            }
        }
    }
    public void ApplyAbilitySkill()
    {
        var colliders = GetEnemiesInHitbox();
        if (colliders == null) return;

        foreach (var col in colliders)
        {
            if (col.CompareTag("Hitbox"))
            {
                var damagable = col.GetComponentInParent<IDamagable>();
                                if (damagable != null)
                {
                    damagable.TakeDamage(knightKen.GetAttackDamage());
                    AudioManager.Instance.PlayHit();
                }
            }
        }
    }
    


public void UseUltimate()
    {
        var colliders = GetEnemiesInHitbox();
        if (colliders == null) return;

        foreach (var col in colliders)
        {
            if (col.CompareTag("Hitbox"))
            {
                float duration = knightKen.ultimateSkill.StunDuration;
                var damagable = col.GetComponentInParent<IDamagable>();
                                if (damagable != null)
                {
                    damagable.TakeDamage(knightKen.GetAttackDamage());
                    AudioManager.Instance.PlayHit();
                }
                var buffable = col.GetComponentInParent<IBuffable>();
                if (buffable != null)
                {
                    buffable.BuffManager.ApplyBuff(new StunDebuff(duration));
                }
            }
        }
    }

    public IEnumerator ResetKnockback(Collider2D enemy, float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        if (enemy.TryGetComponent<IStunable>(out IStunable stunable))
        {
            stunable.IsStunned = false;
        }
    }
    public void OutBlock()
    {
        KnightKen knightKen = attackHitbox.GetComponentInParent<KnightKen>();
        if (knightKen != null)
        {
            knightKen.IsBlocking = false;
            Debug.Log("KnightKen is no longer blocking.");
        }
    }
    private Collider2D[] GetEnemiesInHitbox()
    {
        if (attackHitbox == null) return null;

        BoxCollider2D hitboxCollider = attackHitbox.GetComponent<BoxCollider2D>();
        if (hitboxCollider == null)
        {
            Debug.LogWarning("Attack hitbox does not have a BoxCollider2D component.");
            return null;
        }

        return Physics2D.OverlapBoxAll(
            hitboxCollider.bounds.center,
            hitboxCollider.bounds.size,
            hitboxCollider.transform.eulerAngles.z
        );
    }



}
    

