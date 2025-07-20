using UnityEngine;

[CreateAssetMenu(fileName = "EnemySO", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public float health;
    public float speed;
    public float attackDamage;
    public float attackCooldown;
    public float exp;
}