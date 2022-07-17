using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MachinegunData", menuName = "PowerData/MachinegunData")]

public class MachinegunData: PowerData
{
    [Tooltip("The angle theta of the spread. Each volley of bullets will be spread evenly between -theta and +theta, 0 being the forward direction of the player")]
    public int spread;

    [Tooltip("The number of bullet per volley. They will be spread evenly between -spread and +spread")]
    public int bulletPerCharge;

    [Tooltip("The number of damage each enemy hit by a bullet will take ")]
    public int damagePerBullet;

    public GameObject bulletPrefab;

    public override string ToString()
    {
        return string.Format("MachineGunData : spread = {0}, bulletPerCharge = {1}, damagePerBullet = {2}, cooldown = {3}, totalCharges = {4}", spread, bulletPerCharge, damagePerBullet, cooldown, totalCharges);
    }
}
