using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DashData", menuName = "PowerData/DashData")]
public class DashData : PowerData
{  
    
    [Tooltip("The duration of the dash")]
    public int duration;
    
    [Tooltip("The maximal distance of the dash")]
    public int distance;
    
    [Tooltip("The height of the rectangular hitbox during the dash (height = player's front-back axis)  ")]
    public int movingDamageHeight;
    
    [Tooltip("The width of the rectangular hitbox during the dash (width = player's left-right axis)")]
    public int movingDamageWidth;
    
    [Tooltip("The radius of the circular hitbox at the end of the dash ")]
    public int endDamageRadius;
    
    [Tooltip("Damage taken by each enemy hit during the dash")]
    public int movingDamage;
    
    [Tooltip("Damage take by each enemy hit at the end of the dash")]
    public int endDamage;

    public override string ToString()
    {
        return string.Format("DashData : duration = {0}, distance = {1}, movingDamageHeight = {2}, movingDamageWidth = {3}, endDamageRadius = {4}, movingDamage = {5}, endDamage = {6}, cooldown = {7}, total charges = {8}", duration, distance, movingDamageHeight, movingDamageWidth, endDamageRadius, movingDamage, endDamage, cooldown, totalCharges);
    }
}
