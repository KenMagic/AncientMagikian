using UnityEngine;

public class Castle : MonoBehaviour
{
    public float maxHP = 200;
    private float currentHP;

    void Start() => currentHP = maxHP;

    public void TakeDamage(float dmg)
    {
        currentHP -= dmg;
        Debug.Log("Castle bị tấn công! HP còn lại: " + currentHP);
        if (currentHP <= 0)
        {
            Debug.Log("Castle đã bị phá huỷ!");
        }
    }
}
