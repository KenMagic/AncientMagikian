using System;
using System.Collections;
using UnityEngine;

public class OrcRiderBossKen : MonoBehaviour, IDamagable, IStunable, IBuffable
{
    public EnemySO enemyData;
    public BuffManager buffManager; // Reference to the buff manager
    Animator animator;
    StateMachine stateMachine;

    public BuffManager BuffManager => buffManager;


    [SerializeField]
    public GameObject target;

    public GameObject bolahPrefab; // Prefab for the bolah attack

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
                                 // Flag to check if the target is in attack range
    public bool isInAttackRange = false;

    private bool _bolahHit;
    public bool bolahHit
    {
        get => _bolahHit;
        set
        {
            if (!_bolahHit && value)
            {
                StartCoroutine(ResetBolahHit());
            }
            _bolahHit = value;
        }
    }

    private IEnumerator ResetBolahHit(float delay = 3f)
    {
        yield return new WaitForSeconds(delay);
        _bolahHit = false;
    }
    public bool IsStunned { get; set; }

    //cooldown timer
    private float attackCooldown = 0f; // Time in seconds

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
        idleState = new ORBKIdleState(animator);
        attackState = new ORBKAttackState(animator);
        ultimateState = new ORBKUltimateState(animator);
        abilityState = new ORBKAbilityState(animator);
        moveState = new ORBKMoveState(animator);

        //assign target
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
        if (IsStunned)
        {
            return; // Skip further updates if stunned
        }
        if (!isAttacking)
        {
            Debug.Log("OrcRiderBossKen is not attacking, cooldown: " + attackCooldown + ", abilityCooldown: " + abilityCooldown + ", ultimateCooldown: " + ultimateCooldown);
            if (ultimateCooldown <= 0f)
                {

                    Debug.Log("Using Ultimate!");
                    ultimateCooldown = 10f; // Reset ultimate cooldown
                    ThrowBolah(); // Perform bolah attack

                }
            if (isInAttackRange)
            {
                if (bolahHit)
                {
                    stateMachine.SetState(ultimateState);
                    return;
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

    public void ApplyBuff(IBuff buff)
    {
        buffManager.ApplyBuff(buff);
    }

    public void RemoveBuff(IBuff buff)
    {
        buffManager.RemoveBuff(buff);
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

    //THROW BOLAH
    public void ThrowBolah()
    {
        if (bolahPrefab != null && target != null)
        {
            GameObject bolah = Instantiate(bolahPrefab, transform.position, Quaternion.identity);
            bolah.GetComponent<BolahAttack>().PerformBolahAttack(target, this.gameObject);
        }
        else
        {
            Debug.LogWarning("Bolah prefab or target is not set.");
        }
    }
}
