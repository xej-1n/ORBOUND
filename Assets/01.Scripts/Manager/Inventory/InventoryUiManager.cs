using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class InventoryUiManager : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private ItemSlot itemSlotPrefab;
    [SerializeField] private Image attackEquipmentIcon;
    [SerializeField] private Image shieldEquipmentIcon;
    [SerializeField] private invenItemInfoPannel infoPanel;
    [SerializeField] private TMP_Text goldtext;

    private void Start()
    {
        CreateItemSlots();
        UpdateEquipmentUI();
        UpdateGoldUI();
    }
    public void RefreshInventory()
    {
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        CreateItemSlots();
        UpdateEquipmentUI();
    }
    private void CreateItemSlots()
    {
        foreach (WeaponData weapon in InventoryManager.Instance.GetWeapons())
        {
            if (EquipManager.Instance.GetEquippedWeapon() == weapon) continue;

            ItemSlot slot = Instantiate(itemSlotPrefab, content);
            slot.SetInventoryItem(weapon, this, infoPanel);
        }

        foreach (ShieldData shield in InventoryManager.Instance.GetShields())
        {
            if (EquipManager.Instance.GetEquippedShield() == shield) continue;

            ItemSlot slot = Instantiate(itemSlotPrefab, content);
            slot.SetInventoryItem(shield, this, infoPanel);
        }
    }
    private void UpdateGoldUI()
    {
        goldtext.text = moneyManager.Instance.Gold + "G";
    }
    private void UpdateEquipmentUI()
    {
        WeaponData weapon = EquipManager.Instance.GetEquippedWeapon();
        ShieldData shield = EquipManager.Instance.GetEquippedShield();

        attackEquipmentIcon.sprite = weapon != null ? weapon.Icon : null;
        shieldEquipmentIcon.sprite = shield != null ? shield.Icon : null;

        Color attackColor = attackEquipmentIcon.color;
        attackColor.a = weapon != null ? 1f : 0f;
        attackEquipmentIcon.color = attackColor;

        Color shieldColor = shieldEquipmentIcon.color;
        shieldColor.a = shield != null ? 1f : 0f;
        shieldEquipmentIcon.color = shieldColor;
    }
    public void EquipItem(ShopItemData item)
    {
        if (item is WeaponData weapon)
        {
            EquipManager.Instance.EquipWeapon(weapon);
        }
        else if (item is ShieldData shield)
        {
            EquipManager.Instance.EquipShield(shield);
        }

        RefreshInventory();
    }
    public void ShowEquippedWeapon()
    {
        WeaponData weapon = EquipManager.Instance.GetEquippedWeapon();
        if (weapon == null) return;

        infoPanel.Show(weapon, this);
    }

    public void ShowEquippedShield()
    {
        ShieldData shield = EquipManager.Instance.GetEquippedShield();
        if (shield == null) return;

        infoPanel.Show(shield, this);
    }
    public void UnequipItem(ShopItemData item)
    {
        if (item is WeaponData)
            EquipManager.Instance.UnequipWeapon();
        else if (item is ShieldData)
            EquipManager.Instance.UnequipShield();

        RefreshInventory();
    }
}
