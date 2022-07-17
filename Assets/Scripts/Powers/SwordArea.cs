using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordArea : MonoBehaviour
{   
    Sword swordPower;

    public void Start()
    {
        swordPower = gameObject.GetComponentInParent<Player>().currentPower as Sword;
        //circleCollider.radius = swordPower.novaData.radius;
    }
}
