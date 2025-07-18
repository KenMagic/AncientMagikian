using UnityEngine;
public class KnightKenAbilitySkill : ISkill
{
    public string SkillName => "KnightKenAbilitySkill";
    public float Cooldown { get; private set; }
    public int Level { get; private set; } = 1;

    public float abilityDamage = 10f;

    public KnightKenAbilitySkill(float cooldown, int level)
    {
        Cooldown = cooldown;
        Level = level;
    }

    public void Activate(GameObject owner, GameObject target)
    {
        
    }

    public void Deactivate()
    {

    }

    public void Upgrade()
    {
        Level++;
        abilityDamage *= 1.1f; // Increase damage by 10%
    }
}