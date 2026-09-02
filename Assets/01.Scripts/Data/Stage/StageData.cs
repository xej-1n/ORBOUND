using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "stage/StageData")]
public class StageData : ScriptableObject
{
    public bool IsBoss;
    public List<EnemyData> Enemies;
}
