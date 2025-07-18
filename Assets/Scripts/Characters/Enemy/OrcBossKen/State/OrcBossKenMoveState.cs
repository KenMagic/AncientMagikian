using System;
using Unity.VisualScripting;
using UnityEngine;

public class OrcBossKenMoveState : IState
{
    private Animator animator;
    [SerializeField]
    private float moveSpeed = 2f; // Speed at which the orc moves towards the target

    public OrcBossKenMoveState(Animator animator)
    {
        this.animator = animator;
    }

    public void OnEnter()
    {
        animator.SetBool("isMoving", true);
    }

    public void OnExit()
    {
        GameObject orc = animator.gameObject;
        // Reset the orc's velocity when exiting the move state
        Rigidbody2D rb = orc.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Stop the orc's movement
        }
        animator.SetBool("isMoving", false);
    }

    

    public void OnUpdate()
    {
        // orc gameobject
        GameObject orc = animator.gameObject;
        GameObject target = orc.GetComponent<OrcBossKen>().GetTarget();
        if (target != null)
        {

            //if target on the left side of the orc, flip the orc
            if (target.transform.position.x < orc.transform.position.x)
            {
                orc.transform.localScale = new Vector3(-Math.Abs(orc.transform.localScale.x), orc.transform.localScale.y, orc.transform.localScale.z);
            }
            else
            {
                orc.transform.localScale = new Vector3(Mathf.Abs(orc.transform.localScale.x), orc.transform.localScale.y, orc.transform.localScale.z);
            }
            orc.transform.position = Vector2.MoveTowards(orc.transform.position, target.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}
