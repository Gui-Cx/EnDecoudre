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

    public PowerData _powerData;
    public Player player;
    

    public abstract void ActivateOnce(Player player);

    public static Power GetPower(Player playerArg, PowerEnum powerName, List<PowerData> listPowerData)
    {
        Power power;
        PowerData powerData;
        int index = (int) powerName;
        switch (powerName)
        {
            case PowerEnum.Nova:
                powerData = listPowerData[index];
                power = new Nova(powerData, playerArg);
                break;
            case PowerEnum.Shotgun:
                powerData = listPowerData[index];
                power = new Shotgun(powerData, playerArg);
                break;
            case PowerEnum.Boomerang:
                powerData = listPowerData[index];
                power = new Boomerang(powerData, playerArg);
                break;
            case PowerEnum.Dash:
                powerData = listPowerData[index];
                power = new Dash(powerData, playerArg);
                break;
            case PowerEnum.Sword:
                powerData = listPowerData[index];
                power = new Sword(powerData, playerArg);
                break;
            case PowerEnum.Machinegun:
                powerData = listPowerData[index];
                power = new Machinegun(powerData, playerArg);
                break;
            default:
                throw new System.NotImplementedException(string.Format("PowerEnum : {0} not recognized", powerName));
        }
        return power;
    }
    
    public Power(){}

    public Power(PowerData powerData, Player playerArg)
    {
        this.player = playerArg;
        totalCharges = powerData.totalCharges;
        currentCharges = powerData.totalCharges;
        _powerData = powerData;
        
    }
}
