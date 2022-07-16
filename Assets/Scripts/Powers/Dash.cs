using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Power
{
    public new int totalCharges = 4;

    public Dash(){}

    protected override void ActivateOnce()
    {
        Debug.LogFormat("Cx : Dash activated");
    }
}
