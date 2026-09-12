using TMPro;
using UnityEngine;

public class moneyManager : MonoBehaviour
{
    public static moneyManager Instance { get; private set; }

    [SerializeField] private int gold = 1000;
    [SerializeField] private TMP_Text goldText;

    public int Gold => gold;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateGoldUI();
    }

    public bool CanAfford(int price)
    {
        return gold >= price;
    }

    public bool SpendGold(int price)
    {
        if (!CanAfford(price)) return false;

        gold -= price;
        UpdateGoldUI();
        return true;
    }

    public void AddGold(int amount)
    {
        if (amount <= 0) return;

        gold += amount;
        UpdateGoldUI();
    }

    private void UpdateGoldUI()
    {
        goldText.text = gold + "G";
    }
}
