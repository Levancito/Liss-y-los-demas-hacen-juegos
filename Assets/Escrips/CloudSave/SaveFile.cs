using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveFile
{
    public int highScore = 0;
    public string playerName = "empty";
    public int fuel = 5;
    public int coins = 0;
    public int bolts = 0;
    public bool upgrade1 = false;
    public bool upgrade2 = false;
    public bool upgrade3 = false;
    public bool customization1 = false;
    public bool customization2 = false;
    public bool customization3 = false;
}