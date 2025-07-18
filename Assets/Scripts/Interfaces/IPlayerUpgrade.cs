using UnityEngine;
public enum StatType
{
    Attack,
    Defense,
    Speed,
    Health
}

public interface IPlayerUpgrade
{
    void AddStatUpgrade(StatType statType, float amount);
    void SkillUpgrade(string skillName);
}
