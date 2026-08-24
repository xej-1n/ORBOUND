using UnityEngine;

public enum SkillType { Attack, Defense, Utility, CrowdControl }
public enum SkillEffectType { None, HeavyDamage, Pierce, Explosion, Shield }

[CreateAssetMenu(fileName = "SkillData", menuName = "ORBBOUND/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("#Basic")]
    public string SkillName;
    [TextArea] public string Description;
    public Sprite Icon;
    public SkillType Type;
    public SkillEffectType EffectType;

    [Header("#Cost")]
    public float EnergyCost;

    [Header("#Damage")]
    public float DamageMultiplier = 1f;
    public float BonusDamage;
    public float DefenseIgnore;
    public float ExplosionRadius;

    [Header("#Shield")]
    public float ShieldAmount;
    public float Duration;
}