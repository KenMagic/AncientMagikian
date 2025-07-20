using UnityEngine;

[CreateAssetMenu(fileName = "NewWave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public WaveItem[] waveItems;
    public float delayAfterWave = 5f;
}
