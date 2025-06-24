
using Unity.VisualScripting;
using UnityEngine;

public class Triangle : MonoBehaviour
{
    [SerializeField]
    private EnemySO enemyData;
    public Animator animator;
    public StateMachine stateMachine;

    public Transform defaultTarget;
    public Transform target;

    public float speed = 2f;

    private void Awake()
    {
        if (stateMachine == null)
        {
            stateMachine = GetComponent<StateMachine>();
        }

        if (target == null)
        {
            target = defaultTarget;
        }
    }
    void Start()
    {
        stateMachine.SetState(new TriangleMoveState(animator, this, speed, target));
    }

    // Update is called once per frame
    void Update()
    {
        if(InRange(target, 0.5f))
        {
            Debug.Log("Target in range, switching to attack state");
            stateMachine.SetState(new TriangleAttackState(animator, this, 1f));
        }
        else
        {
            stateMachine.SetState(new TriangleMoveState(animator, this, speed, target));
        }
        stateMachine.Update();
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        stateMachine.SetState(new TriangleMoveState(animator, this, speed, target));
    }

    public bool InRange(Transform other, float range)
    {
        return Vector3.Distance(transform.position, other.position) <= range;
    }
}
