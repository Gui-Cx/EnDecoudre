using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    List<MonsterEnum> monsters; //monster a faire spawn
    int alive; //nombre encore en vie
    float cooldown; //cooldown entre chaque spawn
    public Spawner spawner;
    private bool finish; //tout est spawn

    private void Awake()
    {
        alive = 0;
        monsters = new List<MonsterEnum>();
        finish = false;
    }
    

    public void Decompt()
    {
        alive -= 1;
        if (alive == 0 && finish)
        {
            Reset();
            spawner.Decompt();
            
        }
    }

    //Fin d'une vague
    private void Reset()
    {
        monsters.Clear();
        finish= false;
        alive = 0;
    }

    //Debut d'une nouvelle vague
    public void fillMonster(MonsterEnum monster)
    {
        monsters.Add(monster);
    }

    public void StartSpawn()
    {
        StartCoroutine(Spawning());

    }

    private IEnumerator Spawning()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            switch (monsters[i])
            {
                case MonsterEnum.Monster1:
                    GameObject gameObject1 = Instantiate(spawner.Monster1);
                    gameObject1.GetComponent<Monster>().spawn = this;
                    break;
                case MonsterEnum.Monster2:
                    GameObject gameObject2 = Instantiate(spawner.Monster2);
                    gameObject2.GetComponent<Monster>().spawn = this;
                    break;
                case MonsterEnum.Monster3:
                    GameObject gameObject3 = Instantiate(spawner.Monster3);
                    gameObject3.GetComponent<Monster>().spawn = this;

                    break;
            }
            alive++;
            yield return new WaitForSeconds(1);
        }
        finish = true;
        yield return null;

    }
}
