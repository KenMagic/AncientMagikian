using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade")]
public class Upgrade : UpgradeSO
{
    public override void ApplyUpgrade(GameObject target)
    {
        // Implement the logic to apply the upgrade to the target GameObject
        // This could involve modifying properties, adding components, etc.
        Debug.Log($"Applying upgrade: {name} to {target.name}");
        
        // Example: Increase the target's speed or health
        // target.GetComponent<SomeComponent>().IncreaseSpeed(speedBoost);
    }
}
