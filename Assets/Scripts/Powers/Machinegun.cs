using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Machinegun : Power
{
    ///<summary>
    /// The fields are
    ///int spread : The angle theta of the spread. Each volley of bullets will be spread evenly between -theta and +theta, 0 being the forward direction of the player
    ///int bulletPerCharge : The number of bullet per volley. They will be spread evenly between -spread and +spread
    ///int damagePerBullet : The number of damage each enemy hit by a bullet will take 
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    ///</summary>
    MachinegunData mgData;
    public override void ActivateOnce(Player player)
    {
        for (int i = 0; i < 3; i++)
        {
            Debug.LogFormat("MachineGun {0}/{1} : {2}", totalCharges - currentCharges, totalCharges, mgData.ToString());
            Vector2 directionToTarget = new Vector2(player.gameObject.GetComponent<PlayerMovement>().getDirection()[0],
          player.gameObject.GetComponent<PlayerMovement>().getDirection()[1]);
            float angle;
            if (i == 0)
            {
                angle = Vector3.Angle(Vector3.right, directionToTarget) + 10;
            }
            else if (i == 1)
            {
                angle = Vector3.Angle(Vector3.right, directionToTarget);

            }
            else{
                angle = Vector3.Angle(Vector3.right, directionToTarget) - 10;

            }
            if (player.gameObject.GetComponent<PlayerMovement>().getDirection()[1] < player.transform.position.y) angle *= -1;
            Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            GameObject bullet = GameObject.Instantiate(mgData.bulletPrefab, player.transform.position, bulletRotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        }


    }

    public Machinegun(PowerData powerData) : base(powerData){
        mgData = powerData as MachinegunData; 
    }
}
