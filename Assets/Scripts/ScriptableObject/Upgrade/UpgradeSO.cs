using UnityEngine;


public abstract class UpgradeSO : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;
    public UpgradeType upgradeType;

    public enum UpgradeType
    {
        Common, Rare, Epic, Legendary
    }

    public abstract void ApplyUpgrade(GameObject target);
}