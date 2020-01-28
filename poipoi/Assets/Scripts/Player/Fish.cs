using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    AudioSource audioSource;

    public LevelManager lm;
    public GameObject deathExplosion;

    void OnCollisionEnter2D(Collision2D coll)
    {       

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        // kinda working looks weird. tail shrinks and players ooks like the spawn away from wall not coming out of it.
        /*
            wallFishPos = new Vector3(-this.transform.position.x, this.transform.position.y, 0f);
            trail.transform.parent = null;
            this.transform.position = wallFishPos;
            GameObject trailClone = Instantiate(trailGameObject, this.transform.position, this.transform.rotation);
            trailClone.transform.parent = this.transform;
            trailClone.transform.localPosition = new Vector3(7f, 0f, 0f);
            trail = trailClone.GetComponent<TrailRenderer>();
        */
        if (coll.gameObject.tag == "SideWall")
        {
            trail.Clear();
            wallFishPos = new Vector3(-this.transform.position.x, this.transform.position.y, 0f);
            this.transform.position = wallFishPos;
            trail.Clear();

            //trail.transform.parent = null;
            //this.transform.position = wallFishPos;
            // GameObject trailClone = Instantiate(trailGameObject, this.transform.position, this.transform.rotation);
            // trailClone.transform.parent = this.transform;
            //trailClone.transform.localPosition = new Vector3(7f, 0f, 0f);
            //trail = trailClone.GetComponent<TrailRenderer>();

            //GameObject newFish = Instantiate(this.gameObject, wallFishPos, this.transform.rotation);
            // newFish.GetComponent<Fish>().startOfGame = false;
        }
        if (coll.gameObject.tag == "TopWall" )
        {
            /* looks weird try different approach
            wallFishPos = new Vector3(this.transform.position.x, -this.transform.position.y, 0f);
            trail.transform.parent = null;
            this.transform.position = wallFishPos;
            GameObject trailClone = Instantiate(trailGameObject, this.transform.position, this.transform.rotation);
            trailClone.transform.parent = this.transform;
            trailClone.transform.localPosition = new Vector3(7f,0f,0f);
            trail = trailClone.GetComponent<TrailRenderer>();
            
            ////////

            canMove = false;
            if (this.transform.position.y > -this.transform.position.y)
            {
                wallFishPos = new Vector3(this.transform.position.x, -this.transform.position.y - wallSpawnDepth, 0f);
            }
            else
            {
                wallFishPos = new Vector3(this.transform.position.x, -this.transform.position.y + wallSpawnDepth, 0f);
            }

            
            GameObject newFish = Instantiate(this.gameObject, wallFishPos, this.transform.rotation);
            newFish.GetComponent<Fish>().newWallFish = true;
            */
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
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (gm != null)
        {
            if (player1)
            {
                trail.sharedMaterial = gm.getP1Material();
            }
        }
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
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        
        //man = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();


        //add character selected
        if (player1)
        {
            //normal sprite code
            //spriteRender.sprite = man.fishSprites[man.p1CharacterNum];

            //trail.materials[0] = man.fishMaterials[man.p1CharacterNum];
            //trail.sharedMaterial = man.fishMaterials[man.p1CharacterNum];  this si code for different sprites
        }
        else
        {
            //normal sprite code
            //spriteRender.sprite = man.fishSprites[man.p2CharacterNum];

            //trail.materials[0] = man.fishMaterials[man.p2CharacterNum];
           // trail.sharedMaterial = man.fishMaterials[man.p2CharacterNum];  this is code for different sprites
        }

    }
    private void Update()
    {

    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {

        EnterStage();

        Movement();

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
}
