using System.Collections;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField] public EnemySO enemyData;
    [SerializeField] private Animator animator;

    public StateMachine stateMachine;
    public Transform towerTarget;
    public Transform target;
    private Coroutine forgetTargetCoroutine;

    public float distanceToTarget = 1.5f;

    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Tower")?.transform;

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

        stateMachine.SetState(new OrcMoveState(animator, this, target));
    }

    void Update()
    {
        Debug.DrawRay(transform.position, (target.position - transform.position).normalized * distanceToTarget, Color.red);
        stateMachine.Update();
    }


    public void DealDamage()
    {
        Debug.Log("Orc gây sát thương!");
    }

    public void DamegeTaken()
    {
        Debug.Log("Orc nhận sát thương!");
    }

    public void SetAttackState(int type)
    {
        stateMachine.SetState(new OrcAttackState(animator, this, enemyData.attackCooldown, type));
    }

    public void SetMoveState()
    {
        stateMachine.SetState(new OrcMoveState(animator, this, target));
    }

    public void SetHurtState()
    {
        stateMachine.SetState(new OrcHurtState(animator, this));
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
}


