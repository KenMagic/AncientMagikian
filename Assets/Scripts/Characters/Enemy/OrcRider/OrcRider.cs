using System.Collections;
using UnityEngine;

public class OrcRider : MonoBehaviour
{
    [SerializeField] public EnemySO enemyData;
    [SerializeField] private Animator animator;

    public StateMachine stateMachine;
    public Transform towerTarget;
    public Transform target;
    private Coroutine forgetTargetCoroutine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DealDamage()
    {
        Debug.Log("Orc gây sát thương!");
    }

    public void DamegeTaken()
    {
        Debug.Log("Orc nhận sát thương!");
    }

    public void SetMoveState()
    {
        stateMachine.SetState(new OrcRiderMoveState(animator, this, target));
    }

    public void SetHurtState()
    {
        stateMachine.SetState(new OrcRiderHurtState(animator, this));
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
