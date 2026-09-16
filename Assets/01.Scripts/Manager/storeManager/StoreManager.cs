using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private ItemSlot itemSlotPrefab;
    [SerializeField] private ItemInfoPanel infoPanel;
    [SerializeField] private ShopKeeper shopKeeper;
    [SerializeField] private TMP_Text goldtext;

    [Header("#Shop Items")]
    [SerializeField] private List<WeaponData> weapons = new List<WeaponData>();
    [SerializeField] private List<ShieldData> shields = new List<ShieldData>();

    private void Start()
    {
        CreateItemSlots();
        UpdateGoldUI();
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
    }
    public bool BuyItem(ShopItemData item)
    {
        if (!moneyManager.Instance.SpendGold(item.Price))
        {
            shopKeeper.ShowMessage(" 음...\n 돈이 좀 부족하네.");
            return false;
        }

        InventoryManager.Instance.AddItem(item);
        UpdateGoldUI();
        shopKeeper.ShowMessage("좋은 물건을 골랐네!\n잘 쓰게!");
        Debug.Log(item.name + " 구매");

        return true;
    }
    private void UpdateGoldUI()
    {
        goldtext.text = moneyManager.Instance.Gold + "G";
    }
}