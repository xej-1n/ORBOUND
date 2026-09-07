using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance { get; private set; }

    [SerializeField] private WeaponData equippedWeapon;
    [SerializeField] private ShieldData equippedShield;

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
        DontDestroyOnLoad (gameObject);
    }
    #region 무기
    public void EquipWeapon(WeaponData weapon)
    {
        if (weapon == null || !InventoryManager.Instance.HasWeapon(weapon))
            return;

        equippedWeapon = weapon;
    }
    public WeaponData GetEquippedWeapon()
    {
        return equippedWeapon;
    }
    public void UnequipWeapon()
    {
        equippedWeapon = null;
    }
    public bool HasEquippedWeapon()
    {
        return equippedWeapon != null;
    }
    #endregion
    #region 방패
    public void EquipShield(ShieldData shield)
    {
        if (shield == null || !InventoryManager.Instance.HasShield(shield))
            return;

        equippedShield = shield;
    }
    public ShieldData GetEquippedShield()
    {
        return equippedShield;
    }
    public void UnequipShield()
    {
        equippedShield = null;
    }
    public bool HasEquippedShield()
    {
        return equippedShield != null;
    }
    #endregion
}