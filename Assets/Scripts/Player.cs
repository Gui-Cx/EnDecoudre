using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum States { onFoot, onFly, onWait }
    States playerState;
    void Awake()
    {
        playerState = States.onFoot;
    }

    void Update()
    {
        if(playerState == States.onWait)
        {
            //speed = 0
        }
    }

    private void Yeet() //Se mettre en position d'attente du Yeet
    {
        if (playerState == States.onFoot)
        {
            playerState = States.onWait; //faire en sorte qu'il ne puisse pas se déplacer
        }
    }

    public void onYeet(string orientation) //Quand l'autre joueur nous yeet
    {
        playerState = States.onFly;
        //deplacement du joueur selon l'orientation
        playerState = States.onFoot;
    }



    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (playerState == States.onWait && collision.tag == "Player")
        {
            Player otherPlayer = collision.GetComponent<Player>();
            otherPlayer.onYeet("yes");
            playerState = States.onFoot;
        }
    }
}
