using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerStats", menuName = "ScriptableObjects/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    [Header("Base Stats")]
    public float maxHealth = 100f;
    public float moveSpeed = 5f;
    public float damage = 10f;
    public float attackSpeed = 1f;
    public float critChance = 0.1f;
    public float defense = 5f;

    [Header("Ability")]
    public float skillCooldown = 5f;
    public float ultimateCooldown = 10f;

    [Header("Other")]
    public string characterName = "Knight Ken";
    public Sprite portrait;
}
