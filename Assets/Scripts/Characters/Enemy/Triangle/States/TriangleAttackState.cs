using UnityEngine;

public class TriangleAttackState : IState
{
    Animator anim;
    private Triangle triangle;
    private float attackCooldown = 1f;
    private float lastAttackTime;

    public TriangleAttackState(Animator anim, Triangle triangle, float attackCooldown = 1f)
    {
        this.anim = anim;
        this.triangle = triangle;
        this.attackCooldown = attackCooldown;
    }

    public void OnEnter()
    {
        lastAttackTime = Time.time - attackCooldown; // Allow immediate attack on enter
    }

    public void OnUpdate()
    {
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }
    }

    private void Attack()
    {
        anim.SetBool("isMoving", false);
    }

    public void OnExit()
    {
        // Cleanup if necessary
    }
}