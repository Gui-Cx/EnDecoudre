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
        Player.ThePlayerSpawns += OnReceptionOfSignal;

        var p1 = PlayerInput.Instantiate(playersPrefab[0], controlScheme: "Keyboard0", pairWithDevice: Keyboard.current);
        var p2 = PlayerInput.Instantiate(playersPrefab[1], controlScheme: "Keyboard1", pairWithDevice: Keyboard.current);
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
        /*
        indexOtherPrefab = (indexOfPrefab == 0) ? 1 : 0;
        GameObject player = GameObject.Find("Player" + indexOfPrefab + "(Clone)");
        Debug.Log(player.name);
        PlayerInput playerInput = player.GetComponent<PlayerInput>();
        //playerInput.SwitchCurrentControlScheme("Keyboard" + indexOfPrefab);
        //playersInput[indexOfPrefab].actions.Enable();

        //playersInput[indexOfPrefab].actions.FindActionMap("Player" + indexOfPrefab).Enable();

        //playerInput.SwitchCurrentActionMap("Player"+indexOfPrefab);
        //playerInput.actions.FindActionMap("Player" + indexOtherPrefab).Disable();
        */
    }

}
