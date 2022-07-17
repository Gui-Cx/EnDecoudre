using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected float speed;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float distanceAttack;
    [SerializeField] protected float distanceDecay;
    [SerializeField] protected int degat;

    Player[] players;
    public Spawn spawn;
    public enum MonsterStates { Hiting,Reaching,Fleeing }

    protected MonsterStates monsterState;
    private Player player; //cible
    void Start()
    {
        monsterState = MonsterStates.Reaching;
        players = GameObject.FindGameObjectsWithTag("Player").Select(x => x.GetComponent<Player>()).ToArray();
    }

    private void loseHP(int value)
    {
        health -= value;
        if(health <= 0)
        {
            spawn.Decompt();
            Destroy(this.gameObject);
        }
    }

    private void reach(Player player)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime);
    }

    private void fle(Player player)
    {
        transform.position = Vector2.MoveTowards(transform.position, -player.transform.position, speed * Time.deltaTime);
    }

    private void LateUpdate()
    {
        calculDistance();
    }

    public virtual IEnumerator doAttack(Player player)
    {
        //attack
        //cooldown
        yield return null;
    }

    public void calculDistance()
    {
        float minDistance;
        Player cible;
        float[] distance = { -1f,-1f};
        for(int i =0;i<2;i++)
        {
            Vector2 Pos1 = transform.position;
            Vector2 Pos2 = players[i].transform.position;
            float x1 = Pos1.x, x2 = Pos2.x, y1 = Pos1.y, y2 = Pos2.y;
            float xDif = Mathf.Abs((Mathf.Max(x1, x2) - Mathf.Min(x1, x2)));
            float yDif = Mathf.Abs((Mathf.Max(y1, y2) - Mathf.Min(y1, y2)));
            float finalDistance = Mathf.Sqrt(xDif * xDif + yDif * yDif);
            distance[i] = finalDistance;
        }
        if(distance[0]>=0 && (distance[0] <= distance[1]))
        {
            minDistance = distance[0];
            cible = players[0];
        }
        else
        {
            minDistance = distance[1];
            cible = players[1];
        }
        if (minDistance > distanceAttack)
        {
            monsterState = MonsterStates.Reaching;
            reach(cible);
        }
        else if (minDistance < distanceDecay){
            monsterState = MonsterStates.Fleeing;
            fle(cible);
        }
        else
        {
            if (monsterState!= MonsterStates.Hiting)
            {
                monsterState = MonsterStates.Hiting;
                StartCoroutine(doAttack(cible));
            }

        }

        
    }
}
