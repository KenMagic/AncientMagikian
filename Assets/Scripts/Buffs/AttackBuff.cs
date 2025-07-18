using UnityEngine;
public class AttackBuff : IBuff
{
    public string BuffName => "Attack Buff";

    public BuffEffectType EffectType => BuffEffectType.StatModifier;

    public float Duration => 5f;

    public void Apply(GameObject target)
    {
        target.TryGetComponent<IPlayerUpgrade>(out var player);
        if (player != null)
        {
            player.AddStatUpgrade(StatType.Attack, 0.2f); // Increase attack by 20%
        }
        else
        {
            Debug.LogWarning("Target does not implement IPlayerUpgrade.");
        }
    }

    public void Remove(GameObject target)
    {
        target.TryGetComponent<IPlayerUpgrade>(out var player);
        if (player != null)
        {
            player.AddStatUpgrade(StatType.Attack, -0.2f); // Revert attack increase by 20%
        }
        else
        {
            Debug.LogWarning("Target does not implement IPlayerUpgrade.");
        }
    }
}