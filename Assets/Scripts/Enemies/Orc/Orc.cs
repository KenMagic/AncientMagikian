using System.Collections;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField] public EnemySO enemyData;

    public Animator animator;
    public StateMachine stateMachine;

    public Transform target;
    public Transform towerTarget;

    public float distanceToTarget = 0.5f;
    public LayerMask targetLayerMask;
    public float chaseSpeed;

    public bool isChasing = false;

    private Coroutine stopChaseCoroutine;

    private void Awake()
    {
        chaseSpeed = enemyData.speed * 2;
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();

        towerTarget = GameObject.FindGameObjectWithTag("Tower").transform;
        target = towerTarget;
    }

    private void Start()
    {
        if (target == null || stateMachine == null || animator == null)
        {
            Debug.LogError("Orc missing essential components");
            return;
        }

        stateMachine.SetState(new OrcMoveState(animator, this));
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, (target.position - transform.position).normalized * distanceToTarget, Color.red);
        stateMachine.Update();

        if (isChasing && target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance <= distanceToTarget)
            {
                stateMachine.SetState(new OrcAttackState(animator, this, enemyData.attackCooldown));
            }
        }
    }

    public void StartChasing(Transform player)
    {
        if (stopChaseCoroutine != null)
        {
            StopCoroutine(stopChaseCoroutine);
            stopChaseCoroutine = null;
        }

        Debug.Log("Orc: Bắt đầu đuổi Player");
        target = player;
        isChasing = true;
        stateMachine.SetState(new OrcMoveState(animator, this));
    }

    public void StopChasingAfterDelay(float delay = 0.5f)
    {
        if (stopChaseCoroutine == null)
            stopChaseCoroutine = StartCoroutine(StopChase(delay));
    }

    private IEnumerator StopChase(float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Orc: Ngừng đuổi, quay về tower");
        target = towerTarget;
        isChasing = false;
        stateMachine.SetState(new OrcMoveState(animator, this));
        stopChaseCoroutine = null;
    }

    public void TryAttackTower(Transform tower)
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            tower.position - transform.position,
            distanceToTarget,
            targetLayerMask
        );

        if (hit.collider != null && hit.collider.CompareTag("Tower"))
        {
            Debug.Log("Orc: Attack Tower");
            target = tower;
            stateMachine.SetState(new OrcAttackState(animator, this, enemyData.attackCooldown));
        }
    }

    public void DealDamage()
    {
        var castle = target.GetComponent<Castle>();
        if (castle != null)
        {
            castle.TakeDamage(enemyData.attackDamage);
        }
    }
}
