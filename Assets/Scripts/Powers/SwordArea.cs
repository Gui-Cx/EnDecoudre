using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordArea : MonoBehaviour
{
    Sword swordPower;

    public CircleCollider2D circleCollider;

    public PlayerMovement playerMovement;

    private float elapsedTime;

    public void Start()
    {
        swordPower = gameObject.GetComponentInParent<Player>().currentPower as Sword;
        circleCollider.radius = swordPower.swordData.radius;
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > swordPower.swordData.duration)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {

        Vector3 temp = (collider.transform.position - swordPower.player.transform.position);
        temp.Normalize();
        Vector2 playerToCollider = new Vector2(temp.x, temp.y);
        float[] playerForwardArray = playerMovement.getDirection();
        Vector2 playerForward = new Vector2(playerForwardArray[0], playerForwardArray[1]);
        float angle = Vector2.Angle(playerForward, playerToCollider);
        Debug.LogFormat("Cx : angle = {0}", angle);
        if (Mathf.Abs(angle) < swordPower.swordData.angle)
        {
            if (collider.gameObject.tag.Equals("Monster"))
            {
                Debug.LogFormat("Cx : Enemy {0} hit by sword from {1}", collider.gameObject.name, swordPower.player.name);
                collider.GetComponent<Monster>()?.loseHP(swordPower.swordData.damage);
            }
            else if (collider.gameObject.GetComponent<DestructibleBox>() != null)
            {
                collider.gameObject.GetComponent<DestructibleBox>().DestroyBox();
            }
        }
        else
        {
            Debug.LogFormat("Cx : Enemy {0} not hit by sword from {1}", collider.gameObject.name, swordPower.player.name);
        }

    }
}
