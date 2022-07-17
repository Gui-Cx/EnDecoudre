using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class YeetSystem : MonoBehaviour
{

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (player.playerState == Player.States.onWait && collision.CompareTag("Player"))
        {
            float[] direction = player.playerMovement.getDirection();
            Player[] players = collision.GetComponents<Player>();
            Player otherPlayer = players.First(x => x != this);
            otherPlayer.playerState = Player.States.onFly;
            otherPlayer.StartCoroutine(otherPlayer.Fly(new Vector2(otherPlayer.playerTransform.position.x, otherPlayer.playerTransform.position.y),
                new Vector2(otherPlayer.playerTransform.position.x + direction[0] * 5, otherPlayer.playerTransform.position.y + direction[1] * 2)));
            player.playerState = Player.States.onFoot;
        }
    }
}
