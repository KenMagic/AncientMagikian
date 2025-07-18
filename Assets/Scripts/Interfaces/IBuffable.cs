using UnityEngine;
public interface IBuffable
{
    BuffManager BuffManager { get; }
    void ApplyBuff(IBuff buff);
    void RemoveBuff(IBuff buff);
}
