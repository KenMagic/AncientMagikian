using UnityEngine;
public enum BuffEffectType
{
    StatModifier,
    HealOverTime,
    DamageOverTime,
    Stun,
    Custom // dùng để thực hiện logic riêng
}

public interface IBuff
{
    string BuffName { get; }
    BuffEffectType EffectType { get; }
    float Duration { get; }
    void Apply(GameObject target);
    void Remove(GameObject target);
}