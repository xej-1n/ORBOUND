using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private ItemSlot itemSlotPrefab;
    [SerializeField] private ItemInfoPanel infoPanel;
    [SerializeField] private ShopKeeper shopKeeper;

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
    public bool BuyItem(ShopItemData item)
    {
        if (!moneyManager.Instance.SpendGold(item.Price))
        {
            shopKeeper.ShowMessage(" 음...\n 돈이 좀 부족하네.");
            return false;
        }

        InventoryManager.Instance.AddItem(item);
        shopKeeper.ShowMessage("좋은 물건을 골랐네!\n잘 쓰게!");
        Debug.Log(item.name + " 구매");

        return true;
    }

}