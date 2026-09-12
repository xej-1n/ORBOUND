using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    [SerializeField] private List<WeaponData> weapons = new List<WeaponData>();
    [SerializeField] private List<ShieldData> shields = new List<ShieldData>();
    [SerializeField] private List<SkillData> skills = new List<SkillData>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public bool HasItem(ShopItemData item)
    {
        if (item is WeaponData weapon) return HasWeapon(weapon);
        if (item is ShieldData shield) return HasShield(shield);
        if (item is SkillData skill) return HasSkill(skill);

        return false;
    }
    public void AddItem(ShopItemData item)
    {
        if (item is WeaponData weapon) AddWeapons(weapon);
        else if (item is ShieldData shield) AddShields(shield);
        else if (item is SkillData skill) AddSkills(skill);
    }
    #region 무기
    public void AddWeapons(WeaponData weapon)
    {
        if (weapon != null && !weapons.Contains(weapon))
        {
            weapons.Add(weapon);
        }
    }

    public void RemoveWeapons(WeaponData weapon)
    {
        if (weapon != null)
        {
            weapons.Remove(weapon);
        }
    }

    public bool HasWeapon(WeaponData weapon)
    {
        return weapon != null && weapons.Contains(weapon);
    }
    #endregion

    #region 방어구
    public void AddShields(ShieldData shield)
    {
        if (shield != null && !shields.Contains(shield))
        {
            shields.Add(shield);
        }
    }

    public void RemoveShields(ShieldData shield)
    {
        if (shield != null)
        {
            shields.Remove(shield);
        }
    }

    public bool HasShield(ShieldData shield)
    {
        return shield != null && shields.Contains(shield);
    }
    #endregion

    #region 스킬
    public void AddSkills(SkillData skill)
    {
        if (skill != null && !skills.Contains(skill))
        {
            skills.Add(skill);
        }
    }

    public void RemoveSkills(SkillData skill)
    {
        if (skill != null)
        {
            skills.Remove(skill);
        }
    }

    public bool HasSkill(SkillData skill)
    {
        return skill != null && skills.Contains(skill);
    }
    #endregion
}