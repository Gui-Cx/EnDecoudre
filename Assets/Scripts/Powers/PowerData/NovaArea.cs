using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NovaArea : MonoBehaviour
{    
    public Nova novaPower;
    public NovaData novaData;
    private float elapsedTime;

    public CircleCollider2D circleCollider;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > novaData.duration){
            Destroy(this);
        }
    }

    public void Start(){
        novaPower = gameObject.GetComponentInParent<Player>().currentPower as Nova;
        circleCollider.radius = novaPower.novaData.radius;
    }

    public void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag.Equals("Monster"))
            {
                Debug.LogFormat("Cx : Enemy {0} hit by nova from {1}", collider.gameObject.name, novaPower.player.name);
                collider.GetComponent<Monster>()?.loseHP(novaData.damage);
            }

    }
}
