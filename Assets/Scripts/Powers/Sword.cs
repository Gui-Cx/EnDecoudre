using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Power
{
    /// <summary>
    /// Fields are
    /// int radius : The radius of the circular arc described by the sword
    /// int angle : Enemies between -angle and +angle will be damaged, with 0 being the forward direction of the player
    /// int damage : Damage taken by each enemy hit by the attack
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    SwordData swordData;
    public override void ActivateOnce(Player player)
    {
        Debug.LogFormat("Attack {0}/{1} : {2}", totalCharges-currentCharges, totalCharges, swordData.ToString());
        SoundAssets.instance.PlaySword();
    }

    public Sword(PowerData powerData) : base(powerData)
    {
        swordData = powerData as SwordData;
    }
}
