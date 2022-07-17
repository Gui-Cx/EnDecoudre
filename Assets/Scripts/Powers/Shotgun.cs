using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Power
{
    /// <summary>
    /// Fields are :
    /// int radius : The radius of the shot
    /// int angle : Enemies between -angle and +angle will be damaged, with 0 being the forward direction of the player
    /// int damage : The damage taken by each enemy hit by the cone
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    public ShotgunData shotgunData;

    public override void ActivateOnce(Player player)
    {
        Debug.LogFormat("Shotgun {0}/{1} : {2}", totalCharges - currentCharges, totalCharges, shotgunData.ToString());
          Vector2 directionToTarget = new Vector2(player.gameObject.GetComponent<PlayerMovement>().getDirection()[0],
                player.gameObject.GetComponent<PlayerMovement>().getDirection()[1]);
            float angle = Vector3.Angle(Vector3.right, directionToTarget);
            if (player.gameObject.GetComponent<PlayerMovement>().getDirection()[1] < player.transform.position.y) angle *= -1;
            Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject bullet = GameObject.Instantiate(shotgunData.bulletPrefab, player.transform.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

    }

    public Shotgun(PowerData powerData, Player playerArg) : base(powerData, playerArg)
    {
        shotgunData = powerData as ShotgunData;
    }
}
