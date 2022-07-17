using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Power
{
    /// <summary>
    /// Fields are :
    /// int distance : The maximal distance of the boomerang
    /// int duration : The duration for the boomerang to reach its maximal distance
    /// int damage : The damage taken by each enemy hit by the boomerang
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    public BoomerangData boomerangData;

    public override void ActivateOnce(Player player)
    {
        Debug.LogFormat("Boomerang {0}/{1} : {2}", totalCharges-currentCharges, totalCharges, boomerangData.ToString());
        Vector2 directionToTarget = new Vector2(player.gameObject.GetComponent<PlayerMovement>().getDirection()[0],
        player.gameObject.GetComponent<PlayerMovement>().getDirection()[1]);
        if (directionToTarget.x != 0 && directionToTarget.y != 0)
        {
            directionToTarget = new Vector2(directionToTarget.x, 0);
        }
        float angle = Vector3.Angle(Vector3.right, directionToTarget);
        if (player.gameObject.GetComponent<PlayerMovement>().getDirection()[1] < 0) angle *= -1;
        Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        GameObject bullet = GameObject.Instantiate(boomerangData.bulletPrefab, player.transform.position, bulletRotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    }

    public Boomerang(PowerData powerData, Player playerArg) : base(powerData, playerArg)
    {
        boomerangData = powerData as BoomerangData;
    }
}
