using UnityEngine;

public class OrcBossKenAnimationEvent : MonoBehaviour
{
    [SerializeField]
    private GameObject healEffectPrefab; // Prefab for the healing effect
    [SerializeField]
    private GameObject cursedEffectPrefab; // Prefab for the cursed effect
    private OrcBossKen orcBossKen;

    void Start()
    {
        orcBossKen = GetComponent<OrcBossKen>();
    }

    // Animation event for attack hitbox
    public void Reset()
    {
        orcBossKen.isAttacking = false; // Reset attacking state
    }

    // Animation event for ability
    // Gọi từ Animation Event
    public void PerformAbility()
    {
        orcBossKen.isAttacking = true; // Set attacking state
        GameObject target = orcBossKen.GetTarget();
        if (target != null && target.TryGetComponent<IDamagable>(out var damagable))
        {
            target.TryGetComponent<IBlockable>(out var blockable);
            if (blockable != null && blockable.IsBlocking)
            {
                return;
            }
            target.TryGetComponent<IBuffable>(out var buffable);
            ApplyBleedDebuff();
            damagable.TakeDamage(20f);
            orcBossKen.Heal(20f);
            SpawnHealEffect();
        }
    }

    public void SpawnHealEffect()
    {
        if (healEffectPrefab != null)
        {
            GameObject healEffect = Instantiate(healEffectPrefab, orcBossKen.transform.position, Quaternion.identity);
            healEffect.transform.SetParent(orcBossKen.transform); // Set the parent to the orc boss
            Destroy(healEffect, 2f); // Destroy the effect after 2 seconds
        }
    }
    public void SpawnCursedEffect()
    {
        if (cursedEffectPrefab != null)
        {
            Vector3 spawnPosition = orcBossKen.transform.position + new Vector3(0f, 1f, 0f);
            GameObject cursedEffect = Instantiate(cursedEffectPrefab, spawnPosition, Quaternion.identity);
            cursedEffect.transform.SetParent(orcBossKen.transform); // Set the parent to the orc boss
            Destroy(cursedEffect, 1f); // Destroy the effect after 1 seconds
        }
    }

    // aplly bleed debuff
    public void ApplyBleedDebuff()
    {
        GameObject target = orcBossKen.GetTarget();
        if (target != null && target.TryGetComponent<IBuffable>(out var buffable))
        {
            var bleedDebuff = new BleedDebuff();
            buffable.BuffManager.ApplyBuff(bleedDebuff);
        }
    }

    public void PerformAttack()
    {
        orcBossKen.isAttacking = true; // Set attacking state
        GameObject target = orcBossKen.GetTarget();
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
                ApplyBleedDebuff();
                damagable.TakeDamage(orcBossKen.enemyData.attackDamage);
            }
        }
    }

    public void PerformUltimate()
    {
        orcBossKen.isAttacking = true; // Set attacking state
        GameObject target = orcBossKen.GetTarget();
        if (target != null && target.TryGetComponent<IDamagable>(out var damagable))
        {
            target.TryGetComponent<IBlockable>(out var blockable);
            if (blockable != null && blockable.IsBlocking)
            {
                return;
            }
            ApplyBleedDebuff();
            var damage = 50f; // Example damage value for ultimate
            if(target.TryGetComponent<IBuffable>(out var buffable))
            {
                //check if contain bleed debuff
                var bleedDebuff = buffable.BuffManager.GetActiveBuffs().Find(b => b is BleedDebuff);
                if (bleedDebuff != null)
                {
                    damage += (bleedDebuff as BleedDebuff).StackCount * 10f;
                    Debug.LogWarning($"OrcBossKen applied bleed debuff with {((BleedDebuff)bleedDebuff).StackCount} stacks, increasing damage to {damage}.");
                    SpawnCursedEffect();
                    buffable.BuffManager.RemoveBuff(bleedDebuff);
                }
            }
            damagable.TakeDamage(damage);
        }
    }
}
