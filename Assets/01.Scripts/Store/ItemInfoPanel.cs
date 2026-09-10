using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text price;
    [SerializeField] private TMP_Text description;
    [SerializeField] private Button buyBtn;

    private ScriptableObject itemData;
    private StoreManager storeManager;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(string name, Sprite sprite, string desc, ScriptableObject data, StoreManager manager)
    {
        icon.sprite = sprite;
        itemName.text = name;
        description.text = desc;
        itemData = data;
        storeManager = manager;

        if (data is WeaponData weapon)
            price.text = weapon.Price + "G";
        else if (data is ShieldData shield)
            price.text = shield.Price + "G";
        else if (data is SkillData skill)
            price.text = skill.Price + "G";

        buyBtn.onClick.RemoveAllListeners();
        buyBtn.onClick.AddListener(Buy);

        gameObject.SetActive(true);
    }
    private void Buy()
    {
        storeManager.BuyItem(itemData);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}