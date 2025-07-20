
using System.Collections;
using TMPro;
using UnityEngine;

public class KnightKen : MonoBehaviour, IDamagable, IBlockable, IPlayerUpgrade, IBuffable, IStunable
{
    [SerializeField]
    private PlayerStatsSO playerData; // Reference to the player stats ScriptableObject
    public CharacterStats characterStats; // Reference to the character stats
    private Animator animator;

    private StateMachine stateMachine;
    public BuffManager buffManager;

    IState idleState;
    IState moveState;
    IState attackState;
    IState hurtState; // Placeholder for future implementation
    IState deadState; // Placeholder for future implementation
    IState abilityState; // Placeholder for future implementation
    IState ultimateState; // Placeholder for future implementation

    IState blockState; // Placeholder for future implementation

    public bool isAttacking = false;
    private bool isHurt = false;
    private bool isDead = false;

    private bool isBlocking = false;
    public bool IsBlocking
    {
        get { return isBlocking; }
        set { isBlocking = value; }
    } // Implementing IBlockable interface

    public BuffManager BuffManager
    {
        get { return buffManager; }
        set { buffManager = value; }
    }

    public bool IsStunned { get; set; }

    // buff
    private float attackMultiplier = 1f;
    private float defenseMultiplier = 1f;
    private float moveSpeedMultiplier = 1f;
    private float critChanceMultiplier = 1f;
    private float cooldownMultiplier = 1f;
    private float healthBoostMultiplier = 0f;

    //Skill
    public KnightKenAbilitySkill abilitySkill;
    public KnightKenUltimateSkill ultimateSkill;

    public HealthBar healthBar; // Reference to the health bar UI

    //cooldown
    private float abilityCooldown = 0f;
    private float ultimateCooldown = 0f;


    void Start()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
        buffManager = GetComponent<BuffManager>();
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }

        // Initialize and set up states
        idleState = new KnightKenIdleState(animator);
        moveState = new KnightKenMoveState(animator);
        attackState = new KnightKenAttackState(animator);
        abilityState = new KnightKenAbilityState(animator);
        ultimateState = new KnightKenUltimateState(animator);
        blockState = new KnightKenBlockState(animator);

        // Set initial health and cooldowns


        characterStats = new CharacterStats(playerData);
        //Set up skill
        abilitySkill = new KnightKenAbilitySkill(characterStats.CurrentSkillCooldown, 1);
        ultimateSkill = new KnightKenUltimateSkill();
        animator.SetBool("isMoving", false);
        stateMachine.SetState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            animator.SetTrigger("Death");
            return; // Skip further updates if dead
        }
        if (isHurt)
        {
            return;
        }
        if (IsStunned)
        {
            return;
        }
        ultimateCooldown -= Time.deltaTime;
        abilityCooldown -= Time.deltaTime;
        if (!isAttacking && !IsBlocking)
        {

            if (Input.GetMouseButtonDown(0)) // Example for attack input
            {
                isAttacking = true;
                stateMachine.SetState(attackState);
            }
            else if (Input.GetMouseButtonDown(1)) // Example for block input
            {
                IsBlocking = true;
                stateMachine.SetState(blockState);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && ultimateCooldown <= 0) // Example for ability input
            {
                isAttacking = true;
                ultimateCooldown = ultimateSkill.Cooldown;
                stateMachine.SetState(ultimateState);
            }
            else if (Input.GetKeyDown(KeyCode.E) && abilityCooldown <= 0) // Example for ultimate input
            {
                isAttacking = true;
                abilityCooldown = abilitySkill.Cooldown;
                stateMachine.SetState(abilityState);
            }
            // Handle movement input and state transitions here
            else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                stateMachine.SetState(moveState);
            }


            else
            {
                stateMachine.SetState(idleState);
            }
        }

        stateMachine.Update();
    }

    public void TakeDamage(float damage)
    {
        if (!IsBlocking)
        {
            if (characterStats.CurrentHealth <= 0 || characterStats.CurrentHealth - damage <= 0)
            {
                characterStats.CurrentHealth = 0; // Ensure health doesn't go below zero
                isDead = true;
                StartCoroutine(EndGame(1f));
            }
            else
            {
                characterStats.CurrentHealth -= damage;
                if (healthBar != null)
                {
                    healthBar.UpdateHealthBar(characterStats.CurrentHealth, characterStats.MaxHealth + healthBoostMultiplier);
                }
                StartCoroutine(HurtRoutine());
            }
        }
        else
        {
            Debug.Log("KnightKen blocked the attack.");
        }

    }
    private IEnumerator HurtRoutine()
    {
        isHurt = true;
        animator.SetTrigger("Hurt");

        float hurtDuration = 0.5f;
        yield return new WaitForSeconds(hurtDuration);
        isAttacking = false; // Reset attacking state after hurt
        isBlocking = false; // Reset blocking state after hurt
        isHurt = false;
    }


    //Get player stats
    public float GetAttackDamage()
    {
        return characterStats.CurrentAttack * attackMultiplier;
    }
    public float GetDefense()
    {
        return characterStats.CurrentDefense * defenseMultiplier;
    }
    public float GetMoveSpeed()
    {
        return characterStats.CurrentSpeed * moveSpeedMultiplier;
    }
    public float GetCritChance()
    {
        return characterStats.CurrentCritChance * critChanceMultiplier;
    }
    public float GetSkillCooldown()
    {
        return characterStats.CurrentSkillCooldown * cooldownMultiplier;
    }
    public float GetUltimateCooldown()
    {
        return characterStats.CurrentUltimateCooldown * cooldownMultiplier;
    }
    public float GetMaxHealth()
    {
        return characterStats.CurrentHealth + healthBoostMultiplier;
    }
    // control wasd 
    //Set multiplier methods
    public void AddStatUpgrade(StatType statType, float amount)
    {
        characterStats.UpgradeStats(statType, amount);
    }

    public void SkillUpgrade(string skillName)
    {
        if (skillName == "KnightKenAbilitySkill")
        {
            abilitySkill.Upgrade();
            Debug.Log($"{abilitySkill.SkillName} upgraded to level {abilitySkill.Level}");
        }
        else if (skillName == "KnightKenUltimateSkill")
        {
            ultimateSkill.Upgrade();
            Debug.Log($"{ultimateSkill.SkillName} upgraded to level {ultimateSkill.Level}");
        }
        else
        {
            Debug.LogWarning($"Skill {skillName} not recognized for upgrade.");
        }
    }

    public void ApplyBuff(IBuff buff)
    {
        buffManager.ApplyBuff(buff);
    }
    public void RemoveBuff(IBuff buff)
    {
    }
    private IEnumerator EndGame(float v)
    {
        yield return new WaitForSeconds(v);
        GameController.Instance.GameOver();
    }
}

