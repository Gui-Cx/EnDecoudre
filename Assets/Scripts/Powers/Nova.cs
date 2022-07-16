using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nova : Power
{
    public new int totalCharges = 1;

    public Nova(){}

    protected override void ActivateOnce(){
        Debug.LogFormat("Cx : Nova activated");
    }
}
