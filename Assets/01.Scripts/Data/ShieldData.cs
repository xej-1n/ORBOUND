using UnityEngine;

public enum ShieldType { Basic, Reflect, Regeneration, Explosion, Charge, Emergency, Barrier, Lifesteal, Impact, LastStand }

[CreateAssetMenu(fileName = "ShieldData", menuName = "ORBBOUND/Shield Data")]
public class ShieldData : ScriptableObject
{
    [Header("#Basic")]
    public string ShieldName;
    [TextArea] public string Description;
    public Sprite Icon;
    public ShieldType Type;

    [Header("#Shield")]
    public float ShieldAmount;
    public float DamageReduction;

    [Header("#Special")]
    public float ReflectPercent; // 반사
    public float RegenerationAmount; // 재생
    public float ExplosionDamage; // 폭발
    public float ChargeBonusDamage; // 충전
    public float EmergencyThreshold; // 응급
    public int BarrierCount; // 보호막
    public float LifestealPercent; // 흡혈
}