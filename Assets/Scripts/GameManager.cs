using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        InGame,
        GameOver
    }

    public GameState State { get; private set; }

    private Player playerOne;
    private Player playerTwo;
    private UImanager uiManager;
    int playersAreDead;

    #region Singleton
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    #endregion

    private void Start()
    {
        State = GameState.Start;
        uiManager = GameObject.FindGameObjectWithTag("UImanager").GetComponent<UImanager>();
        SoundAssets.instance.PlayStartMusic();
        Player[] players = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<Player>()).ToArray();

        //playerOne = players[0];
        //playerTwo = players[1];
    }

    public void UpdateGameState(GameState _state)
    {
        if (State == _state)
            return;

        State = _state;
        switch (State)
        {
            case GameState.Start:
                break;
            case GameState.InGame:
                SoundAssets.instance.PlayMainMusic();
                break;
            case GameState.GameOver:
                SoundAssets.instance.PlayGameOverMusic();
                uiManager.TriggerGameOver(true);
                break;
        }
    }

    public void HandlePLayerDied()
    {
        playersAreDead--;
        if(playersAreDead == 0)
        {
            UpdateGameState(GameState.GameOver);
        }
    }

    public void HandlePlayerResurect()
    {
        playersAreDead++;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
