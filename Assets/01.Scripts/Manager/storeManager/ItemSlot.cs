using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private Button button;

    private string description;
    private ItemInfoPanel infoPanel;
    private ShopItemData itemData;
    private StoreManager storeManager;
    private InventoryUiManager inventoryUiManager;
    private invenItemInfoPannel inventoryInfoPanel;

    public void SetItem(string name, Sprite sprite, string desc, ItemInfoPanel panel, ShopItemData data, StoreManager manager)
    {
        itemName.text = name;
        icon.sprite = sprite;
        icon.color = InventoryManager.Instance.HasItem(data) ? Color.gray : Color.white;

        description = desc;
        infoPanel = panel;
        itemData = data;
        storeManager = manager;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ShowInfo);
    }
    public void SetInventoryItem(ShopItemData data, InventoryUiManager manager, invenItemInfoPannel panel)
    {
        itemData = data;
        inventoryUiManager = manager;
        inventoryInfoPanel = panel;

        if (data is WeaponData weapon)
        {
            itemName.text = weapon.WeaponName;
            icon.sprite = weapon.Icon;
        }
        else if (data is ShieldData shield)
        {
            itemName.text = shield.ShieldName;
            icon.sprite = shield.Icon;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ShowInventoryInfo);
    }
    private void ShowInfo()
    {
        infoPanel.Show(itemName.text, icon.sprite, description, itemData, storeManager,this);
    }
    private void ShowInventoryInfo()
    {
        inventoryInfoPanel.Show(itemData, inventoryUiManager);
    }
    public void SetPurchased()
    {
        icon.color = Color.gray;
    }
}