using System;
using System.Collections.Generic;

[Serializable]
public class GameDataSave
{
    public string sceneName;
    public int stageIndex;
    public int gold;

    public List<string> weapons = new List<string>();
    public List<string> shields = new List<string>();

    public string equippedWeapon;
    public string equippedShield;
    public BattleSaveData battle;
    public bool hasPlayerHp;
    public int playerHp;
}

[Serializable]
public class BattleSaveData
{
    public int turn;
    // -1: player shot, 0..enemy count: next enemy action.
    public int nextEnemyIndex = -1;
    public int score;
    public int playerHp;
    public List<EnemySaveData> enemies = new List<EnemySaveData>();
    public ShieldSaveData shield;
    public WeaponSaveData weapon;
}

[Serializable]
public class EnemySaveData
{
    public int hp;
    public bool rewardGiven;
    public bool crusherReady;
    public int crusherDelay;
    public bool barrierUsed;
    public int barrierAmount;
    public int barrierTimer;
}

[Serializable]
public class ShieldSaveData
{
    public string itemName;
    public int durability;
    public int barrierCount;
    public bool emergencyUsed;
    public bool chargeReady;
}

[Serializable]
public class WeaponSaveData
{
    public string itemName;
    public int markedEnemy = -1;
    public int markTurns;
    public int dotTarget = -1;
    public int dotDamage;
    public int dotTurns;
    public int focusTarget = -1;
    public int focusLevel;
    public bool focusLastAttackDealtDamage;
}
