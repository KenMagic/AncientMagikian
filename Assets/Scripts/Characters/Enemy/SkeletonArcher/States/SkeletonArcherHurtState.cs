using UnityEngine;

public class SkeletonArcherHurtState : IState
{
    private Animator anim;
    private SkeletonArcher skeletonArcher;

    public SkeletonArcherHurtState(Animator anim, SkeletonArcher skeletonArcher)
    {
        this.anim = anim;
        this.skeletonArcher = skeletonArcher;
    }

    public void OnEnter()
    {

        if (skeletonArcher.enemyData.health <= 0)
        {
            anim.SetTrigger("isDeath");
            skeletonArcher.HideAfterDelay(1f);
            SkeletonArcherPool.Instance.ReturnObject(skeletonArcher.gameObject);
        }
        else
        {
            anim.SetTrigger("isHurt");
        }
    }

    public void OnExit()
    {
    }

    public void OnUpdate()
    {
    }
}
