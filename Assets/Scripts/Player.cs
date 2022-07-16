using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    enum States { onFoot, onFly, onWait }
    [SerializeField] int indexOfPrefab;
    public static event Action<int> ThePlayerSpawns;
    States playerState;
    void Awake()
    {
        playerState = States.onFoot; }
{



    // Start is called before the first frame update
    void Start()
    {
        ThePlayerSpawns?.Invoke(indexOfPrefab);

    }

    void Update()
    {

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




}
