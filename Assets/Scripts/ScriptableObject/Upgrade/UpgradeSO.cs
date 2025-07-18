using UnityEngine;


public abstract class UpgradeSO : ScriptableObject, IUpgradable
{
    [SerializeField] private string upgradeName;
    [TextArea]
    [SerializeField] private string description;

    public string UpgradeName => upgradeName;
    public string Description => description;
    public Sprite icon;
    public UpgradeType upgradeType;

    public enum UpgradeType
    {
        Common, Rare, Epic, Legendary
    }

    public abstract void Apply(GameObject target);

}