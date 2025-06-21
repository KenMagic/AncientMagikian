public interface IUpgradeEffect
{
    /// <summary>
    /// Applies the upgrade effect to the target.
    /// </summary>
    /// <param name="target">The target to apply the upgrade effect to.</param>
    void Apply(IUpgradable target);

    /// <summary>
    /// Reverts the upgrade effect from the target.
    /// </summary>
    /// <param name="target">The target to revert the upgrade effect from.</param>
    void Revert(IUpgradable target);
}