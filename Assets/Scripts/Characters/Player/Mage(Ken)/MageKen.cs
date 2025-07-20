
using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class MageKen : MonoBehaviour, IDamagable, IBlockable, IPlayerUpgrade, IBuffable, INoCD, IStunable
{
    [SerializeField]
    private PlayerStatsSO playerData; // Reference to the player stats ScriptableObject
    public CharacterStats characterStats; // Reference to the character stats
    public Animator animator;

    private StateMachine stateMachine;
    public BuffManager buffManager;

    IState idleState;
    IState moveState;
    IState attackState;
    IState hurtState; // Placeholder for future implementation
    IState deadState; // Placeholder for future implementation
    IState abilityState; // Placeholder for future implementation
    IState ultimateState; // Placeholder for future implementation

    public bool isUltimate = false;
    public bool isAttacking = false;
    private bool isHurt = false;
    private bool isDead = false;

    public bool isBlocking = false;
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

    public bool IsNoCoolDown { get; set; } = false;

    // buff
    private float attackMultiplier = 1f;
    private float defenseMultiplier = 1f;
    private float moveSpeedMultiplier = 1f;
    private float critChanceMultiplier = 1f;
    private float cooldownMultiplier = 1f;
    private float healthBoostMultiplier = 0f;

    //Skill
    public MageKenAbilitySkill abilitySkill;
    public MageKenUltimateSkill ultimateSkill;
    public MageKenSpecialAttackSkill spec;

    public HealthBar healthBar; // Reference to the health bar UI

    //cooldown
    public float abilityCooldown = 0f;
    public float ultimateCooldown = 0f;
    public float specCooldown = 0f;
    private int blockCount = 0;


    //Projectile prefab
    public GameObject shieldPrefab;
    public GameObject bulletPrefab;


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
        idleState = new MageKenIdleState(this);
        moveState = new MageKenMoveState(this);
        attackState = new MageKenAttackState(this);
        abilityState = new MageKenAbilityState(this);
        ultimateState = new MageKenUltimateState(this);

        // Set initial health and cooldowns


        characterStats = new CharacterStats(playerData);
        //Set up skill
        abilitySkill = new MageKenAbilitySkill(characterStats.CurrentSkillCooldown, 1);
        ultimateSkill = new MageKenUltimateSkill();
        spec = new MageKenSpecialAttackSkill();
        animator.SetBool("isMoving", false);
        stateMachine.SetState(idleState);
    }

    // Update is called once per frame
    void Update()
    {
        isUltimate = IsNoCoolDown;
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
        specCooldown -= Time.deltaTime;
        if (!isAttacking)
        {

            if (Input.GetMouseButtonDown(0)) // Example for attack input
            {
                isAttacking = true;
                ShootNormal();
                stateMachine.SetState(attackState);
            }
            if ( Input.GetMouseButtonDown(1)  && IsNoCoolDown || Input.GetMouseButtonDown(1)  && specCooldown <= 0) // Example for attack input
            {
                specCooldown = spec.Cooldown;
                isAttacking = true;
                spec.Activate(this.gameObject, null);
                stateMachine.SetState(attackState);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && ultimateCooldown <= 0) // Example for ability input
            {
                ultimateCooldown = ultimateSkill.Cooldown;
                stateMachine.SetState(ultimateState);
            }
            else if ( Input.GetKeyDown(KeyCode.E) && IsNoCoolDown || Input.GetKeyDown(KeyCode.E) && abilityCooldown <= 0) // Example for ultimate input
            {
                abilitySkill.Activate(this.gameObject, null);
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
            MageKenShield shield = GetComponentInChildren<MageKenShield>();
            shield.BreakShield();
            blockCount--;
            if (blockCount == 0)
            {
                isBlocking = false;
                shield.RemoveShield();
            }

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
    void ShootNormal()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        Vector2 direction = (mousePos - this.transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);

        BulletControllerKen bulletController = bullet.GetComponent<BulletControllerKen>();
        if (bulletController != null)
        {
            bulletController.SetDirection(direction);
            bulletController.SetDamage(characterStats.CurrentAttack);
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    public void Shield()
    {
        GameObject shield = Instantiate(shieldPrefab, this.transform.position, Quaternion.identity);
        shield.transform.SetParent(this.transform);
    }
    
}

