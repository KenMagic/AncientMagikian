using UnityEngine;
public class KnightKenUltimateSkill : ISkill
{
    public string SkillName => "KnightKenAbilitySkill";

    public float Cooldown { get; private set; } = 30f;
    public int Level { get; private set; } = 1;

    public float StunDuration { get; private set; } = 2f; // Duration of the stun effect

    public float ultimateDamage = 50f; // Base damage of the ultimate skill

    public KnightKenUltimateSkill()
    {

    }

    public void Activate(GameObject Owner, GameObject target)
    {
    }

    public void Deactivate()
    {
    }

    public void Upgrade()
    {
        Level++;
        StunDuration *= 1.2f; // Increase stun duration by 20%
        ultimateDamage *= 1.1f;
        Debug.Log($"{SkillName} upgraded to level {Level}. Stun duration is now {StunDuration} seconds.");
    }
}