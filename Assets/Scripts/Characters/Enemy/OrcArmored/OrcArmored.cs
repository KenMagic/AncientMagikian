using System.Collections;
using UnityEngine;

public class OrcArmored : MonoBehaviour, IDamagable
{
    [SerializeField] public EnemySO enemyData;
    [SerializeField] private Animator animator;

    public StateMachine stateMachine;
    public Transform towerTarget;
    public Transform target;
    private Coroutine forgetTargetCoroutine;
    public float currentHealth;
    private HealthBar healthBar;

    IState attackState;
    IState moveState;
    IState hurtState;
    
    public bool isDeath = false;
    
    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Tower")?.transform;
        healthBar = GetComponentInChildren<HealthBar>();
        currentHealth = enemyData.health;

        attackState = new OrcArmoredAttackState(this, animator, enemyData.attackCooldown);
        moveState = new OrcArmoredMoveState(animator, this, target);
        hurtState = new OrcArmoredHurtState(animator, this);
    }

    void Start()
    {
        if (target == null || stateMachine == null || animator == null)
        {
            return;
        }

        towerTarget = GameObject.FindGameObjectWithTag("Tower")?.transform;
        if (towerTarget == null)
        {
            return;
        }

        stateMachine.SetState(moveState);
    }

    void Update()
    {
        if (isDeath) return;
        stateMachine.Update();
    }

    public void SetAttackState()
    {
        if (isDeath) return;
        stateMachine.SetState(attackState);
    }

    public void SetMoveState()
    {
        if (isDeath) return;
        stateMachine.SetState(moveState);
    }

    public void SetHurtState()
    {
        if (isDeath) return;
        stateMachine.SetState(hurtState);
    }

    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget;
        SetMoveState();
    }

    public void ResetTargetToTower()
    {
        target = towerTarget;
        SetMoveState();
    }

    public void DealDamage(GameObject gameObject)
    {
        gameObject.GetComponent<IDamagable>()?.TakeDamage(enemyData.attackDamage);
    }
    public void Skill(GameObject gameObject, float damege)
    {
        gameObject.GetComponent<IDamagable>()?.TakeDamage(damege);
    }

    public void StartForgetTargetCoroutine(float delay)
    {
        if (!gameObject.activeInHierarchy) return;

        if (forgetTargetCoroutine != null)
            StopCoroutine(forgetTargetCoroutine);

        forgetTargetCoroutine = StartCoroutine(ForgetTargetAfterDelay(delay));
    }
    public void HideAfterDelay(float delay)
    {
        StartCoroutine(HideCoroutine(delay));
    }

    public void TakeDamage(float damage)
    {
        int chance = Random.Range(0, 100);
        if (chance < 35)
        {
            Debug.LogWarning("chance" + chance);
            animator.SetTrigger("isBlock");
        }
        else
        {
            currentHealth -= damage;
            SetHurtState();
            healthBar.UpdateHealthBar(currentHealth, enemyData.health);
        }
    }

    public void ResetStatus()
    {
        isDeath = false;
        currentHealth = enemyData.health;
    }

    #region private methods
    private IEnumerator HideCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
    }

    private IEnumerator ForgetTargetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetTargetToTower();
    }
    #endregion
}
