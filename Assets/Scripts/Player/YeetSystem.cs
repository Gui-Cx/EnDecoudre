using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class YeetSystem : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Awake()
    {
        player = transform.parent.GetComponent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.playerState == States.Waiting && collision.CompareTag("Player"))
        {
            Player[] players = collision.GetComponents<Player>();
            Player otherPlayer = players.First(x => x != this); //On recupere l'autre joueur
            player.MakeFly(otherPlayer);
        }
    }
}
