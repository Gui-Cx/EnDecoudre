using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Power
{
    public override int totalCharges{get; set;} = 5;

    public override void ActivateOnce(Player player){
        Debug.LogFormat("Cx : Boomerang activated once, Total charges = {0}, remaining charges = {1}", totalCharges, currentCharges);
    }
}
