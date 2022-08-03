using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;

public enum States { OnFoot, Flying, Waiting, Dashing, Dead }

public class Player : MonoBehaviour
{
    //Life
    public int hp;
    [SerializeField] public int maxHP;

    //Power
    public Power currentPower;
    [HideInInspector]
    public List<PowerEnum> availablePowers;
    private System.Random rnd = new System.Random();
    private int indexOfFace;
    public int currentFace;
    [SerializeField]
    public List<PowerData> listPowerPrefabs;

    //Two Player
    [SerializeField] int indexOfPrefab;
    public static event Action<int> ThePlayerSpawns;

    //yeet
    GameObject deathBubble;
    GameObject throwBubble;
    private float _yeetDistanceX;
    private float _yeetDistanceY;
    private int _yeetIteration;

    //Animation and sprite
    Transform _groudCircle;
    Animator anim;
    private SpriteRenderer spriteRenderer;

    public States playerState;
    public PlayerMovement playerMovement;
    public Transform playerTransform;


    private bool isInvincible = false;
    [SerializeField]
    private float invincibilityDurationSeconds;
    [SerializeField]
    private float invincibilityDeltaTime;

    UImanager _uimanager;

    bool canFire; //for cooldown

    private float duration = 2f;



    void Awake()
    {
        _uimanager = GameObject.FindGameObjectWithTag("UImanager").GetComponent<UImanager>();
        playerState = States.OnFoot;
        anim = this.GetComponent<Animator>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
        playerTransform = gameObject.GetComponent<Transform>();
        deathBubble = transform.GetChild(1).gameObject;
        deathBubble.SetActive(false);
        throwBubble = transform.GetChild(2).gameObject;
        throwBubble.SetActive(false);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        _groudCircle = transform.GetChild(3);
        _yeetDistanceX = 5f;
        _yeetDistanceY = 3f;
        _yeetIteration = 5;
        hp = maxHP;
        canFire = true;
        availablePowers = new List<PowerEnum>() { PowerEnum.Nova, PowerEnum.Shotgun, PowerEnum.Boomerang, PowerEnum.Dash, PowerEnum.Sword, PowerEnum.Machinegun };

        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        Roll();
        _uimanager.SwapSkill(currentPower, this);
        _uimanager.UpdateShotCount(currentPower, this);
        if(indexOfPrefab==0)
            playerTransform.position = new Vector2(0, -3f);
        else
            playerTransform.position = new Vector2(5f, -3f);
        SoundAssets.instance.PlaySpawnSound();
        ThePlayerSpawns?.Invoke(indexOfPrefab);
    }

    public int getIndexOfPrefab()
    {
        return indexOfPrefab;
    }

    public void Yeet(InputAction.CallbackContext context) //Se mettre en position d'attente du Yeet
    {
        if (playerState == States.OnFoot)
        {
            playerState = States.Waiting;
            anim.SetBool("onWait", true); //Animation de la fl?che
            throwBubble.SetActive(true); //On active le CircleCollider qui detecte l'autre joueur pour le yeet
            playerMovement.setSpeed(0); //On ne peut pas bouger lors de l'attente
            StartCoroutine(ReviveCoroutine()); //A d?placer
        }
        if (context.canceled)
        {
            playerState = States.OnFoot; //On reintialise le joueur lorsqu'il relache le bouton
            anim.SetBool("onWait", false);
            throwBubble.SetActive(false);
            if (playerState != States.Dead)
            {
                playerMovement.setSpeed(playerMovement.maxSpeed);
            }
        }
    }

    public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        var mid = Vector2.Lerp(start, end, t);
        return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));

    }

    private static Vector2 GroundCircleDuringParabola(Vector2 start, Vector2 end, float height, float t)
    {
        return Vector2.Lerp(start, end, t);
    }

    public void Roll()
    {
        currentFace = rnd.Next(0, availablePowers.Count); //Next(int x, int y) returns a value between x and y, upper bound excluded.
        //currentFace = (int)PowerEnum.Machinegun;
        Debug.LogFormat("Cx : {0} rolled {1}", this.gameObject.name, availablePowers[currentFace]);
        currentPower = Power.GetPower(this, availablePowers[currentFace], listPowerPrefabs);
        indexOfFace = currentFace + 1;
    }

    private int CalculDistanceYeet(float[] direction)
    {
        int iter = _yeetIteration;
        Vector2 trajectoire = new Vector2(direction[0], direction[1]);
        int layerMask = ~((1 << 6) | (1 << 7) | (1 << 8));
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
            trajectoire,
            Mathf.Abs(direction[0] * _yeetDistanceX) + Mathf.Abs(direction[1] * _yeetDistanceY),
            layerMask);
        if (hit.collider != null) //Si il y a un mur sur la trajectoire 
        {
            //On it?re en r?duisant la taille du lanc?e, si on est trop proche on abandonne le yeet
            for (int i = _yeetIteration - 1; i > 0; i--)
            {
                iter = i;
                RaycastHit2D hitAgain = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y),
            trajectoire,
            Mathf.Abs(direction[0] * _yeetDistanceX * i / _yeetIteration) + Mathf.Abs(direction[1] * _yeetDistanceY * i / _yeetIteration),
            layerMask);
                if (!hitAgain.collider)
                {
                    break;
                }
            }
        }
        return iter;
    }

    private void SpriteScale(float progression)
    {
        progression = (progression < 0.5) ? (1 - progression) : progression;
        progression = (progression + 0.2f < 1f) ? progression + 0.2f : progression;
        transform.localScale = new Vector3(2 - progression, 2 - progression, 1);
    }

    public void MakeFly(Player otherPlayer) //Lance le joueur
    {
        float[] direction = playerMovement.getDirection();
        ///On test avec des raycasts si on peut lancer le joueur sans qu'il touche le mur
        ///S'il touche le mur, on essaie en divisant par i/_yeetiteration
        ///On yeet le joueur sur une distance o? l'on sait qu'il ne va pas traverser le mur
        ///todo : emp?cher le joueur de pouvoir indiquer le yeet si trop proche
        int distanceDivision = CalculDistanceYeet(direction);
        if (distanceDivision == 1) //Trop proche du mur, on abandonne
        {
            playerState = States.OnFoot;
            anim.SetBool("onWait", false);
        }
        else
        {
            otherPlayer.playerState = States.Flying;
            float _xYeet = direction[0] * _yeetDistanceX * (float)((float)distanceDivision / (float)_yeetIteration) -0.1f;
            float _yYeet = direction[1] * _yeetDistanceY * (float)((float)distanceDivision / (float)_yeetIteration)-0.1f;
            otherPlayer.StartCoroutine(otherPlayer.Fly(
                new Vector2(otherPlayer.playerTransform.position.x, otherPlayer.playerTransform.position.y),
                new Vector2(otherPlayer.playerTransform.position.x + _xYeet , otherPlayer.playerTransform.position.y + _yYeet)));
        }
        playerState = States.OnFoot;
    }
    public IEnumerator Fly(Vector2 start, Vector2 finish) //Coroutine calcul de la trajectoire
    {

        playerState = States.Flying;
        this.GetComponent<PlayerMovement>().SetOnFly(true);
        SoundAssets.instance.PlayYeetSound(getIndexOfPrefab());
        float animation = 0f;
        anim.SetBool("onFly", true);
        // faut lancer ROLL pour que sa change la valeur de indexOfFace
        Roll();

        while (animation < duration)
        {
            animation += Time.deltaTime;
            //scale du joueur
            SpriteScale(animation / duration);
            //lancement du joueur selon une parabole
            transform.position = Parabola(start, finish, duration, animation / duration);
            //Le cercle agi comme une ombre au sol, elle se d?place tout droit
            _groudCircle.position = GroundCircleDuringParabola(start-new Vector2(0,0.4f), finish - new Vector2(0, 0.4f), duration, animation / duration);

            anim.SetInteger("indexOfFace", indexOfFace);
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
        _groudCircle.position = transform.position - new Vector3(0, 0.4f,0);
        this.GetComponent<PlayerMovement>().SetInput(0, 0);
        this.GetComponent<PlayerMovement>().SetOnFly(false);
        playerState = States.OnFoot;
        _uimanager.SwapSkill(currentPower, this);
        anim.SetBool("onFly", false);
        yield return null;
    }

    public void Fire(InputAction.CallbackContext context) //Attaque
    {
        if (GameManager.Instance.State == GameManager.GameState.InGame && playerState == States.OnFoot)
        {
            if (context.performed)
            {
                if (currentPower != null && currentPower.currentCharges > 0 && canFire)
                {
                    StartCoroutine(CooldownAttack());
                    currentPower.currentCharges--;
                    currentPower.ActivateOnce(this);
                    _uimanager.UpdateShotCount(currentPower, this);
                }
            }
        }
    }

    private IEnumerator CooldownAttack()
    {
        canFire = false;
        yield return new WaitForSeconds(1);
        canFire = true;
        yield return null;
    }

    private void Die()
    {
        GameManager.Instance.HandlePLayerDied();
        SoundAssets.instance.PlayPlayerDieSound(getIndexOfPrefab());
        deathBubble.SetActive(true);
        anim.SetBool("died", true);
        playerMovement.setSpeed(0);
        playerMovement.SetInput(0, 0);
        playerState = States.Dead;
        Debug.Log("{0} died");
    }

    public void Revive()
    {
        hp = maxHP;
        playerState = States.OnFoot;
        deathBubble.SetActive(false);
        playerMovement.setSpeed(playerMovement.maxSpeed);
        anim.SetBool("died", false);
        MakeFly(this);
        GameManager.Instance.HandlePlayerResurect();
    }

    private IEnumerator ReviveCoroutine()
    {
        Collider2D[] colliders = new Collider2D[10];
        var tempNumber = this.GetComponent<BoxCollider2D>().OverlapCollider(new ContactFilter2D(), colliders);
        Collider2D temp = colliders.FirstOrDefault(x => x != null && x.CompareTag("Player"));

        if (tempNumber > 0 && temp != null && temp.gameObject.GetComponent<Player>() is Player otherPlayer)
        {
            if (otherPlayer.playerState == States.Dead)
            {
                otherPlayer.Revive();
            }
        }
        yield return new WaitForSeconds(0.3f);
    }
    public void TakeDamage(int value)
    {
        if (isInvincible) return;

        if (hp > 0)
        {
            hp -= value;
            SoundAssets.instance.PlayTakeDamagePlayer(getIndexOfPrefab());
            Debug.LogFormat("{0} lost {1} Hp", gameObject.name, value);
            _uimanager.UpdatePlayerLife(this);
        }

        if (hp <= 0 && playerState != States.Dead)
        {
            hp = -1;
            Die();
        }
        _uimanager.UpdatePlayerLife(this);
        StartCoroutine(BecomeTemporarilyInvincible());

    }

    private IEnumerator BecomeTemporarilyInvincible()
    {
        isInvincible = true;

        for (float i = 0; i < invincibilityDurationSeconds; i += invincibilityDeltaTime)
        {
            // Alternate between 0 and 1 scale to simulate flashing
            if (spriteRenderer.enabled)
            {
                spriteRenderer.enabled = false;
            }
            else
            {
                spriteRenderer.enabled = true;
            }
            yield return new WaitForSeconds(invincibilityDeltaTime);
        }

        spriteRenderer.enabled = true;
        isInvincible = false;
    }
}



