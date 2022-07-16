using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Power
{
    public override int totalCharges{get; set;} = 6;

    public override void ActivateOnce(Player player){
        Debug.LogFormat("Cx : Sword activated once ! Total charges = {0}, remaining charges = {1}", totalCharges, currentCharges);
    } 
}
