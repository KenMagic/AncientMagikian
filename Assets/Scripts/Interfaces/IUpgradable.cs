using UnityEngine;
public interface IUpgradable
{
    string UpgradeName { get; }
    string Description { get; }
    void Apply(GameObject target);
}