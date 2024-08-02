using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    // TODO - Hapus boolean tools1-5 kalo emang gak kepake
    public bool tools1, tools2, tools3, tools4, tools5;
    public int money, kain;
    public bool isAlreadyTutor;
    public int machineLevel1, machineLevel2, machineLevel3, machineLevel4, machineLevel5;
    public int jumlahTools1, jumlahTools2, jumlahTools3, jumlahTools4, jumlahTools5;
    public int upgradeCounter;
    public int day;
    public GameData()
    {
        this.tools1 = false;
        this.tools2 = false;
        this.tools3 = false;
        this.tools4 = false;
        this.tools5 = false;
        money = 0;
        kain = 0;
        machineLevel1 = 1;
        machineLevel2 = 1;
        machineLevel3 = 1;
        machineLevel4 = 1;
        machineLevel5 = 1;

        jumlahTools1 = 1;
        jumlahTools2 = 1;
        jumlahTools3 = 1;
        jumlahTools4 = 1;
        jumlahTools5 = 1;

        isAlreadyTutor = false;
        upgradeCounter = 0;
        day = 0;
    }
}
