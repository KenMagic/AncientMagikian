//FOR STATS RUNTIME
using UnityEngine;
public class CharacterStats
{
    public PlayerStatsSO baseStats;

    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    public float CurrentAttack { get; set; }
    public float CurrentDefense { get; set; }
    public float CurrentSpeed { get; set; }
    public float CurrentCritChance { get; set; }
    public float CurrentSkillCooldown { get; set; }
    public float CurrentUltimateCooldown { get; set; }
    public float CurrentAttackSpeed { get; set; }

    public CharacterStats(PlayerStatsSO baseStats)
    {
        this.baseStats = baseStats;
        CurrentHealth = baseStats.maxHealth;
        MaxHealth = baseStats.maxHealth;
        CurrentAttack = baseStats.damage;
        CurrentDefense = baseStats.defense;
        CurrentSpeed = baseStats.moveSpeed;
        CurrentCritChance = baseStats.critChance;
        CurrentSkillCooldown = baseStats.skillCooldown;
        CurrentUltimateCooldown = baseStats.ultimateCooldown;
        CurrentAttackSpeed = 0f;
    }

    //UPGRADE METHODS
    public void UpgradeStats(StatType statType, float amount)
    {
        switch (statType)
        {
            case StatType.Attack:
                CurrentAttack += amount;
                break;
            case StatType.AttackSpeed:
                CurrentAttackSpeed += amount;
                break;
            case StatType.Speed:
                CurrentSpeed += amount;
                break;
            case StatType.Health:
                MaxHealth += amount;
                CurrentHealth += amount;
                break;
        }
    }

}
