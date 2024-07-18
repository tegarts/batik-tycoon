using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool tools1, tools2, tools3, tools4, tools5;
    public int money;
    public bool isAlreadyTutor;
    public int machineLevel1, machineLevel2, machineLevel3, machineLevel4, machineLevel5;
    public GameData()
    {
        this.tools1 = false;
        this.tools2 = false;
        this.tools3 = false;
        this.tools4 = false;
        this.tools5 = false;
        money = 0;
        machineLevel1 = 1;
        machineLevel2 = 1;
        machineLevel3 = 1;
        machineLevel4 = 1;
        machineLevel5 = 1;
        isAlreadyTutor = false;
    }
}
