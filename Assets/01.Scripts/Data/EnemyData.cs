using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public int MaxHP;
    [Header("계수")]
    public int MinAttack;
    public int MaxAttack;
    public int Defense;
    [Header("그외")]
    public int RewardGold;
    public GameObject Prefab;
}
