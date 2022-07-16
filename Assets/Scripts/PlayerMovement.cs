using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    private Animator anim;
    private float inputX;
    private float inputY;
    [SerializeField] public float maxSpeed;
    private Vector2 moveDirection;
    private float moveSpeed;



    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        moveSpeed = maxSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        anim.SetFloat("inputX", inputX);
        anim.SetFloat("inputY", inputY);
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        inputY = context.ReadValue<Vector2>().y;
        moveDirection = new Vector2(inputX, inputY).normalized;

    }

    public float[] getDirection()
    {
        return new float[] { inputX, inputY };
    }

    public void setSpeed(float value)
    {
        moveSpeed = value;
    }

}