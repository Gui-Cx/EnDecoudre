using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Nova : Power
{

    /// <summary>
    /// Fields are
    /// int radius : The radius of the nova
    /// int damage : Damage taken by each enemy hit by the nova
    /// int duration : Duration of the nova
    /// int total charges : the number of attack before this power is unusable
    /// float cooldown : the time between two attacks 
    /// </summary>
    public NovaData novaData;

    GameObject novaAreaGO;

    public override void ActivateOnce(Player player){
        Debug.LogFormat("Nova {0}/{1} : {2}", totalCharges-currentCharges, totalCharges, novaData.ToString());
        SoundAssets.instance.PlayNova();
        novaAreaGO = GameObject.Instantiate(novaData.novaAreaPrefab, player.transform);
        GameObject effect = GameObject.Instantiate(novaData.novaEffect, player.transform.position, novaData.novaEffect.transform.rotation);
        GameObject.Destroy(effect, 0.5f);
        // novaAreaGO.GetComponent<NovaArea>().Init(this);
    }

    public Nova(PowerData powerData, Player playerArg) : base (powerData, playerArg){
        novaData = (powerData as NovaData);
    }
}
