using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using WaterRippleForScreens;

public class Fish : MonoBehaviour {
    /// <summary>
    /// handles all things player relate.
    /// both for player 1 and 2 despite the name.
    /// this is new player1 script, need to replace on prefabs
    /// </summary>

    public bool player1 = true;
    public bool player2 = false;


    public float speed;             //Floating point variable to store the player's movement speed.
    private float moveVertical;
    private float moveHorizontal;

    public Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public GameManager gm;

    public float rotSpeed = 2f;


    public SpriteRenderer spriteRender;
    public TrailRenderer trail;
    public GameObject trailGameObject;

    //startVaribles
    private bool startOfGame = true;
    private bool canMove = false;
    private bool newWallFish = false;
    private Quaternion startRot;
    private Vector3 startLoc;
    private float startTime = 0f;
    public float canMoveTime = 5f;
    public float canBeHitTime = 7f;
    public float canMoveTimeWall = 3f;
    public float canBeHitTimeWall = 4f;

    private CircleCollider2D cirColl;

    //wall varaibles
    private Vector3 wallFishPos;
    public float wallSpawnDepth;

    public AudioClip impact;
    public AudioClip rippleFX;
    AudioSource audioSource;

    public LevelManager lm;
    public GameObject deathExplosion;

    public RippleEffect ripple;
    public RippleGenerator rippleGen;
    public Camera cam;
    public ParticleSystem parSys;
    private bool splashed = false;
    private float splashCooldown = 0f;

    // keeps track of where the fish is and what stage of dragon development they are at.
    public bool[] pondNum;
    public float[] pondSpeed;
    public float[] pondCameraSize;
    public Material[] pondDragonMaterials;
    public float[] pondRotSpeeds;
    public float[] pondTrailTimes;
    public float[] pondTrailWidths;
    private float camSize;

    public GameObject triggerBubble;
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PowerUp")
        {
            audioSource.PlayOneShot(impact, 0.7F);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Waterfall")
        {
            
            coll.enabled = false;

            for(int i = 0; i < pondNum.Length-1; i++)
            {
                if (pondNum[i])
                {
                    trail.material = pondDragonMaterials[i + 1];
                    rotSpeed = pondRotSpeeds[i + 1];
                    trail.time = pondTrailTimes[i + 1];
                    trail.startWidth = pondTrailWidths[i + 1];


                    speed = pondSpeed[i+1];
                    pondNum[i] = false;
                    pondNum[i+1] = true;
                    camSize = pondCameraSize[i+1];
                    lm.PowerPetalCollected(false);
                    break;
                }
            }          
        }


        if (coll.gameObject.tag == "Petal")
        {
            audioSource.PlayOneShot(impact, 0.7F);
        }


    }

    // Use this for initialization
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRender = this.GetComponent<SpriteRenderer>();
        /*gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (gm != null)
        {
            if (player1)
            {
                trail.sharedMaterial = gm.getP1Material();
            }
        }
        */
        cirColl = GetComponent<CircleCollider2D>();

        //start of game move finish to target location from bottom
        //startOfGame = true;
        startRot = this.transform.rotation;
        startLoc = this.transform.position;
        if (startOfGame)
        {
            if (newWallFish)
            {
                canMoveTime = canMoveTimeWall;
                canBeHitTime = canBeHitTimeWall;
            }
        }

        if (!player1)
        {
            this.gameObject.tag = "Player2";
        }

        //set speed to whichever pond the player is in.
        for (int i = 0; i < pondNum.Length; i++)
        {
            if (pondNum[i])
            { 
                speed = pondSpeed[i];
                camSize = pondCameraSize[i];
                trail.material = pondDragonMaterials[i];
                rotSpeed = pondRotSpeeds[i];
                trail.time = pondTrailTimes[i];
                trail.startWidth = pondTrailWidths[i];

            }
        }


    }
    private void Update()
    {

    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        EnterStage();
        CameraZoom();
        Movement();

        if (Input.GetKey(KeyCode.Space) && !splashed)
        {
            Splash();
            Instantiate(triggerBubble, parSys.transform.position, this.transform.rotation);
            splashed = true;
        }

        if(splashed)
        {
            splashCooldown += Time.deltaTime;
            if(splashCooldown > 3f)
            {
                splashed = false;
                splashCooldown = 0f;
            }
        }

    } 

    public void SetSkin(Material m)
    {
        trail.sharedMaterial = m;
    }

    void EnterStage()
    {
        if (startOfGame)
        {
            //transform.position = Vector3.Lerp(this.transform.position, startLoc, 0.01f);
            startTime += Time.deltaTime;
            if (startTime >= canMoveTime)
            {
                canMove = true;
                startOfGame = false;
            }
        }
    }

    void Movement()
    {
        if (player1 && canMove)
        {
            //Store the current vertical input in the float moveVertical.
            moveVertical = Input.GetAxis("Vertical");
            //Store the current horizontal input in the float moveHorizontal.
            moveHorizontal = Input.GetAxis("Horizontal");
        }
        else if (player2 && canMove)
        {
            //Store the current vertical input in the float moveVertical.
            moveVertical = Input.GetAxis("Vertical2");
            //Store the current horizontal input in the float moveHorizontal.
            moveHorizontal = Input.GetAxis("Horizontal2");
        }
        else if (!canMove)
        {
            moveHorizontal = 0f;
        }

        moveVertical = 1f;// used for constant forward motion 

        transform.Rotate(Vector3.forward * -moveHorizontal * rotSpeed);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = transform.right * moveVertical;

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }

    void Splash()
    {
        //transform.Rotate(Vector3.forward * Random.Range(-2f, 2f) * rotSpeed);
        parSys.Play();
        audioSource.PlayOneShot(rippleFX, 0.7F);
        // ripple.SetNewRipplePosition(new Vector2(cam.WorldToScreenPoint(this.transform.position).x, cam.WorldToScreenPoint(this.transform.position).y));
        // ripple.SetNewRipplePosition(new Vector2(0f, 0f));
    }

    void CameraZoom()
    {
        if (cam.orthographicSize < camSize)
        {
            cam.orthographicSize += Time.deltaTime * 10f;
        }
    }

}
