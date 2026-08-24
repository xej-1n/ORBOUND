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
    public float Duration;

    [Header("#Special")]
    public float ReflectPercent;
    public float RegenerationAmount;
    public float ExplosionDamage;
    public float ChargeBonusDamage;
    public float EmergencyThreshold;
    public int BarrierCount;
    public float LifestealPercent;
    public float ImpactDamage;
    public float LastStandHeal;
}