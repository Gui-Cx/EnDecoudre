using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private float speed;
    [SerializeField] private float cooldown;
    [SerializeField] private float distanceAttack;

    Player[] players;
    public Spawn spawn;
    public enum MonsterStates { Hiting,Reaching,Fleeing }
    protected MonsterStates monsterState;
    private Player player; //cible
    [SerializeField] float distance; //pour pouvoir frappe
    void Start()
    {
        monsterState = MonsterStates.Reaching;
        players = GetComponents<Player>();
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

    private void attack()
    {
        if (monsterState == MonsterStates.Hiting)
        {
            StartCoroutine(doAttack());
        }
    }

    private void reach()
    {


    }
    private void LateUpdate()
    {
        attack();
    }

    public virtual IEnumerator doAttack()
    {
        //attack
        //cooldown
        yield return null;
    }

    public void calculDistance()
    {
        float[] distance = { -1f,-1f};
        foreach(Player player in players)
        {
            Vector2 Pos1 = transform.position;
            Vector2 Pos2 = player.transform.position;
            float x1 = Pos1.x, x2 = Pos2.x, y1 = Pos1.y, y2 = Pos2.y;
            float xDif = Mathf.Abs((Mathf.Max(x1, x2) - Mathf.Min(x1, x2)));
            float yDif = Mathf.Abs((Mathf.Max(y1, y2) - Mathf.Min(y1, y2)));
            float finalDistance = Mathf.Sqrt(xDif * xDif + yDif * yDif);
        }
    }
}
