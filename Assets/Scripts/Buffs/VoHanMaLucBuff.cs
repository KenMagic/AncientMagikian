
using UnityEngine;
public class VoHanMaLucBuff : IBuff
{
    public string BuffName => "Vo Han Ma Luc";

    public BuffEffectType EffectType => BuffEffectType.Custom;

    public float Duration { get; private set; }

    public VoHanMaLucBuff()
    {
        Duration = 3f;
    }
    public VoHanMaLucBuff(float duration)
    {
        Duration = duration;
    }

    public void Apply(GameObject target)
    {
        if (target.TryGetComponent<INoCD>(out INoCD noCD))
        {
            noCD.IsNoCoolDown = true;
        }
    }

    public void Remove(GameObject target)
    {
        if (target.TryGetComponent<INoCD>(out INoCD noCD))
        {
            noCD.IsNoCoolDown = false;
        }
    }
}
