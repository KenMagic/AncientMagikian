using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour, IDamagable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] public EnemySO enemyData;
    [SerializeField] private Animator animator;

    public StateMachine stateMachine;
    public Transform towerTarget;
    public Transform target;
    private Coroutine forgetTargetCoroutine;
    private float currentHealth;
    public HealthBar healthBar;



    IState attackState;
    IState moveState;
    IState hurtState;
    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Tower")?.transform;
        healthBar = GetComponentInChildren<HealthBar>();

        currentHealth = enemyData.health;

        attackState = new SlimeAttackState(animator, this, enemyData.attackCooldown);
        moveState = new SlimeMoveState(animator, this, target);
        hurtState = new SlimeHurtState(animator, this);
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
        stateMachine.Update();
    }

    public void SetAttackState()
    {
        stateMachine.SetState(attackState);
    }

    public void SetMoveState()
    {
        stateMachine.SetState(moveState);
    }

    public void SetHurtState()
    {
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

    public void ResetStatus()
    {
        currentHealth = enemyData.health;
    }
    public void DealDamage(GameObject gameObject)
    {
        gameObject.GetComponent<IDamagable>()?.TakeDamage(enemyData.attackDamage);
    }
    public void Suprise(GameObject gameObject, float damege)
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
        currentHealth -= damage;
        SetHurtState();
        healthBar.UpdateHealthBar(currentHealth, enemyData.health);
    }

    #region private methods
    private IEnumerator HideCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this);
    }

    private IEnumerator ForgetTargetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetTargetToTower();
    }
    #endregion
}
