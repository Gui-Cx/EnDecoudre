using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] private float speed;

    private void Awake()
    {
        StartCoroutine(lifeTime());
    }
    private void Update()
    {
        transform.Translate(transform.right * speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    private IEnumerator lifeTime()
    {
        yield return new WaitForSeconds(10);
        Destroy(this);
        yield return null;
    }
}
