using UnityEngine;
public class MageKenUltimateSkill : ISkill
{
    public string SkillName => "Mage Ken's Ultimate Skill";

    public float Cooldown { get; private set; } = 30f;
    public int Level { get; private set; } = 1;

    public float Duration { get; private set; } = 5f; // Duration of the ultimate skill effect

    public MageKenUltimateSkill()
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
        Duration *= 1.2f; // Increase duration by 20%
    }
}