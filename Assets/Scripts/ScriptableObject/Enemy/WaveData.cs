using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Wave Data", fileName = "New Wave Data")]
public class WaveData : ScriptableObject
{
    public string waveName = "Wave 1";
    public List<WaveEnemyEntry> enemiesInWave;
}