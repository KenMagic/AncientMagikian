using System.Collections;
using UnityEngine;

public class Orc : MonoBehaviour, IDamagable
{
    [SerializeField] public EnemySO enemyData;
    [SerializeField] private Animator animator;

    public StateMachine stateMachine;
    public Transform towerTarget;
    public Transform target;
    private Coroutine forgetTargetCoroutine;

    IState attackState;
    IState moveState;
    IState hurtState;

    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Tower")?.transform;
        float currentHealth = enemyData.health;

        attackState = new OrcAttackState(animator, this, enemyData.attackCooldown, "isAttackTower");
        moveState = new OrcMoveState(animator, this, target);
        hurtState = new OrcHurtState(animator, this);

    }

    void Start()
    {
        if (target == null || stateMachine == null || animator == null)
        {
            Debug.LogError("Orc missing essential components");
            return;
        }

        towerTarget = GameObject.FindGameObjectWithTag("Tower")?.transform;
        if (towerTarget == null)
        {
            Debug.LogError("Tower target not found");
            return;
        }

        stateMachine.SetState(moveState);
    }

    void Update()
    {
        stateMachine.Update();
    }


    public void DealDamage(GameObject gameObject)
    {
        gameObject.GetComponent<IDamagable>()?.TakeDamage(enemyData.attackDamage);
    }

    public void SetAttackState(string type)
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

    private IEnumerator HideCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(this); // hoặc Destroy(gameObject);
    }

    private IEnumerator ForgetTargetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Orc quay lại Tower vì player biến mất.");
        ResetTargetToTower();
    }

    public void TakeDamage(float damage)
    {
        enemyData.health -= damage;
        Debug.Log($"Orc nhận {damage} sát thương. Máu còn lại: {enemyData.health}");
        if (enemyData.health <= 0)
        {
            Debug.Log("Orc đã chết!");
            animator.SetTrigger("isDeath");
            HideAfterDelay(1f);
        }
        else
        {
            SetHurtState();
        }
    }
}


