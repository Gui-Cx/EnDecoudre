using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Power
{
    /// <summary>
    /// Fields are :
    /// int duration : The duration of the dash
    /// int distance : The maximal distance of the dash
    /// int movingDamageHeight : The height of the rectangular hitbox during the dash (height = player's front-back axis)  
    /// int movingDamageWidth : The width of the rectangular hitbox during the dash (width = player's left-right axis)
    /// int endDamageRadius : The radius of the circular hitbox at the end of the dash 
    /// int movingDamage : Damage taken by each enemy hit during the dash
    /// int endDamage : Damage take by each enemy hit at the end of the dash
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    public DashData dashData;


    public override void ActivateOnce(Player player)
    {
        Debug.LogFormat("Dash {0}/{1} : {2}", totalCharges-currentCharges, totalCharges, dashData.ToString());
    }
    public Dash(PowerData powerData) : base(powerData)
    {
        DashData dashData = powerData as DashData;
    }
}
