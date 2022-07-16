using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nova : Power
{
    public override int totalCharges{get; set;} = 1;

    public override void ActivateOnce(Player player){
        Debug.LogFormat("Cx : Nova activated, Total charges = {0}, remaining charges = {1}", totalCharges, currentCharges);
    }
}
