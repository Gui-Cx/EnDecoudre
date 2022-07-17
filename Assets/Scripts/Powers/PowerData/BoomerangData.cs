using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoomerangData", menuName = "PowerData/BoomerangData")]

public class BoomerangData : PowerData
{   
    [Tooltip("The maximal distance of the boomerang")]
    public int distance;

    [Tooltip("The duration for the boomerang to reach its maximal distance")]
    public int duration;

    [Tooltip("The damage taken by each enemy hit by the boomerang")]
    public int damage;
    public GameObject bulletPrefab;


    public override string ToString(){
        return string.Format("Boomerang : distance = {0}, duration = {1}, damage = {2}, cooldown = {3}, total charges = {4}", distance, duration, damage, cooldown, totalCharges);
    }
}