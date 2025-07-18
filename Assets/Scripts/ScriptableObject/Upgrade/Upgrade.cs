using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade")]
public class Upgrade : UpgradeSO
{
    [SerializeField] private float damageMultiplier = 0.1f;

    public override void Apply(GameObject target)
    {
        if (target.TryGetComponent<IPlayerUpgrade>(out var player))
        {
            player.AddStatUpgrade(StatType.Attack, damageMultiplier);
        }
    }
}
