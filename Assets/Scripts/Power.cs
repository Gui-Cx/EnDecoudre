using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            ActivateOnce();
            currentCharges--;
        } else {
            //An animation where the player to show the player does not have enough charges
        }
    }

    protected void ActivateOnce()
    { }
}
