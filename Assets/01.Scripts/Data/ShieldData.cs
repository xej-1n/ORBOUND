using UnityEngine;

public enum ShieldType
{
    Basic,Reflect,Regeneration,Explosion,Charge,Emergency,Barrier,Lifesteal
}

[CreateAssetMenu(fileName = "ShieldData", menuName = "ORBBOUND/Shield Data")]
public class ShieldData : ShopItemData
{
    [Header("#Basic")]
    public string ShieldName;
    [TextArea] public string Description;
    public Sprite Icon;
    public ShieldType Type;

    [Header("#내구도")]
    [Min(0)] public int Durability;
    [Range(0f, 100f)] public float DamageReduction;

    [Header("#반사율")]
    [Range(0f, 1f)] public float ReflectPercent;

    [Header("#재생")]
    [Min(0)] public int RegenerationAmount;

    [Header("#폭팔")]
    [Min(0)] public int ExplosionDamage;

    [Header("#충전")]
    [Min(0)] public int ChargeBonusDamage;

    [Header("#응급")]
    [Range(0f, 1f)] public float EmergencyThreshold;
    [Min(0)] public int EmergencyDurability;

    [Header("#보호막")]
    [Min(0)] public int BarrierCount;

    [Header("#흡혈")]
    [Range(0f, 1f)] public float LifestealPercent;
}