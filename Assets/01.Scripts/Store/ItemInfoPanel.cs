using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text price;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text buyBtnText;
    [SerializeField] private Button buyBtn;

    private ShopItemData itemData;
    private StoreManager storeManager;
    private ItemSlot itemSlot;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(string name, Sprite sprite, string desc, ShopItemData data, StoreManager manager, ItemSlot slot)
    {
        icon.sprite = sprite;
        itemName.text = name;
        description.text = desc;
        price.text = data.Price + "G";
        itemData = data;
        storeManager = manager;
        itemSlot = slot;

        buyBtn.onClick.RemoveAllListeners();
        buyBtn.onClick.AddListener(Buy);

        bool hasItem = InventoryManager.Instance.HasItem(data);
        buyBtn.interactable = !hasItem;
        buyBtnText.text = hasItem ? "구매완료" : "구매";

        gameObject.SetActive(true);
    }

    private void Buy()
    {
        if (storeManager.BuyItem(itemData))
        {
            itemSlot.SetPurchased();
            buyBtn.interactable = false;
            buyBtnText.text = "구매완료";
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}