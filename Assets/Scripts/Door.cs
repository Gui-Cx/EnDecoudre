using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator anim;
    private BoxCollider2D collider;

    private void Awake()
    {
        collider = gameObject.GetComponent<BoxCollider2D>();
    }
    public void open()
    {
        collider.enabled = false;
        anim.SetTrigger("IsOpen");
        // remove collider
    }

}
