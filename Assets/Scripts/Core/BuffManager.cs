using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    //list of active buffs
    private List<IBuff> activeBuffs = new List<IBuff>();

    // Method to apply a buff
    public void ApplyBuff(IBuff buff)
    {
        if (buff is BleedDebuff newBleed)
        {
            var existingInstance = activeBuffs.Find(b => b is BleedDebuff);

            if (existingInstance is BleedDebuff existingBleed)
            {
                // Gộp stack
                newBleed.StackCount += existingBleed.StackCount;
                Debug.Log($"Stacking Bleed Debuff: {newBleed.StackCount} on {gameObject.name}");
                RemoveBuff(existingBleed);
            }
        }
        buff.Apply(gameObject);
        
        activeBuffs.Add(buff);
        StartCoroutine(RemoveBuffAfterDuration(buff));
    }

    // Method to remove a buff after its duration
    public IEnumerator RemoveBuffAfterDuration(IBuff buff)
    {
        yield return new WaitForSeconds(buff.Duration);
        buff.Remove(gameObject);
        activeBuffs.Remove(buff);
    }
    // Method to remove a specific buff
    public void RemoveBuff(IBuff buff)
    {
        if (activeBuffs.Contains(buff))
        {
            buff.Remove(gameObject);
            activeBuffs.Remove(buff);
        }
        else
        {
            Debug.LogWarning($"Buff {buff.BuffName} not found in active buffs.");
        }
    }
    public List<IBuff> GetActiveBuffs()
    {
        return activeBuffs;
    }
}

