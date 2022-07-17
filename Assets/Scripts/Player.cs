using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


public class Player : MonoBehaviour
{
    enum States { onFoot, onFly, onWait }
    public int hp;

    public Power currentPower;

    [HideInInspector]
    public List<PowerEnum> availablePowers;

    private System.Random rnd = new System.Random();


    [SerializeField] int indexOfPrefab;
    public static event Action<int> ThePlayerSpawns;
    States playerState;
    private CircleCollider2D detection;
    private PlayerMovement playerMovement;
    public Transform playerTransform;

    private float duration = 2f;

    public int currentFace;


    [SerializeField]
    public List<PowerData> listPowerPrefabs;

    void Awake()
    {
        playerState = States.onFoot;
        detection = gameObject.GetComponent<CircleCollider2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerTransform = gameObject.GetComponent<Transform>();

        availablePowers = new List<PowerEnum>() { PowerEnum.Nova, PowerEnum.Shotgun, PowerEnum.Boomerang, PowerEnum.Dash, PowerEnum.Sword, PowerEnum.Machinegun };
    }



    void Start()
    {
        Roll();

        ThePlayerSpawns?.Invoke(indexOfPrefab);
    }

    public void Yeet(InputAction.CallbackContext context) //Se mettre en position d'attente du Yeet
    {
        if (playerState == States.onFoot)
        {
            playerState = States.onWait;
            playerMovement.setSpeed(0);
        }
        if (context.canceled)
        {
            playerState = States.onFoot;
            playerMovement.setSpeed(playerMovement.maxSpeed);
        }
    }

    private void Update()
    {
        //print(playerState);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerState == States.onWait && collision.CompareTag("Player"))
        {
            float[] direction = playerMovement.getDirection();
            Player[] players = collision.GetComponents<Player>();
            Player otherPlayer = players.First(x => x != this);
            otherPlayer.playerState = States.onFly;
            otherPlayer.StartCoroutine(otherPlayer.Fly(new Vector2(otherPlayer.playerTransform.position.x, otherPlayer.playerTransform.position.y),
                new Vector2(otherPlayer.playerTransform.position.x + direction[0] * 5, otherPlayer.playerTransform.position.y + direction[1] * 2)));
            playerState = States.onFoot;

        }
    }
        //print(this.GetComponent<PlayerInput>().currentControlScheme);

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector2.Lerp(start, end, t);

        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));

    }


    public void Roll()
    {
        currentFace = rnd.Next(0, availablePowers.Count); //Next(int x, int y) returns a value between x and y, upper bound excluded.
        Debug.LogFormat("Cx : {0} rolled {1}", this.gameObject.name, availablePowers[currentFace]);
        currentPower = Power.GetPower(availablePowers[currentFace], listPowerPrefabs);
    }


    private IEnumerator Fly(Vector2 start, Vector2 finish)
    {

        float animation = 0f;
        while (animation < duration)
        {
            animation += Time.deltaTime;
            transform.position = Parabola(start, finish, duration, animation / duration);
            yield return null;
        }
        playerState = States.onFoot;
        Roll();
        yield return null;
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (currentPower != null && currentPower.currentCharges > 0)
            {
                currentPower.currentCharges--;
                currentPower.ActivateOnce(this);
            }
            else
            {
                Roll();
            }
        }

    }

}




