using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public static EquipManager Instance { get; private set; }

    [SerializeField] private WeaponData equippedWeapon;
    [SerializeField] private ShieldData equippedShield;
    [SerializeField] private SkillData equippedSkill;

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

    public void EquipWeapon(WeaponData weapon)
    {
        if (weapon == null)
            return;

        if (!InventoryManager.Instance.HasWeapon(weapon))
            return;

        equippedWeapon = weapon;
    }

    public void EquipShield(ShieldData shield)
    {
        if (shield == null)
            return;

        if (!InventoryManager.Instance.HasShield(shield))
            return;

        equippedShield = shield;
    }

    public void EquipSkill(SkillData skill)
    {
        if (skill == null)
            return;

        if (!InventoryManager.Instance.HasSkill(skill))
            return;

        equippedSkill = skill;
    }

    public WeaponData GetEquippedWeapon()
    {
        return equippedWeapon;
    }

    public ShieldData GetEquippedShield()
    {
        return equippedShield;
    }

    public SkillData GetEquippedSkill()
    {
        return equippedSkill;
    }
}