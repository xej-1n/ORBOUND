using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoPanel : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private TMP_Text description;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show(string name, Sprite sprite, string desc)
    {
        icon.sprite = sprite;
        itemName.text = name;
        description.text = desc;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}