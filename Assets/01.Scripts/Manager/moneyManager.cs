using TMPro;
using UnityEngine;

public class moneyManager : MonoBehaviour
{
    public static moneyManager Instance { get; private set; }

    [SerializeField] private int gold = 0;
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
    public bool CanAfford(int price)
    {
        return gold >= price;
    }

    public bool SpendGold(int price)
    {
        if (!CanAfford(price)) return false;

        gold -= price;
        return true;
    }

    public void AddGold(int amount)
    {
        if (amount <= 0) return;

        gold += amount;
    }
}
