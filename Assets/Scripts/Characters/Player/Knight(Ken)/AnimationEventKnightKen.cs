using UnityEngine;

public class AnimationEventKnightKen : MonoBehaviour
{
    public GameObject attackHitbox;
    
    public void EnableAttackHitbox()
    {
        attackHitbox.SetActive(true);
    }
    public void DisableAttackHitbox()
    {
        attackHitbox.SetActive(false);
    }
}
