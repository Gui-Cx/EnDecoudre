using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class SwitchCharacters : MonoBehaviour
{
    PlayerInputManager playerManager;
    [SerializeField] GameObject[] playersPrefab;
    PlayerInput[] playersInput;
    int current;
    int tmpCurrent;
    int indexOtherPrefab;
    // Start is called before the first frame update
    void Start()
    {
        playerManager=this.GetComponent<PlayerInputManager>();
        //playersInput = new PlayerInput[2] {playersPrefab[0].GetComponent<PlayerInput>(), playersPrefab[1].GetComponent<PlayerInput>() };

        Player.ThePlayerSpawns += OnReceptionOfSignal;


        //playersInput[0].actions.FindActionMap("Player0").Enable();
        //playersInput[0].actions.FindActionMap("Player1").Disable();
        //playersInput[1].actions.FindActionMap("Player1").Enable();
        //playersInput[1].actions.FindActionMap("Player0").Disable();


        //playersInput[0].SwitchCurrentActionMap("Player0");
        //playersInput[1].SwitchCurrentActionMap("Player1");


    }

    // Update is called once per frame
    void Update()
    {
        current = (playerManager.playerCount == 2) ? 0 : playerManager.playerCount;
        playerManager.playerPrefab = playersPrefab[current];

 

    }

    public int GetPlayerCount()
    {
        return playerManager.playerCount;
    }

    private void OnReceptionOfSignal(int indexOfPrefab)
    {
        indexOtherPrefab = (indexOfPrefab == 0) ? 1 : 0;
        //GameObject player = GameObject.Find("Player" + indexOfPrefab + "(Clone)");
        //Debug.Log(player.name);
        //PlayerInput playerInput = player.GetComponent<PlayerInput>();
        //playerInput.SwitchCurrentControlScheme("Keyboard" + indexOfPrefab);
        //playersInput[indexOfPrefab].actions.Enable();

        //playersInput[indexOfPrefab].actions.FindActionMap("Player" + indexOfPrefab).Enable();

        //playerInput.SwitchCurrentActionMap("Player"+indexOfPrefab);
        //playerInput.actions.FindActionMap("Player" + indexOtherPrefab).Disable();
    }

}
