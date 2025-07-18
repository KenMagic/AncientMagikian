using UnityEngine;
public interface ISkill
{
    string SkillName { get; }
    float Cooldown { get; }
    int Level { get; }
    void Activate(GameObject Owner, GameObject target);
    void Deactivate();
    void Upgrade();
}