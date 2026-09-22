using UnityEngine;

public enum BossType
{
    None,Crusher,WaterBarrier,ShadowChase
}

[CreateAssetMenu(fileName = "EnemyData", menuName = "ORBBOUND/EnemyData")]
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
    public RuntimeAnimatorController animatorController;
    public bool isFlip;

    [Header("Boss")]
    public bool IsBoss;
    public BossType BossType;

    private void OnValidate()
    {
        if (Prefab != null)
        {
            name = Prefab.name;
        }
    }
}