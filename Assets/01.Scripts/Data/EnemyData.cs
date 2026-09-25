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
    public RuntimeAnimatorController animatorController;
    public bool isFlip;
    public int scale = 1;

    [Header("Boss")]
    public bool IsBoss;
    public BossType BossType;
}