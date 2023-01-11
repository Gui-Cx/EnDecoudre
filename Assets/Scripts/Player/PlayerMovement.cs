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

    private int indexOfPrefab;
    private bool onFly;
    
    /// <summary>
    /// At any moment, (inputXTmp, inputYTmp) is the vector representing the direction in which the die faces. 
    /// inputXTmp = -1 if the die faces left;
    /// inputXTmp = 0 if the die faces up or down;
    /// inputXTmp = 1 if the die faces right.    
    /// </summary>
    public float inputXTmp = 0;
        /// <summary>

    /// At any moment, (inputXTmp, inputYTmp) is the vector representing the direction in which the die faces. 
    /// inputYTmp = -1 if the die faces down;
    /// inputYTmp = 0 if the die faces left or right;
    /// inputYTmp = 1 if the die faces up.    
    /// </summary>

    public float inputYTmp = -1;
    private Vector2 moveDirection;

    [SerializeField] private float moveSpeed;
    private bool isMoving;

    public bool isControllable;

    private Player player;

    #region Dash
    Dash dashPower;

    private float dashElapsedTime;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        indexOfPrefab = this.GetComponent<Player>().getIndexOfPrefab();
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        moveSpeed = maxSpeed;
        player = this.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.State == GameManager.GameState.InGame)
        {
            if (player.playerState == States.Dashing)
            {
                dashElapsedTime += Time.deltaTime;
                rb.velocity = new Vector2(inputXTmp, inputYTmp) * dashPower.dashSpeed;
                dashPower.DashFrame(dashElapsedTime);

            }
            else
            {
                moveDirection = new Vector2(inputX, inputY).normalized;
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
                isMoving = !(inputX == 0 && inputY == 0);
                //print(isMoving);
                anim.SetBool("isMoving", isMoving);

                if (isMoving)
                {
                    if (!onFly)
                    {
                        SoundAssets.instance.PlayFootstep();
                    }
                    anim.SetFloat("inputX", inputX);
                    anim.SetFloat("inputY", inputY);
                    inputXTmp = inputX;
                    inputYTmp = inputY;
                }
                else
                {
                    anim.SetFloat("inputX", inputXTmp);
                    anim.SetFloat("inputY", inputYTmp);
                }
            }
        }
    }

    public void SetOnFly(bool value)
    {
        onFly = value;
    }

    public void SetInput(float inputX, float inputY)
    {
        this.inputX = inputX;
        this.inputY = inputY;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (!onFly)
        {
            inputX = context.ReadValue<Vector2>().x;
            inputY = context.ReadValue<Vector2>().y;
        }
    }

    public float[] getDirection()
    {
        return new float[] { inputXTmp, inputYTmp };

    }

    public void setSpeed(float value)
    {
        moveSpeed = value;
    }

    public void InitDashMovement(Dash dash)
    {
        dashPower = dash;
        player.playerState = States.Dashing;
        dashElapsedTime = 0f;

    }

}