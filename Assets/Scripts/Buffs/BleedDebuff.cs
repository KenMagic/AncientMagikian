using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class BleedDebuff : IBuff
{
    public string BuffName => "Bleed Debuff";
    public BuffEffectType EffectType => BuffEffectType.DamageOverTime;
    public float Duration => 5f;

    private float damagePerTick = 1f;
    private float tickInterval = 1f;

    public int StackCount = 1;

    private Coroutine bleedRoutine;

    public void Apply(GameObject target)
    {
        if (target.TryGetComponent<MonoBehaviour>(out var mb))
        {
            // Dừng coroutine cũ nếu có
            if (bleedRoutine != null)
            {
                mb.StopCoroutine(bleedRoutine);
            }

            bleedRoutine = mb.StartCoroutine(ApplyBleed(target, mb));
        }
    }

    private IEnumerator ApplyBleed(GameObject target, MonoBehaviour mb)
    {
        float elapsed = 0f;
        while (elapsed < Duration)
        {
            if (target.TryGetComponent<IDamagable>(out var damagable))
            {
                damagable.TakeDamage(damagePerTick * StackCount);
                Debug.LogWarning($"{target.name} took {damagePerTick * StackCount} bleed damage (Stack {StackCount}).");
            }

            yield return new WaitForSeconds(tickInterval);
            elapsed += tickInterval;
        }

        Remove(target);
    }

    public void Remove(GameObject target)
    {
        if (target.TryGetComponent<MonoBehaviour>(out var mb) && bleedRoutine != null)
        {
            mb.StopCoroutine(bleedRoutine);
            bleedRoutine = null;
            Debug.LogWarning($"{target.name} bleed effect stopped early.");
        }
    }
}
