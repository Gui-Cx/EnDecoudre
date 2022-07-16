using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PowerEnum{
    Nova,
    Dash,
    Boomerang,
    Sword
}

public abstract class Power : MonoBehaviour
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
    /// The method called when the power must discard one use
    /// </summary>
    public void Activate(){
        if (currentCharges > 0){
            currentCharges--;
            ActivateOnce();
        } else {
            //An animation where the player to show the player does not have enough charges
        }
    }

    protected abstract void ActivateOnce();

    public Power(PowerEnum powerName){
        Power power;
        switch (powerName){
            case PowerEnum.Nova:
                power = new Nova(); 
                break;
            case PowerEnum.Dash:
                power = new Dash();
                break;
            // case PowerEnum.Boomerang:
            //     break;
            // case PowerEnum.Sword:
            //     break;
            default:
                throw new System.NotImplementedException(string.Format("PowerEnum : {0} not recognized", powerName));
        }
        power.currentCharges = power.totalCharges;
    }

    public Power(){}
}
