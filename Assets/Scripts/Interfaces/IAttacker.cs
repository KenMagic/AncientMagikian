public interface IAttacker{
    float AttackDamage { get; }
    float AttackRange { get; }
    void Attack(IDamagable target);
}