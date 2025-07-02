using System.Collections;
using UnityEngine;

public class SkeletonArcher : MonoBehaviour
{
    [SerializeField] public EnemySO enemyData;
    public GameObject arrowPrefab;

    public Animator animator;
    public StateMachine stateMachine;

    public Transform target;
    public Transform towerTarget;
    public Transform shootPoint;

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

        towerTarget = GameObject.FindGameObjectWithTag("Tower")?.transform;
        target = towerTarget;
    }

    private void Start()
    {
        if (target == null || stateMachine == null || animator == null)
        {
            Debug.LogError("SkeletonArcher thiếu thành phần");
            return;
        }

        stateMachine.SetState(new SkeletonArcherMoveState(animator, this));
    }

    private void Update()
    {
        stateMachine.Update();

        if (isChasing && target != null)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance <= distanceToTarget)
            {
                stateMachine.SetState(new SkeletonArcherAttackState(animator, this, enemyData.attackCooldown));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player vào vùng SkeletonArcher");
            target = other.transform;
            isChasing = true;
            stateMachine.SetState(new SkeletonArcherMoveState(animator, this));
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, distanceToTarget, targetLayerMask);
        if (hit.collider != null && hit.collider.CompareTag("Tower"))
        {
            Debug.Log("SkeletonArcher chạm Tower");
            stateMachine.SetState(new SkeletonArcherAttackState(animator, this, enemyData.attackCooldown));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player rời SkeletonArcher");
            if (stopChaseCoroutine != null) StopCoroutine(stopChaseCoroutine);
            stopChaseCoroutine = StartCoroutine(DelayStopChasing());
            return;
        }

        if (other.CompareTag("Tower"))
        {
            Debug.Log("Exited Tower");
            stateMachine.SetState(new SkeletonArcherMoveState(animator, this));
        }
    }

    private IEnumerator DelayStopChasing()
    {
        yield return new WaitForSeconds(2f);
        target = towerTarget;
        isChasing = false;
        stateMachine.SetState(new SkeletonArcherMoveState(animator, this));
    }

    public void DealDamage()
    {
        var castle = target.GetComponent<Castle>();
        if (castle != null)
        {
            castle.TakeDamage(enemyData.attackDamage);
        }
    }

    public void ShootArrow()
    {
        if (arrowPrefab == null || shootPoint == null || target == null) return;

        GameObject arrow = Instantiate(arrowPrefab, shootPoint.position, Quaternion.identity);
        arrow.GetComponent<Arrow>().SetTarget(target); // Quan trọng!
    }

}
