using System.Collections;
using UnityEngine;

public class SkeletonArcher : MonoBehaviour, IDamagable
{
    [SerializeField] public EnemySO enemyData;
    [SerializeField] private Animator animator;
    public GameObject arrowPrefab;

    public StateMachine stateMachine;
    public Transform towerTarget;
    public Transform target;
    public Transform arrowShoter;
    private Coroutine forgetTargetCoroutine;

    IState attackState;
    IState moveState;
    IState hurtState;

    private void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Tower")?.transform;
        float currentHealth = enemyData.health;

        attackState = new SkeletonArcherAttackState(animator, this, enemyData.attackCooldown);
        moveState = new SkeletonArcherMoveState(animator, this, target);
        hurtState = new SkeletonArcherHurtState(animator, this);
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

    public void TakeDamage(float damage)
    {
        enemyData.health -= damage;
        SetHurtState();
    }

    public void ShootArrow()
    {
        if (arrowPrefab == null || target == null) return;

        GameObject arrowGO = Instantiate(arrowPrefab, arrowShoter.position, Quaternion.identity);
        Arrow arrow = arrowGO.GetComponent<Arrow>();

        if (arrow != null)
        {
            arrow.startPosition = arrowShoter.position;
            arrow.SetTarget(target.position);
        }
    }

    #region private methods
    private IEnumerator HideCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private IEnumerator ForgetTargetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        ResetTargetToTower();
    }
    #endregion
}
