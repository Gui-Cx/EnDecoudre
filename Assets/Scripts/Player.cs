using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    public int hp;

    public Power currentPower;

    public List<PowerEnum> availablePowers;

    private System.Random rnd = new System.Random();

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Roll(){
        int val = rnd.Next(0,5); //Next(int x, int y) returns a value between x and y, both included.

    }
}
