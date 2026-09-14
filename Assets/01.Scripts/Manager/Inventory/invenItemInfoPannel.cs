using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class invenItemInfoPannel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private Button actionButton;

    private ShopItemData itemData;
    private InventoryUiManager inventoryUiManager;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(ShopItemData data, InventoryUiManager manager)
    {
        itemData = data;
        inventoryUiManager = manager;

        bool equipped = false;

        if (data is WeaponData weapon)
        {
            icon.sprite = weapon.Icon;
            itemName.text = weapon.WeaponName;
            description.text = weapon.Description;
            equipped = EquipManager.Instance.GetEquippedWeapon() == weapon;
        }
        else if (data is ShieldData shield)
        {
            icon.sprite = shield.Icon;
            itemName.text = shield.ShieldName;
            description.text = shield.Description;
            equipped = EquipManager.Instance.GetEquippedShield() == shield;
        }

        buttonText.text = equipped ? "해제" : "장착";

        actionButton.onClick.RemoveAllListeners();

        if (equipped)
            actionButton.onClick.AddListener(Unequip);
        else
            actionButton.onClick.AddListener(Equip);

        gameObject.SetActive(true);
    }

    private void Equip()
    {
        inventoryUiManager.EquipItem(itemData);
        Hide();
    }
    private void Unequip()
    {
        inventoryUiManager.UnequipItem(itemData);
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
