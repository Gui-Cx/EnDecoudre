using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Power
{
    public override int totalCharges{get; set;} = 4;

    public Dash(){}

    public override void ActivateOnce(Player player)
    {
        Debug.LogFormat("Cx : Dash activated ! Total charges = {0}, remaining charges = {1}", totalCharges, currentCharges);
    }
}
