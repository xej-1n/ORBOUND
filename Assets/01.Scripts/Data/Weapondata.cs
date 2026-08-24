using UnityEngine;

public enum WeaponType{Basic,Pierce,Chain,Explosion,Split,Lifesteal,Execute,DamageOverTime,Mark,Siege,Spread,Focus}

[CreateAssetMenu(fileName = "WeaponData", menuName = "ORBBOUND/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("#Basic")]
    public string WeaponName;
    [TextArea] public string Description;
    public Sprite Icon;
    public WeaponType Type;

    [Header("#Damage")]
    public float DamageMultiplier = 1f;
    public float BonusDamage;
    public float DefenseIgnore;
    public float Duration;

    [Header("#Special")]
    public int HitCount = 1;
    public int ChainCount;
    public float ExplosionRadius;
    public float HealPercent;
    public float ExecuteThreshold;
    public float MarkBonusDamage;
    public float HighDefenseBonus;
    public float FocusBonus;
}