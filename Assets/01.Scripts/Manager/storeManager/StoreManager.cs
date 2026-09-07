using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private ItemSlot itemSlotPrefab;

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
            slot.SetItem(weapon.WeaponName, weapon.Icon);
        }

        foreach (ShieldData shield in shields)
        {
            ItemSlot slot = Instantiate(itemSlotPrefab, content);
            slot.SetItem(shield.ShieldName, shield.Icon);
        }

        foreach (SkillData skill in skills)
        {
            ItemSlot slot = Instantiate(itemSlotPrefab, content);
            slot.SetItem(skill.SkillName, skill.Icon);
        }
    }
}
