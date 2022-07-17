using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : Power
{
    ///<summary>
    /// The fields are
    ///int spread : The angle theta of the spread. Each volley of bullets will be spread evenly between -theta and +theta, 0 being the forward direction of the player
    ///int bulletPerCharge : The number of bullet per volley. They will be spread evenly between -spread and +spread
    ///int damagePerBullet : The number of damage each enemy hit by a bullet will take 
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    ///</summary>
    MachinegunData mgData;
    public override void ActivateOnce(Player player)
    {
        Debug.LogFormat("MachineGun {0}/{1} : {2}", totalCharges-currentCharges, totalCharges, mgData.ToString());
    }

    public Machinegun(PowerData powerData) : base(powerData){
        mgData = powerData as MachinegunData; 
    }
}
