using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Power
{
    /// <summary>
    /// Fields are :
    /// int duration : The duration of the dash
    /// int distance : The maximal distance of the dash
    /// int movingDamageHeight : The height of the rectangular hitbox during the dash (height = player's front-back axis)  
    /// int movingDamageWidth : The width of the rectangular hitbox during the dash (width = player's left-right axis)
    /// int endDamageRadius : The radius of the circular hitbox at the end of the dash 
    /// int movingDamage : Damage taken by each enemy hit during the dash
    /// int endDamage : Damage take by each enemy hit at the end of the dash
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    public DashData dashData;

    public float elapsedTime;

    public float dashSpeed;

    public PlayerMovement playerMovement;

    //private TrailRenderer trailRenderer;

    public override void ActivateOnce(Player player)
    {
        //Debug.LogFormat("Dash {0}/{1} : {2}", totalCharges - currentCharges, totalCharges, dashData.ToString());
        Debug.LogFormat("Dash {0}/{1}", totalCharges - currentCharges, totalCharges);
        player.playerState = States.Dashing;
        //trailRenderer.emitting = true;
        playerMovement.InitDashMovement(this);
    }
    public Dash(PowerData powerData, Player playerArg) : base(powerData, playerArg)
    {
        dashData = powerData as DashData;
        dashSpeed = dashData.distance / dashData.duration;
        playerMovement = playerArg.gameObject.GetComponent<PlayerMovement>();
        //trailRenderer = player.GetComponent<TrailRenderer>();
    }
    public void DashFrame(float elapsedTime)
    {
        List<RaycastHit2D> results = new List<RaycastHit2D>();
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        contactFilter2D.NoFilter();
        if (elapsedTime < dashData.duration)
        {

            // Physics2D.BoxCast(player.transform.position, new Vector3(dashData.movingDamageWidth/2, dashData.movingDamageHeight/2, 1), player.transform.forward, out rayHit ,player.transform.rotation);
            Physics2D.BoxCast(player.transform.position, new Vector2(dashData.movingDamageWidth / 2, dashData.movingDamageHeight / 2), player.transform.rotation.z, new Vector2(1, 1), contactFilter2D, results, 0);
        }
        else if (elapsedTime > dashData.duration)
        {
            Physics2D.CircleCast(player.transform.position, dashData.endDamageRadius, direction: new Vector2(1, 1), contactFilter2D, results);
            player.playerState = States.OnFoot;
            //trailRenderer.emitting = false;
        }
        foreach (var enemy in results)
        {
            if (enemy.collider.gameObject.tag.Equals("Monster"))
            {
                enemy.collider.GetComponent<Monster>()?.loseHP((elapsedTime < dashData.duration) ? dashData.movingDamage : dashData.endDamage );
            }
        }
    }
}
