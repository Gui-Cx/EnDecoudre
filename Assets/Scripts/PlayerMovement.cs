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
    private Vector2 moveDirection;
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        rb.velocity = new Vector2(moveDirection.x , moveDirection.y) * moveSpeed;
        anim.SetFloat("inputX", inputX);
        anim.SetFloat("inputY", inputY);
    }

    public void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x;
        inputY = context.ReadValue<Vector2>().y;
        moveDirection = new Vector2(inputX, inputY).normalized;
        
    }
    
    public float[] Direction()
    {
        return new float[] { inputX, inputY };
    }
}
