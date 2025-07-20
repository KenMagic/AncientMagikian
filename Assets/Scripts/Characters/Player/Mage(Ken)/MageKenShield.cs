using UnityEngine;

public class MageKenShield : MonoBehaviour
{
    public Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void RemoveShield()
    {
        Destroy(this);
    }

    // break shield
    public void BreakShield()
    {
        animator.SetTrigger("Hurt");
    }

}
