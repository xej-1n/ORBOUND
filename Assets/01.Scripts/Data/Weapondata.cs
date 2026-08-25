using UnityEngine;

public enum WeaponType { Basic, Pierce, Chain, Explosion, Split, Lifesteal, Execute, DamageOverTime , Mark, Siege, Spread, Focus }

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
    public bool DefenseIgnore;

    [Header("#Special")]
    public int SplitCount = 1; // 분열
    public int ChainCount; //연쇄
    public float ExplosionRadius; //폭팔
    public float LifestealPercent; //흡혈
    public float ExecuteThreshold; //처형
    public float DamageOverTime; // 도트
    public int Duration; // 도트 지속 턴
    public float MarkBonusDamage; //표식
    public float SiegeBonusDamage; //공성 
    public float SpreadDamageMultiplier = 1f; //분산
    public float FocusBonusDamage; //집중

}