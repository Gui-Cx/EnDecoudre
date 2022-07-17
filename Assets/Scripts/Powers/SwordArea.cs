using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordArea : MonoBehaviour
{
    Sword swordPower;

    public CircleCollider2D circleCollider;

    public PlayerMovement playerMovement;

    public void Start()
    {
        swordPower = gameObject.GetComponentInParent<Player>().currentPower as Sword;
        circleCollider.radius = swordPower.swordData.radius;
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals("Monster"))
        {
            Vector3 temp = (collider.transform.position - swordPower.player.transform.position);
            temp.Normalize();
            Vector2 playerToCollider = new Vector2(temp.x, temp.y);
            float angle = Vector2.Angle(playerMovement.GetForwardDirection(), playerToCollider);
            if (Mathf.Abs(angle) < swordPower.swordData.angle)
            {
                Debug.LogFormat("Cx : Enemy {0} hit by sword from {1}", collider.gameObject.name, swordPower.player.name);
                collider.GetComponent<Monster>()?.loseHP(swordPower.swordData.damage);
            } else {
                Debug.LogFormat("Cx : Enemy {0} not hit by sword from {1}", collider.gameObject.name, swordPower.player.name);
            }
        } else if (collider.gameObject.CompareTag("Box")){
            collider.gameObject.GetComponent<DestructibleBox>().DestroyBox();
        }
    }
}
