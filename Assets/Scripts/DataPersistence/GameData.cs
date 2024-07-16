using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public bool tools1, tools2, tools3, tools4, tools5;
    public GameData()
    {
        this.tools1 = false;
        this.tools2 = false;
        this.tools3 = false;
        this.tools4 = false;
        this.tools5 = false;
    }
}
