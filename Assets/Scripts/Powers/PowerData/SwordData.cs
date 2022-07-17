using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwordData", menuName = "PowerData/SwordData")]

public class SwordData : PowerData
{   
    [Tooltip("The radius of the circular arc described by the sword")]
    public int radius;

    [Tooltip("Enemies between -angle and +angle will be damaged, with 0 being the forward direction of the player")]
    public int angle;

    [Tooltip("Damage taken by each enemy hit by the attack")]
    public int damage;

    [Tooltip("The prefab of the game object of the sword area")]
    public GameObject swordAreaPrefab;

    public override string ToString()
    {
        return string.Format("Sword : radius = {0}, angle = {1}, damage = {2}, cooldown = {3}, total charges = {4}", radius, angle, damage, cooldown, totalCharges);
    }
}