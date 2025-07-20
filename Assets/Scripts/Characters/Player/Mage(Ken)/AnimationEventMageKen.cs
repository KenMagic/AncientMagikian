using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationEventMageKen : MonoBehaviour
{
    [SerializeField]
    private MageKen mageKen;
    public void ResetAttackState()
    {
        mageKen.isAttacking = false;
    }
}
    

