using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public string EnemyName;
    public int MaxHP;
    public int MaxAttack;
    public int MinAttack;
    public int Defense;
    public int RewardGold;
    public GameObject Prefab;
}
