using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Power
{
    /// <summary>
    /// Fields are
    /// int radius : The radius of the circular arc described by the sword
    /// int angle : Enemies between -angle and +angle will be damaged, with 0 being the forward direction of the player
    /// int damage : Damage taken by each enemy hit by the attack
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    public SwordData swordData;
    
    GameObject swordAreaGO;

    public override void ActivateOnce(Player player)
    {
        Debug.LogFormat("Attack {0}/{1} : {2}", totalCharges-currentCharges, totalCharges, swordData.ToString());
        SoundAssets.instance.PlaySword();
        swordAreaGO = GameObject.Instantiate(swordData.swordAreaPrefab, player.transform);
        GameObject effect = GameObject.Instantiate(swordData.swordEffect, player.transform.position+player.transform.forward*0.5f, rotation:Quaternion.identity, player.transform);
        Animator animator = effect.GetComponent<Animator>();
        //TODO : Trouver comment obtenir la direction dans laquelle se trouve le personnage
        animator.SetFloat("inputX", player.playerMovement.inputXTmp);
        animator.SetFloat("inputY", player.playerMovement.inputYTmp);
        GameObject.Destroy(effect, swordData.duration);

        // animator.Play("Base Layer");
    }

    public Sword(PowerData powerData, Player playerArg) : base(powerData, playerArg)
    {
        swordData = powerData as SwordData;
    }
}
