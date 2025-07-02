using UnityEngine;

public class SkeletonArcher : MonoBehaviour
{
    [SerializeField]
    public EnemySO enemyData; // Reference to the EnemySO scriptable object containing enemy data
    public Animator animator;
    public StateMachine stateMachine;
    public Transform target;
    private void Awake()
    {
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
            Debug.LogError("SkeletonArcher missing essential components");
            return;
        }
        stateMachine.SetState(new SkeletonArcherMoveState(animator, this));
    }
    private void Update()
    {
        stateMachine.Update();
    }
    public void MoveTowardsTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, enemyData.speed * Time.deltaTime);
    }
    public bool IsInRange()
    {
        return Vector2.Distance(transform.position, target.position) <= 10;
    }
}