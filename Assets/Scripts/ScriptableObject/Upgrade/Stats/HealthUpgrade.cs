using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "ScriptableObjects/Upgrades/HealthUpgrade")]
public class HealthUpgrade : UpgradeSO
{
    [SerializeField] private float healthBoost = 20f;

    public override void Apply(GameObject target)
    {
        if (target.TryGetComponent<IPlayerUpgrade>(out var player))
        {
            player.AddStatUpgrade(StatType.Health, healthBoost);
            Debug.Log($"Applied {UpgradeName} to {target.name}, boosting health by {healthBoost}");
        }
        else
        {
            Debug.LogWarning($"Target {target.name} does not have a IPlayerUpgrade component.");
        }
    }
}