using UnityEngine;
public class StunDebuff : IBuff
{
    public string BuffName => "Stun";

    public BuffEffectType EffectType => BuffEffectType.Stun;

    public float Duration => 2f;

    public void Apply(GameObject target)
    {
        if (target.TryGetComponent<IStunable>(out IStunable stunable))
        {
            stunable.IsStunned = true;
        }
    }

    public void Remove(GameObject target)
    {
        if (target.TryGetComponent<IStunable>(out IStunable stunable))
        {
            stunable.IsStunned = false;
        }
    }
    
}
