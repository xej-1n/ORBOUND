using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private ItemSlot itemSlotPrefab;
    [SerializeField] private ItemInfoPanel infoPanel;

    [Header("#Shop Items")]
    [SerializeField] private List<WeaponData> weapons = new List<WeaponData>();
    [SerializeField] private List<ShieldData> shields = new List<ShieldData>();
    [SerializeField] private List<SkillData> skills = new List<SkillData>();

    private void Start()
    {
        CreateItemSlots();
    }

    private void CreateItemSlots()
    {
        foreach (WeaponData weapon in weapons)
        {
            ItemSlot slot = Instantiate(itemSlotPrefab, content);
            slot.SetItem(weapon.WeaponName, weapon.Icon, weapon.Description, infoPanel, weapon,this);
        }

        foreach (ShieldData shield in shields)
        {
            ItemSlot slot = Instantiate(itemSlotPrefab, content);
            slot.SetItem(shield.ShieldName, shield.Icon, shield.Description, infoPanel, shield,this);
        }

        foreach (SkillData skill in skills)
        {
            ItemSlot slot = Instantiate(itemSlotPrefab, content);
            slot.SetItem(skill.SkillName, skill.Icon, skill.Description, infoPanel, skill,this);
        }
    }
    public void BuyItem(ScriptableObject item)
    {
        if (item is WeaponData weapon)
        {
            InventoryManager.Instance.AddWeapons(weapon);
            Debug.Log(weapon.WeaponName + " 구매");
        }
        else if (item is ShieldData shield)
        {
            InventoryManager.Instance.AddShields(shield);
            Debug.Log(shield.ShieldName + " 구매");
        }
        else if (item is SkillData skill)
        {
            InventoryManager.Instance.AddSkills(skill);
            Debug.Log(skill.SkillName + " 구매");
        }
    }
}