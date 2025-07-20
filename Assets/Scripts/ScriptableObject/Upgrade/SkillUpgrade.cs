using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Upgrade/Skill Upgrade")]
public class SkillUpgrade : UpgradeSO
{
    [SerializeField] private string skillName;
    public override void Apply(GameObject target)
    {
        if (target.TryGetComponent<IPlayerUpgrade>(out var player))
        {
            player.SkillUpgrade(skillName);
        }
    }
}
