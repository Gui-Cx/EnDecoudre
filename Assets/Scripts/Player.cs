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
    [SerializeField] int maxHP;

    public Power currentPower;

    [HideInInspector]
    public List<PowerEnum> availablePowers;

    private System.Random rnd = new System.Random();


    [SerializeField] int indexOfPrefab;
    private int indexOfFace;
    public static event Action<int> ThePlayerSpawns;
    States playerState;
    private CircleCollider2D detection;
    private PlayerMovement playerMovement;
    private Transform playerTransform;
    Animator anim;

    private float duration = 2f;

    public int currentFace;


    [SerializeField]
    public List<PowerData> listPowerPrefabs;

    void Awake()
    {
        playerState = States.onFoot;
        anim = this.GetComponent<Animator>();
        detection = gameObject.GetComponent<CircleCollider2D>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerTransform = gameObject.GetComponent<Transform>();
        hp = maxHP;
        availablePowers = new List<PowerEnum>() { PowerEnum.Nova, PowerEnum.Shotgun, PowerEnum.Boomerang, PowerEnum.Dash, PowerEnum.Sword, PowerEnum.Machinegun };
    }



    void Start()
    {
        Roll();
        SoundAssets.instance.PlaySpawnSound();
        ThePlayerSpawns?.Invoke(indexOfPrefab);
    }

    public int getIndexOfPrefab()
    {
        return indexOfPrefab;
    }

    public void Yeet(InputAction.CallbackContext context) //Se mettre en position d'attente du Yeet
    {
        if (playerState == States.onFoot)
        {
            playerState = States.onWait;
            anim.SetBool("onWait", true);
            playerMovement.setSpeed(0);
        }
        if (context.canceled)
        {
            playerState = States.onFoot;
            anim.SetBool("onWait", false);
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
        //currentFace = rnd.Next(0, availablePowers.Count); //Next(int x, int y) returns a value between x and y, upper bound excluded.
        currentFace = (int)PowerEnum.Machinegun;
        Debug.LogFormat("Cx : {0} rolled {1}", this.gameObject.name, availablePowers[currentFace]);
        currentPower = Power.GetPower(availablePowers[currentFace], listPowerPrefabs);
        indexOfFace = currentFace + 1;
    }


    private IEnumerator Fly(Vector2 start, Vector2 finish)
    {
        this.GetComponent<PlayerMovement>().SetOnFly(true);
        SoundAssets.instance.PlayYeetSound(getIndexOfPrefab());
        float animation = 0f;
        anim.SetBool("onFly", true);
        // faut lancer ROLL pour que �a change la valeur de indexOfFace
        Roll();
        Debug.Log(indexOfFace);
        while (animation < duration)
        {
            animation += Time.deltaTime;
            transform.position = Parabola(start, finish, duration, animation / duration);
            anim.SetInteger("indexOfFace", indexOfFace);
            //lancer l'al�atoire entre 1 et 6 avec powers ? en gros tenir � jour une valeur int faceValue pour que d�s l'atterissage on soit dans la bonne animation
            yield return null;
        }
        this.GetComponent<PlayerMovement>().SetInput(0, 0);
        this.GetComponent<PlayerMovement>().SetOnFly(false);
        playerState = States.onFoot;
        anim.SetBool("onFly", false);
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


    private void die()
    {
        SoundAssets.instance.PlayPlayerDieSound(getIndexOfPrefab());
        print("isdie");
    }

    public void takeDamage(int value)
    {
        hp -= value;
        SoundAssets.instance.PlayTakeDamagePlayer(getIndexOfPrefab());
        print(hp);
        if( hp <= 0)
        {
            die();
        }
    }

}




