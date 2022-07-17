using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Nova : Power
{

    /// <summary>
    /// Fields are
    /// int radius : The radius of the nova
    /// int damage : Damage taken by each enemy hit by the nova
    /// int duration : Duration of the nova
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    NovaData novaData;

    public override void ActivateOnce(Player player){
        Debug.LogFormat("Nova {0}/{1} : {2}", totalCharges-currentCharges, totalCharges, novaData.ToString());
    }

    public Nova(PowerData powerData) : base (powerData){
        novaData = (powerData as NovaData);
    }
}
