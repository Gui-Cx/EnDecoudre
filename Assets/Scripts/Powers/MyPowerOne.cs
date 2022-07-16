using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyPowerOne", menuName = "CustomScriptableObject/Powers/MyPowerOne")]
public class MyPowerOne : MyTemplatePower
{   
    public override void ActivateOnce(Player player){
        Debug.Log("Cx : My Power Once activated");
    }
}
