using UnityEngine;

public class ORBKAnimationEvent : MonoBehaviour
{
    private OrcRiderBossKen orcRiderBossKen;

    void Start()
    {
        orcRiderBossKen = GetComponent<OrcRiderBossKen>();
    }

    // Animation event for attack hitbox
    public void Reset()
    {
        orcRiderBossKen.isAttacking = false; // Reset attacking state
    }

    // Animation event for ability
    // Gọi từ Animation Event
    public void PerformAbility()
    {
        orcRiderBossKen.isAttacking = true; // Set attacking state
        GameObject target = orcRiderBossKen.target;
        if (target != null && target.TryGetComponent<IDamagable>(out var damagable))
        {
            target.TryGetComponent<IBlockable>(out var blockable);
            if (blockable != null && blockable.IsBlocking)
            {
                return;
            }
            damagable.TakeDamage(20f);
        }
    }

    // aplly bleed debuff
    public void ApplyBleedDebuff()
    {
        GameObject target = orcRiderBossKen.target;
        if (target != null && target.TryGetComponent<IBuffable>(out var buffable))
        {
            var bleedDebuff = new BleedDebuff();
            buffable.BuffManager.ApplyBuff(bleedDebuff);
        }
    }

    public void PerformAttack()
    {
        orcRiderBossKen.isAttacking = true; // Set attacking state
        GameObject target = orcRiderBossKen.target;
        if (target != null)
        {
            IDamagable damagable = target.GetComponent<IDamagable>();
            if (damagable != null)
            {
                target.TryGetComponent<IBlockable>(out var blockable);
                if (blockable != null && blockable.IsBlocking)
                {
                    return;
                }
                damagable.TakeDamage(orcRiderBossKen.enemyData.attackDamage);
            }
        }
    }

    public void PerformUltimate()
    {
        orcRiderBossKen.isAttacking = true; // Set attacking state
        GameObject target = orcRiderBossKen.target;
        if (target != null && target.TryGetComponent<IDamagable>(out var damagable))
        {
            target.TryGetComponent<IBlockable>(out var blockable);
            if (blockable != null && blockable.IsBlocking)
            {
                return;
            }
            var damage = 30f;
            damagable.TakeDamage(damage);
        }
    }
}
