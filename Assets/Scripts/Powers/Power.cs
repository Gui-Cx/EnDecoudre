using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerEnum
{
    Nova = 0,
    Shotgun = 1,
    Boomerang = 2,
    Dash = 3,
    Sword = 4,
    Machinegun = 5
}

public abstract class Power
{
    /// <summary>
    /// The number of uses left for this power;
    /// </summary>
    public int currentCharges;

    /// <summary>
    /// The total number of uses at the start for this power;
    /// </summary>
    public int totalCharges;

    /// <summary>
    /// The time between two attacks 
    /// </summary>
    public float cooldown;
    

    public abstract void ActivateOnce(Player player);

    public static Power GetPower(PowerEnum powerName, List<PowerData> listPowerData)
    {
        Power power;
        PowerData powerData;
        int index = (int) powerName;
        switch (powerName)
        {
            case PowerEnum.Nova:
                powerData = listPowerData[index];
                power = new Nova(powerData);
                break;
            case PowerEnum.Shotgun:
                powerData = listPowerData[index];
                power = new Shotgun(powerData);
                break;
            case PowerEnum.Boomerang:
                powerData = listPowerData[index];
                power = new Boomerang(powerData);
                break;
            case PowerEnum.Dash:
                powerData = listPowerData[index];
                power = new Dash(powerData);
                break;
            case PowerEnum.Sword:
                powerData = listPowerData[index];
                power = new Sword(powerData);
                break;
            case PowerEnum.Machinegun:
                powerData = listPowerData[index];
                power = new Machinegun(powerData);
                break;
            default:
                throw new System.NotImplementedException(string.Format("PowerEnum : {0} not recognized", powerName));
        }
        return power;
    }
    
    public Power(){}

    public Power(PowerData powerData)
    {
        totalCharges = powerData.totalCharges;
        currentCharges = powerData.totalCharges;
        
    }
}
