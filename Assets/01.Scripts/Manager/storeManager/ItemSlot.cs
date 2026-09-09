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

    public void SetItem(string name, Sprite sprite, string desc, ItemInfoPanel panel)
    {
        itemName.text = name;
        icon.sprite = sprite;
        description = desc;
        infoPanel = panel;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ShowInfo);
    }

    private void ShowInfo()
    {
        infoPanel.Show(itemName.text, icon.sprite, description);
    }
}