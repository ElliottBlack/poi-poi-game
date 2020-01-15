using UnityEngine;
using System.Collections;
using TMPro;

public class Player1 : MonoBehaviour
{
    /// <summary>
    /// handles all things player relate.
    /// both for player 1 and 2 despite the name.
    /// 
    /// </summary>

    public bool player1 = true;
    public bool player2 = false;


    public float speed;             //Floating point variable to store the player's movement speed.
    private float moveVertical;
    private float moveHorizontal;

    public Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    public Manager man;

    public float rotSpeed = 2f;
    //power up vars
    private int powerUpNum = 0;
    private bool poweringUp = false;
    private bool poweredUp = false;
    private bool poweringDown = false;
    private float powerTime = 0f;
    public float rotSpeedIncrease = 0.2f;
    public float speedIncrease = 100f;
    public float sizeIncrease = 1.2f;
    public GameObject tadpole;

    // bounce variables
    private ContactPoint2D hit;
    private bool bounce = false;
    private float bounceTimer = 0f;
    private float bounceSecs = 0.5f;
    private float bounceSpeed = 1000f;
    private float bounceSpeedOG = 1000f;

    public GameObject player1Zone;

    public Vector3 bigSize;

    public bool copy = false;
    public GameObject player1Clone;
    public GameObject player2Clone;
    private GameObject playerCopy;

    public SpriteRenderer spriteRender;
    public TrailRenderer trail;

    //starVaribles
    private bool startOfGame = true;
    private Quaternion startRot;
    private Vector3 startLoc;
    private float startTime = 0f;
    private CircleCollider2D cirColl;
    //public GameObject petalSpawner;
    //public GameObject powerSpawner;
    //private bool spawnersActive = false;
    //public TextMeshProUGUI countdown;

    private Quaternion bounceRot;
    private Vector2 n; //normal vector
    private Vector2 v; //velcoty vector
    private Vector2 r; //reflected vector

    // bubble variables
    public float bubbleSpeed = 10f;
    public Rigidbody2D bubble;
    public GameObject bubbleSpawner;
    private Vector2 bubblePushDir;
    public float bubbleForce = 10f;
    private float bubblePushTime = 0f;
    private bool bubblePushing = false;


    void OnCollisionEnter2D(Collision2D coll)
    {
        // this needs to be made into a method also add else if instead of if
        /*
        if(player1)
        {
            if (coll.gameObject.tag == "Player2")
            {
                //rb2d.velocity = Vector3.zero;
                hit = coll.contacts[0]; // the first contact is enough
                //Debug.DrawRay(hit.point, hit.normal * 10, Color.green, 3f); // draw green normal

                n = hit.normal;
                v = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
                // Debug.Log(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)+","+ Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));

                r = v - 2 * (Vector2.Dot(v, n)) * n;

                //Debug.Log(Mathf.Asin(rb2d.velocity.normalized.y)* Mathf.Rad2Deg);

                if (r.x < 0)
                {
                    //Debug.Log(180 - Mathf.Asin(hit.normal.y) * Mathf.Rad2Deg);
                    transform.rotation = Quaternion.Euler(0, 0, 180 - Mathf.Asin(r.y) * Mathf.Rad2Deg);
                }
                else
                {
                    //Debug.Log(Mathf.Asin(hit.normal.y) * Mathf.Rad2Deg);
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Asin(r.y) * Mathf.Rad2Deg);
                }
            }
        }

        if (player2)
        {
            if (coll.gameObject.tag == "Player")
            {
                //rb2d.velocity = Vector3.zero;
                hit = coll.contacts[0]; // the first contact is enough
                //Debug.DrawRay(hit.point, hit.normal * 10, Color.green, 3f); // draw green normal

                n = hit.normal;
                v = new Vector2(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));
                // Debug.Log(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad)+","+ Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad));

                r = v - 2 * (Vector2.Dot(v, n)) * n;

                //Debug.Log(Mathf.Asin(rb2d.velocity.normalized.y)* Mathf.Rad2Deg);

                if (r.x < 0)
                {
                    //Debug.Log(180 - Mathf.Asin(hit.normal.y) * Mathf.Rad2Deg);
                    transform.rotation = Quaternion.Euler(0, 0, 180 - Mathf.Asin(r.y) * Mathf.Rad2Deg);
                }
                else
                {
                    //Debug.Log(Mathf.Asin(hit.normal.y) * Mathf.Rad2Deg);
                    transform.rotation = Quaternion.Euler(0, 0, Mathf.Asin(r.y) * Mathf.Rad2Deg);
                }            
            }
        }
        if (coll.gameObject.tag == "Player2")
        {
            //rb2d.velocity = Vector3.zero;
            //hit = coll.contacts[0]; // the first contact is enough
           // bounce = true;
            //Debug.DrawRay(hit.point, hit.normal * 10, Color.green, 3f); // draw green normal
        }
        if (coll.gameObject.tag == "PowerUp")
        {
            if (poweringUp || poweringDown || poweredUp)
            {
                //cloned friends change the refence of the clone prefab to itself rather then the prefab
                // therefore when they are poweredUp eg speed and hit another power up the new clone will
                // have the powerup already implemented leading to errors.
                if (!copy)
                {
                    if (player1)
                    {
                        Instantiate(player1Clone, this.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                    }
                    else
                    {
                        Instantiate(player2Clone, this.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                    }
                }
            }
            else
            {
                powerUpNum = Random.Range(1, 4);
                poweringUp = true;
            }

        }


        if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "DeathBall")
        {
            Instantiate(this.gameObject, startLoc, startRot);
            Destroy(this.gameObject);
        }
        */

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "DeathBall")
        {
            Instantiate(this.gameObject, startLoc, startRot);
            Destroy(this.gameObject);
        }
        if (coll.gameObject.tag == "Wall")
        {
            //GameObject newFish = Instantiate(this.gameObject, Vector3.zero, startRot);
            //newFish.GetComponent<Player1>().startOfGame = false;

            //Destroy(this.gameObject);
        }

        // cuplicated code from collision take one out or create method
        if (coll.gameObject.tag == "Player2")
        {
            //rb2d.velocity = Vector3.zero;
            //hit = coll.contacts[0]; // the first contact is enough
            // bounce = true;
            //Debug.DrawRay(hit.point, hit.normal * 10, Color.green, 3f); // draw green normal
        }
        if (coll.gameObject.tag == "PowerUp")
        {
            if (poweringUp || poweringDown || poweredUp)
            {
                //cloned friends change the refence of the clone prefab to itself rather then the prefab
                // therefore when they are poweredUp eg speed and hit another power up the new clone will
                // have the powerup already implemented leading to errors.
                if (!copy)
                {
                    if (player1)
                    {
                        Instantiate(player1Clone, this.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                    }
                    else
                    {
                        Instantiate(player2Clone, this.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                    }
                }
            }
            else
            {
                powerUpNum = Random.Range(1, 4);
                poweringUp = true;
            }

        }
    }

    // Use this for initialization
    void Start()
    {

        cirColl = GetComponent<CircleCollider2D>();
        //start of game move finish to target location from bottom
        startOfGame = true;
        startRot = this.transform.rotation;
        startLoc = this.transform.position;
        if (!copy)
        {           
            cirColl.enabled = false;
        }

        if (!player1)
        {
            this.gameObject.tag = "Player2";
        }
        //Get and store a reference to the Rigidbody2D component so that we can access it.
        rb2d = GetComponent<Rigidbody2D>();
        man = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>();
        spriteRender = this.GetComponent<SpriteRenderer>();
        
        //add character selected
        if (player1)
        {
            //normal sprite code
            //spriteRender.sprite = man.fishSprites[man.p1CharacterNum];

            //trail.materials[0] = man.fishMaterials[man.p1CharacterNum];
            trail.sharedMaterial = man.fishMaterials[man.p1CharacterNum];
        }
        else
        {
            //normal sprite code
            //spriteRender.sprite = man.fishSprites[man.p2CharacterNum];

            //trail.materials[0] = man.fishMaterials[man.p2CharacterNum];
            trail.sharedMaterial = man.fishMaterials[man.p2CharacterNum];
        }

        if (man.gameModeBigFish)
        {
            transform.localScale = bigSize;
        }
    }
    private void Update()
    {
        PowerUp();
        //PermanentPowerUp();
        
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if(startOfGame && !copy)
        {
            //transform.position = Vector3.Lerp(this.transform.position, startLoc, 0.01f);
            startTime += Time.deltaTime;
            if (startTime >= 5f)
            {
                startOfGame = false;
                cirColl.enabled = true;
                if (man.gameModeSchool && !copy)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        playerCopy = Instantiate(this.gameObject, this.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                        playerCopy.GetComponent<Player1>().copy = true;
                    }
                }
            }
        }

        Movement();

        //add both keys for both players
        if (player1 && Input.GetButtonDown("Vertical"))
        {
           // FireBubble();
        }
        else if (player2 && Input.GetButtonDown("Vertical2"))
        {
          //  FireBubble();
        }
    }

    void HitPlayer()
    {
        Debug.Log("hit");
        
        rb2d.AddForce(hit.normal * bounceSpeed);
        bounceSpeed -= 50f;
        bounceTimer += Time.deltaTime;
        //if (bounceTimer > bounceSecs)
        if (bounceSpeed <= 0f)
        {
            bounceTimer = 0;
            bounceSpeed = bounceSpeedOG;
            bounce = false;
        }

    }

    public void PermanentPowerUp()
    {
        //speed
        if (powerUpNum == 1 && poweringUp)
        {
            speed += speedIncrease;
            poweringUp = false;
        }
        //rot
        else if (powerUpNum == 2 && poweringUp)
        {
            rotSpeed += rotSpeedIncrease;
            poweringUp = false;
        }
        //size
        else if (powerUpNum == 3 && poweringUp)
        {
            transform.localScale += new Vector3(sizeIncrease, sizeIncrease, 0);
            poweringUp = false;
        }
        //tadpole
        else if (powerUpNum == 4 && poweringUp)
        {
            Instantiate(tadpole, player1Zone.transform.position, Quaternion.identity);
            poweringUp = false;
        }
    }

    public void PowerUp()
    {
        //add power up
        if (poweringUp)
        {
            //speed
            if (powerUpNum == 1)
            {
                speed += speedIncrease;
                if (man.gameModeExtremePowerUps)
                {
                    speed += speedIncrease;
                }
                poweringUp = false;
            }
            //size
            else if (powerUpNum == 2)
            {
                transform.localScale += new Vector3(sizeIncrease, sizeIncrease, 0);
                if (man.gameModeExtremePowerUps)
                {
                    transform.localScale += new Vector3(sizeIncrease, sizeIncrease, 0);
                }
                poweringUp = false;
            }
            //friend
            else if (powerUpNum == 3)
            { 
                if (player1)
                {
                    Instantiate(player1Clone, this.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                    if (man.gameModeExtremePowerUps)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            Instantiate(player1Clone, this.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                        }
                    }
                }
                else
                {
                    Instantiate(player2Clone, this.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
                    if (man.gameModeExtremePowerUps)
                    {
                        for (int i = 0; i < 20; i++)
                        {
                            Instantiate(player2Clone, this.transform.position + new Vector3(0, 5, 0), Quaternion.Euler(0f,0f,Random.Range(0f,360f)));
                        }
                    }
                }


                poweringUp = false;
            }
            poweredUp = true;
        }
        //mantain power up for 5 secs
        if (poweredUp)
        {
            powerTime += Time.deltaTime;
            if (powerTime >= 7)
            {
                powerTime = 0;
                poweredUp = false;
                poweringDown = true;
            }
        }
        //remove power up
        if (poweringDown)
        {
            //speed
            if (powerUpNum == 1)
            {
                speed -= speedIncrease;
                if (man.gameModeExtremePowerUps)
                {
                    speed -= speedIncrease;
                }
                poweringUp = false;
            }
            //size
            else if (powerUpNum == 2)
            {
                transform.localScale -= new Vector3(sizeIncrease, sizeIncrease, 0);
                if (man.gameModeExtremePowerUps)
                {
                    transform.localScale -= new Vector3(sizeIncrease, sizeIncrease, 0);
                }
                poweringUp = false;
            }
            //friend
            else if (powerUpNum == 3)
            {
                //Destroy(playerCopy);
                poweringUp = false;
            }
            poweringDown = false;
        }
    }

    void Movement()
    {
        if (player1 && !startOfGame)
        {
            //Store the current vertical input in the float moveVertical.
            moveVertical = Input.GetAxis("Vertical");
            //Store the current horizontal input in the float moveHorizontal.
            moveHorizontal = Input.GetAxis("Horizontal");           
        }
        else if (player2 && !startOfGame)
        {
            //Store the current vertical input in the float moveVertical.
            moveVertical = Input.GetAxis("Vertical2");
            //Store the current horizontal input in the float moveHorizontal.
            moveHorizontal = Input.GetAxis("Horizontal2");
        }
        else if (startOfGame)
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

    void FireBubble()
    {
        //bubbleSpawnPosition = this.transform.position + this.transform.forward * 30f;
        Rigidbody2D bubbleClone = (Rigidbody2D) Instantiate(bubble, bubbleSpawner.transform.position, transform.rotation);
        //bubbleClone.velocity = transform.forward * bubbleSpeed;
        bubbleClone.AddForce(bubbleSpeed * this.transform.right);
    }

}