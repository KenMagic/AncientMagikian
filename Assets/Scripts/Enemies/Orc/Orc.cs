using System.Collections;
using UnityEngine;

public class Orc : MonoBehaviour
{
    [SerializeField]
    public EnemySO enemyData; // Reference to the EnemySO scriptable object containing enemy data

    public Animator animator;

    public StateMachine stateMachine;
    public Transform target;
    public Transform towerTarget;

    private bool isChasing = false; // Flag to check if the orc is chasing the target
    public float chaseSpeed;
    public float distanceToTarget = 0.5f; // Distance to the target to trigger attack
    private Coroutine stopChaseCoroutine;
    public LayerMask targetLayerMask;

    private void Awake()
    {
        chaseSpeed = enemyData.speed * 2; // Set chase speed from the enemy data
        stateMachine = GetComponent<StateMachine>();
        animator = GetComponent<Animator>();

        GameObject tower = GameObject.FindGameObjectWithTag("Tower");
        if (tower == null)
            target = tower.transform;
    }

    private void Start()
    {
        if (target == null || stateMachine == null || animator == null)
        {
            Debug.LogError("Orc missing essential components");
            return;
        }
        towerTarget = GameObject.FindGameObjectWithTag("Tower").transform;
        stateMachine.SetState(new OrcMoveState(animator, this));

    }


    private void Update()
    {
        stateMachine.Update();
        if (isChasing)
        {
            float distance = Vector2.Distance(transform.position, target.position);
            if (distance <= distanceToTarget)
            {
                stateMachine.SetState(new OrcAttackState(animator, this, enemyData.attackCooldown));
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player vào vùng phát hiện");
            target = other.transform;
            isChasing = true;
            stateMachine.SetState(new OrcMoveState(animator, this));
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(transform.position, other.transform.position - transform.position, distanceToTarget, targetLayerMask);
        if (hit.collider != null && hit.collider.CompareTag("Tower"))
        {
            Debug.Log("Entered Tower");
            stateMachine.SetState(new OrcAttackState(animator, this, enemyData.attackCooldown));
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player rời khỏi vùng");

            if (stopChaseCoroutine != null)
            {
                StopCoroutine(stopChaseCoroutine);
            }
            stopChaseCoroutine = StartCoroutine(DelayStopChasing());
            return;
        }

        if (other.CompareTag("Tower"))
        {
            Debug.Log("Exited Tower");
            stateMachine.SetState(new OrcMoveState(animator, this));
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

    private IEnumerator DelayStopChasing()
    {
        yield return new WaitForSeconds(0.75f); // chờ 2 giây

        target = towerTarget;
        isChasing = false;
        Debug.Log("Ngừng chase sau 2 giây");
        stateMachine.SetState(new OrcMoveState(animator, this));
    }
}
