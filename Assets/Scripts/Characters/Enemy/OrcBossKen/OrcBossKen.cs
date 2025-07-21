using UnityEngine;

public class OrcBossKen : MonoBehaviour, IDamagable, IStunable, IBuffable
{
    [SerializeField]
    public EnemySO enemyData; // Reference to the enemy stats ScriptableObject
    public BuffManager buffManager; // Reference to the buff manager
    Animator animator;

    public bool IsStunned { get; set; }

    public BuffManager BuffManager => throw new System.NotImplementedException();

    StateMachine stateMachine;

    [SerializeField]
    GameObject target;

    //IState
    IState idleState;
    IState attackState;
    IState ultimateState;
    IState abilityState;
    IState moveState;

    //flag
    public bool isAttacking = false;
    private bool isHurt = false; // Placeholder for future implementation
    private bool isDead = false; // Placeholder for future implementation
    public bool isInAttackRange = false; // Flag to check if the target is in attack range

    //cooldown timer
    private float attackCooldown = 0f; // Time in seconds
    private float attackCooldownTotal = 0f; // Time in seconds

    private float abilityCooldown = 0f; // Time in seconds for ability cooldown
    private float ultimateCooldown = 0f; // Time in seconds for ultimate cooldown

    private float currentHealth;

    //HEALTHBAR
    [SerializeField]
    private HealthBar healthBar;

    void Start()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
        buffManager = GetComponent<BuffManager>();
        healthBar = GetComponentInChildren<HealthBar>();

        // Initialize character stats from the ScriptableObject
        currentHealth = enemyData.health; // Set initial health from enemy data

        // Initialize and set up states
        idleState = new OrcBossKenIdleState(animator);
        attackState = new OrcBossKenAttackState(animator);
        ultimateState = new OrcBossKenUltimateState(animator);
        abilityState = new OrcBossKenAbilityState(animator);
        moveState = new OrcBossKenMoveState(animator);

        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Player");
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Destroy(gameObject, 1f);
            return;
        }
        if (isHurt)
        {
            animator.SetTrigger("Hurt");
            isHurt = false; // Reset hurt state after handling
            isAttacking = false;
            return; // Skip further updates if hurt
        }
        attackCooldown -= Time.deltaTime;
        abilityCooldown -= Time.deltaTime;
        ultimateCooldown -= Time.deltaTime;
        attackCooldownTotal -= Time.deltaTime;
        if (IsStunned)
        {
            return; // Skip further updates if stunned
        }
        if (!isAttacking)
        {
            Debug.Log("OrcBossKen is not attacking, cooldown: " + attackCooldown + ", abilityCooldown: " + abilityCooldown + ", ultimateCooldown: " + ultimateCooldown);

            if (isInAttackRange && attackCooldownTotal<=0)
            {
                attackCooldownTotal = enemyData.attackCooldown;
                if (ultimateCooldown <= 0f
                && target != null
                && target.TryGetComponent<IBuffable>(out var buffable))
                {
                    var bleed = buffable.BuffManager.GetActiveBuffs().Find(b => b is BleedDebuff) as BleedDebuff;
                    if (bleed != null && bleed.StackCount >= 5)
                    {
                        Debug.Log("Using Ultimate!");
                        ultimateCooldown = 10f; // Reset ultimate cooldown
                        stateMachine.SetState(ultimateState);
                        return;
                    }
                }

                if (attackCooldown <= 0f)
                {
                    Debug.Log("Performing Attack!");
                    attackCooldown = 1f;
                    stateMachine.SetState(attackState);
                }
                else if (abilityCooldown <= 0f)
                {
                    Debug.Log("Using Ability!");
                    abilityCooldown = 5f; // Reset ability cooldown
                    stateMachine.SetState(abilityState);
                }
            }
            else
            {
                ChaseTarget();
            }
        }

        stateMachine.Update();

    }

    // Enemy AI methods
    //chase target
    public void ChaseTarget()
    {
        if (target != null)
        {
            stateMachine.SetState(moveState);
        }
        else
        {
            stateMachine.SetState(idleState);
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }

    public void TakeDamage(float damage)
    {
        isHurt = true;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0; // Ensure health doesn't go below zero
            isDead = true; // Set dead flag
            animator.SetTrigger("Death");
            WaveManager.Instance.OnEnemyDied(enemyData.exp);
        }
        // Update health bar
        healthBar.UpdateHealthBar(currentHealth, enemyData.health);

    }

    public void Heal(float amount)
    {

        if (currentHealth + amount > enemyData.health)
        {
            currentHealth = enemyData.health; // Cap health at max value
            healthBar.UpdateHealthBar(currentHealth, enemyData.health);
            return; // Don't allow healing beyond max health
        }
        currentHealth += amount;
        // Update health bar
        healthBar.UpdateHealthBar(currentHealth, enemyData.health);
    }

    public void ApplyBuff(IBuff buff)
    {
        buffManager.ApplyBuff(buff);
    }

    public void RemoveBuff(IBuff buff)
    {
        buffManager.RemoveBuff(buff);
    }
}
