using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Power
{
    /// <summary>
    /// Fields are :
    /// int radius : The radius of the shot
    /// int angle : Enemies between -angle and +angle will be damaged, with 0 being the forward direction of the player
    /// int damage : The damage taken by each enemy hit by the cone
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    public ShotgunData shotgunData;

    public override void ActivateOnce(Player player)
    {
        Debug.LogFormat("Shotgun {0}/{1} : {2}", totalCharges-currentCharges, totalCharges, shotgunData.ToString());

    }

    public Shotgun(PowerData powerData, Player playerArg) : base(powerData, playerArg)
    {
        shotgunData = powerData as ShotgunData;
    }
}
