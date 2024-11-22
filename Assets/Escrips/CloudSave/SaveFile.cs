using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveFile
{
    public float highScore = 0f;
    //public string playerName = "empty";
    public int fuel = 5;
    public int coins = 0;
    public int bolts = 0;
    public bool upgrade1 = false;
    public bool upgrade2 = false;
    public bool upgrade3 = false;
    public bool customization1 = true;
    public bool customization2 = true;
    public bool customization3 = true;
}