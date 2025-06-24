using System;
using UnityEngine;

public class TriangleMoveState : IState
{
    Animator anim;
    public Triangle controller;
    public float speed = 2f;
    public Transform target;

    public TriangleMoveState(Animator anim, Triangle controller, float speed, Transform target)
    {
        this.anim = anim;
        this.controller = controller;
        this.speed = speed;
        this.target = target;
    }
    public void OnEnter()
    {
        Debug.Log("Entering Move State");
    }

    public void OnExit()
    {
        Debug.Log("Exiting Move State");
        anim.SetBool("isMoving", false);
    }

    public void OnUpdate()
    {
        anim.SetBool("isMoving", true);
        ChaseTarget();
    }
    void ChaseTarget()
    {
        if (target == null) return;
        target = controller.GetComponent<Triangle>().target;
        Vector3 direction = (target.position - controller.transform.position).normalized;
        controller.transform.position += direction * speed * Time.deltaTime;
    }
}