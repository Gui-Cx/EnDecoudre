using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterEnum { Monster1, Monster2, Monster3 };
public class Spawner : MonoBehaviour
{
    [Serializable] public struct wave { public int nbMonster1; public int nbMonster2; public int nbMonster3; }; //une vague
    [SerializeField] Spawn[] spawns; //Liste des points de spawns
    [SerializeField] wave[] waves; //liste des vagues
    [SerializeField] public GameObject Monster1;
    [SerializeField] public GameObject Monster2;
    [SerializeField] public GameObject Monster3;

    public static event Action wavesFinish;

    int nbFinishSpawn; //spawn fini 
    int indexWave; //vague actuelle

    private void Awake()
    {
        indexWave = 0;
        nbFinishSpawn = 0;
    }

    private void Start()
    {
        foreach(Spawn spawn in spawns)
        {
            spawn.spawner = this;
        }
        giveMonster();

    }

    public void Decompt()
    {
        nbFinishSpawn += 1;
        if(nbFinishSpawn == spawns.Length)
        {
            nbFinishSpawn = 0;
            indexWave += 1;
            if (indexWave >= waves.Length)
            {
                wavesFinish?.Invoke();

                SoundAssets.instance.PlayOpenDoor();

            }
            else
            {
                giveMonster();
                if(waves[indexWave].nbMonster1==0 && waves[indexWave].nbMonster2 == 0 && waves[indexWave].nbMonster1 == 0)
                {
                    indexWave++; //la vague était vide
                    if (indexWave >= waves.Length)
                    {
                        //signal d'ouvrir la porte 
                        wavesFinish?.Invoke();
                        SoundAssets.instance.PlayOpenDoor();

                    }
                    else
                    {
                        giveMonster();
                        Check();
                    }
                }
                Check();
            }
        }
    }

    private void Check()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            if (waves[indexWave].nbMonster1 <= i && waves[indexWave].nbMonster2 <= i && waves[indexWave].nbMonster1 <= i)
            {
                Decompt();
            }
        }
    }

    //reparti les monstres dans les points de spawns
    private void giveMonster()
    {
        int nbSpawn= spawns.Length;
        for (int j = 0; j < waves[indexWave].nbMonster1; j++)
        {
            spawns[j % nbSpawn].fillMonster(MonsterEnum.Monster1);

        }
        for (int j = 0; j < waves[indexWave].nbMonster2; j++)
        {
            spawns[j % nbSpawn].fillMonster(MonsterEnum.Monster2);

        }
        for (int j = 0; j < waves[indexWave].nbMonster3; j++)
        {
            spawns[j % nbSpawn].fillMonster(MonsterEnum.Monster3);

        }
        for(int i = 0; i < nbSpawn; i++)
        {
            spawns[i].StartSpawn();
        }

    }
}