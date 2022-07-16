using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public int hp;

    public Power currentPower;

    public List<PowerEnum> availablePowers;

    private System.Random rnd = new System.Random();

    void Awake(){
        availablePowers = new List<PowerEnum>(){PowerEnum.Nova, PowerEnum.Dash, PowerEnum.Boomerang, PowerEnum.Sword};
        currentPower = Roll();
    }

    //Start 
    void Start()
    {}

    private Power Roll()
    {
        int val = rnd.Next(0, availablePowers.Count-1); //Next(int x, int y) returns a value between x and y, both included.
        Power power = Power.GetPower(availablePowers[val]);
        return power;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentPower != null && currentPower.currentCharges > 0)
            {
                Debug.LogFormat("Cx : Remaining charges before firing = {0}", currentPower.currentCharges);
                currentPower.currentCharges--;
                currentPower.ActivateOnce(this);
            }
            else
            {
                Debug.LogFormat("Cx : Can't shoot, choosing new weapon!");
                currentPower = Roll();
            }
        }
    }
}
