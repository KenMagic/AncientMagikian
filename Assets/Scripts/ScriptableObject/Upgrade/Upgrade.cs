using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade/Stat Upgrade")]
public class Upgrade : UpgradeSO
{
    [SerializeField] private StatType statToUpgrade;
    [SerializeField] private float value = 0.1f;


    public override void Apply(GameObject target)
    {
        if (target.TryGetComponent<IPlayerUpgrade>(out var player))
        {
            player.AddStatUpgrade(statToUpgrade, value);
        }
    }
}
