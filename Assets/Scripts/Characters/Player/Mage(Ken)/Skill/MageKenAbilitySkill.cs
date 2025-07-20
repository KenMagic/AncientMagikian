using UnityEngine;
public class MageKenAbilitySkill : ISkill
{
    public string SkillName => "MageKenAbilitySkill";
    public float Cooldown { get; private set; }
    public int Level { get; private set; } = 1;

    public float duration = 5f;
    public int blockCount = 1;

    public MageKenAbilitySkill(float cooldown, int level)
    {
        Cooldown = cooldown;
        Level = level;
    }

    public void Activate(GameObject owner, GameObject target)
    {
        MageKen mg = owner.GetComponent<MageKen>();
        bool IsNoCoolDown = mg.IsNoCoolDown;
        if (!IsNoCoolDown)
        {
            if (mg.transform.GetComponentInChildren<MageKenShield>() == null)
            {
                Debug.Log("Spawn shield");
                mg.Shield();
            }
            mg.abilityCooldown = mg.abilitySkill.Cooldown; // Reset cooldown on no buff
            mg.isBlocking = true;
            if (blockCount < mg.abilitySkill.blockCount)
            {
                blockCount = mg.abilitySkill.blockCount; // Set block count to the skill's block count
            }
        }
    }

    public void Deactivate()
    {

    }

    public void Upgrade()
    {
        Level++;
        duration *= 1.1f; // Increase duration by 10%
        blockCount += 1; // Increase block count by 1
    }
}