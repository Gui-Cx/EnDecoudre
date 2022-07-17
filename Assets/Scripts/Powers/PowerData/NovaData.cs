using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NovaData", menuName = "PowerData/NovaData")]

public class NovaData : PowerData
{   
    [Tooltip("The radius of the nova")]
    public int radius;

    [Tooltip("Damage taken by each enemy hit by the nova")]
    public int damage;

    [Tooltip("Duration of the nova")]
    public int duration;

    [SerializeField]
    [Tooltip("The prefab of the nova area")]
    public GameObject novaAreaPrefab;

    public override string ToString()
    {
        return string.Format("NovaData : radius = {0}, damage = {1}, duration = {2}, cooldown = {3}, total charges = {4}", radius, damage, duration, cooldown, totalCharges);
    }
}
