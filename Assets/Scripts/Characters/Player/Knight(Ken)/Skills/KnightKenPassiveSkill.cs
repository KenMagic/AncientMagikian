using UnityEngine;
public class KnightKenPassiveSkill : ISkill
{
    public string SkillName => "KnightKenPassiveSkill";
    public float Cooldown { get; private set; }
    public int Level { get; private set; } = 1;

    public float healAmount = 20f;

    public KnightKenPassiveSkill()
    {

    }

    public void Activate(GameObject owner, GameObject target)
    {
        owner.GetComponent<KnightKen>().Heal(healAmount);
    }

    public void Deactivate()
    {

    }

    public void Upgrade()
    {
        Level++;
        healAmount *= 10f; // Increase damage by 10%
    }
}