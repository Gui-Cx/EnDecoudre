using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Power
{
    /// <summary>
    /// Fields are :
    /// int distance : The maximal distance of the boomerang
    /// int duration : The duration for the boomerang to reach its maximal distance
    /// int damage : The damage taken by each enemy hit by the boomerang
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    public BoomerangData boomerangData;

    public override void ActivateOnce(Player player)
    {
        Debug.LogFormat("Boomerang {0}/{1} : {2}", totalCharges-currentCharges, totalCharges, boomerangData.ToString());
    }

    public Boomerang(PowerData powerData) : base(powerData)
    {
        boomerangData = powerData as BoomerangData;
    }
}
