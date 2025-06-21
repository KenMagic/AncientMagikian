using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "ScriptableObjects/Upgrades/HealthUpgrade")]
public class HealthUpgrade : UpgradeSO
{
    [SerializeField] private float healthBoost = 20f;

    public override void ApplyUpgrade(GameObject target)
    {
        if (target.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            // Assuming the IDamagable interface has a method to increase health
            // This is a placeholder; the actual implementation may vary
            damagable.TakeDamage(-healthBoost);
            Debug.Log($"Applied health upgrade: {healthBoost} to {target.name}");
        }
        else
        {
            Debug.LogWarning($"Target {target.name} does not implement IDamagable interface.");
        }
    }
}