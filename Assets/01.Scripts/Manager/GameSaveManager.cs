using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager Instance { get; private set; }

    private string savePath;
    private int currentStageIndex;
    private string currentSceneName;
    private string previousSceneName;
    public int CurrentStageIndex => currentStageIndex;

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
            return;
        }

        savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }

    public void Save()
    {
        GameDataSave data = new GameDataSave();

        data.stageIndex = currentStageIndex;
        data.gold = moneyManager.Instance.Gold;
        data.sceneName = currentSceneName;

        foreach (WeaponData weapon in InventoryManager.Instance.GetWeapons())
            data.weapons.Add(weapon.name);

        foreach (ShieldData shield in InventoryManager.Instance.GetShields())
            data.shields.Add(shield.name);

        WeaponData weaponData = EquipManager.Instance.GetEquippedWeapon();
        ShieldData shieldData = EquipManager.Instance.GetEquippedShield();

        data.equippedWeapon = weaponData != null ? weaponData.name : "";
        data.equippedShield = shieldData != null ? shieldData.name : "";

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(savePath, json);

        Debug.Log("게임 저장 완료");
    }

    public void Load()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("저장 파일이 없음");
            return;
        }

        string json = File.ReadAllText(savePath);
        GameDataSave data = JsonUtility.FromJson<GameDataSave>(json);

        currentStageIndex = data.stageIndex;
        currentSceneName = data.sceneName;

        moneyManager.Instance.SetGold(data.gold);

        foreach (string weaponName in data.weapons)
        {
            WeaponData weapon = FindWeapon(weaponName);

            if (weapon != null)
                InventoryManager.Instance.AddWeapons(weapon);
        }

        foreach (string shieldName in data.shields)
        {
            ShieldData shield = FindShield(shieldName);

            if (shield != null)
                InventoryManager.Instance.AddShields(shield);
        }

        WeaponData equippedWeapon = FindWeapon(data.equippedWeapon);
        ShieldData equippedShield = FindShield(data.equippedShield);

        if (equippedWeapon != null)
            EquipManager.Instance.EquipWeapon(equippedWeapon);

        if (equippedShield != null)
            EquipManager.Instance.EquipShield(equippedShield);

        Debug.Log("게임 불러오기 완료");
    }

    public void SetStage(int stageIndex)
    {
        currentStageIndex = stageIndex;
        currentSceneName = SceneManager.GetActiveScene().name;
        Save();
    }

    public void LoadCurrentStage()
    {
        SceneManager.LoadScene(currentStageIndex);
    }

    private WeaponData FindWeapon(string weaponName)
    {
        if (string.IsNullOrEmpty(weaponName))
            return null;

        WeaponData[] weapons = Resources.LoadAll<WeaponData>("Data/Weapons");

        foreach (WeaponData weapon in weapons)
        {
            if (weapon.name == weaponName)
                return weapon;
        }

        return null;
    }

    private ShieldData FindShield(string shieldName)
    {
        if (string.IsNullOrEmpty(shieldName))
            return null;

        ShieldData[] shields = Resources.LoadAll<ShieldData>("Data/Shields");

        foreach (ShieldData shield in shields)
        {
            if (shield.name == shieldName)
                return shield;
        }

        return null;
    }

    public bool HasSave()
    {
        return File.Exists(savePath);
    }

    public void DeleteSave()
    {
        if (!File.Exists(savePath))
            return;

        File.Delete(savePath);
        Debug.Log("저장 파일 삭제");
    }


    public void SetPreviousScene(string sceneName)
    {
        previousSceneName = sceneName;
    }

    public void LoadPreviousScene()
    {
        if (string.IsNullOrEmpty(previousSceneName))
            return;

        SceneManager.LoadScene(previousSceneName);
    }
}