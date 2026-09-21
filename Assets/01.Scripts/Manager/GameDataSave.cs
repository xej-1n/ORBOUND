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
}