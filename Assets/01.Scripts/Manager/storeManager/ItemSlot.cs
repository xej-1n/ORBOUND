using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;

    public void SetItem(string name, Sprite sprite)
    {
        itemName.text = name;
        icon.sprite = sprite;
    }
}
