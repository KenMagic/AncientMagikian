using System.Security.Cryptography;
using UnityEngine;
public class MageKenSpecialAttackSkill : ISkill
{
    public string SkillName => "MageKenSpecialAttackSkill";
    public float Cooldown { get; private set; }
    public int Level { get; private set; } = 1;

    public int count = 3;
    private float spawnOffset = 1f;

    public MageKenSpecialAttackSkill()
    {
        Cooldown = 2f;
    }

    public void Activate(GameObject owner, GameObject target)
    {
        Transform firePoint = owner.transform;
        MageKen mageKen = owner.GetComponent<MageKen>();
        if (firePoint == null)
        {
            Debug.LogWarning("FirePoint not found on owner.");
            return;
        }

        float angleStep = 360f / count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * angleStep;
            float rad = angle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
            Vector3 spawnPos = firePoint.position + (Vector3)(direction * spawnOffset);

            GameObject bullet = GameObject.Instantiate(mageKen.bulletPrefab, spawnPos, Quaternion.Euler(0f, 0f, angle));
            BulletControllerKen bulletController = bullet.GetComponent<BulletControllerKen>();
            if (bulletController != null)
            {
                 bulletController.SetDirection(direction);
                bulletController.SetDamage(mageKen.characterStats.CurrentAttack);
            }
        }
    }

    public void Deactivate()
    {

    }

    public void Upgrade()
    {
        Level++;
        count++;
    }
}