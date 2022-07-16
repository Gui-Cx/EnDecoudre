using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerEnum
{
    Nova,
    Dash,
    Boomerang,
    Sword
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
    public abstract int totalCharges { get; set; }

    /// <summary>
    /// The method called when the power must discard one use
    /// </summary>
    // public void Activate(){
    //     if (currentCharges > 0){
    //         currentCharges--;
    //         ActivateOnce();
    //     } else {
    //         Debug.LogFormat("Cx : No more ammunition");
    //         //An animation where the player to show the player does not have enough charges
    //     }
    // }

    public abstract void ActivateOnce(Player player);

    public static Power GetPower(PowerEnum powerName)
    {
        Power power;
        switch (powerName)
        {
            case PowerEnum.Nova:
                power = new Nova();
                break;
            case PowerEnum.Dash:
                power = new Dash();
                break;
            case PowerEnum.Boomerang:
                power = new Boomerang();
                break;
            case PowerEnum.Sword:
                power = new Sword();
                break;
            default:
                throw new System.NotImplementedException(string.Format("PowerEnum : {0} not recognized", powerName));
        }
        return power;
    }

    public Power()
    {
        Debug.LogFormat("Cx : Power construit");
        currentCharges = totalCharges;
    }
}
