using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSO", menuName = "ScriptableObjects/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public string upgradeName;
    public string description;
    public Sprite icon;

    public int extraHealth;
    public float extraDamage;
    public float attackSpeedMultiplier = 1f;
    public float moveSpeedMultiplier = 1f;
}
