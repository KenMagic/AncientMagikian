using UnityEngine;
public interface ISkill
{
    string SkillName { get; }
    float Cooldown { get; }
    void Activate(GameObject Owner, GameObject target);
    void Deactivate();
    bool IsOnCooldown { get; }
}