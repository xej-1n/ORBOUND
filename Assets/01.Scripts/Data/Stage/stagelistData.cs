using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "stagelistData", menuName = "stage/stagelistData")]
public class stagelistData : ScriptableObject
{
    public List<StageData> Stages;
}
