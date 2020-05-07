using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using WaterRippleForScreens;

public class Fish : MonoBehaviour {
    /// <summary>
    /// handles all things player relate.
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
    private int pondNumIndex = 0;
    public bool[] pondNum;
    public float[] pondSpeed;
    public float[] pondCameraSize;
    public Material[] pondDragonMaterials;
    public float[] pondRotSpeeds;
    public float[] pondTrailTimes;
    public float[] pondTrailWidths;
    public GameObject[] evolveSprites;
    private float camSize;
    public ParticleSystem[] echos;
    public float[] collRadii;
    public float[] collOffsetX;


    public AudioClip levelUpFX;
    public AudioClip gong;
    private bool fishEvolving = false;
    private bool evolveEffectStart = false;
    private bool fadeToEvolve = true;
    private bool fadeOutEvolve = false;
    public Image fade;
    public float fadeTime = 1f;
    private bool waiting = false;
    private bool evolveFinished = false;
    public ParticleSystem evolveEffectRing;
    public ParticleSystem evolveEffectWhite;
    public ParticleSystem evolveEffectGold;
    public ParticleSystem firework;
    private bool fadeBack = false;
    public GameObject petalCounter;
    private bool waitOnFish = false;
    private bool changeSprites = false;
    public GameObject transformationPos;

    public AudioSource music;

    public GameObject triggerBubble;
    public GameObject mouthObject;

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
            camSize = 50f;

            //need to pause player. and play trasnform scene.
            canMove = false;
            speed = 0;
            // trail.time = 10000000000000f;
            //FishEvolve();
            //fadeToEvolve = true;
            fishEvolving = true;
            audioSource.PlayOneShot(levelUpFX, 1F);
            evolveSprites[pondNumIndex].SetActive(true);
            petalCounter.SetActive(false);
            music.Pause();
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
                pondNumIndex = i;
                speed = pondSpeed[i];
                camSize = pondCameraSize[i];
                trail.material = pondDragonMaterials[i];
                rotSpeed = pondRotSpeeds[i];
                trail.time = pondTrailTimes[i];
                trail.startWidth = pondTrailWidths[i];
                if (i == pondNum.Length - 1)
                {
                    //dragon therefore turn off collider and change sorting layer
                    cirColl.enabled = false;
                    //because sorting layer change dragon is not hidden in waterfall.
                    //maybe just hard code something. like start dragon moving early and play cutscene
                    //turn offf controls nd so on.
                    trail.sortingLayerName = "top";
                }
                else
                {
                    cirColl.radius = collRadii[i];
                    cirColl.offset = new Vector2(collOffsetX[i],0f);
                }
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

        if (Input.GetKey(KeyCode.Space) && !splashed && canMove)
        {
            Splash();
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



        if(fishEvolving)
        {
            FishEvolve();         
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
        for (int i = 0; i < pondNum.Length; i++)
        {
            if (pondNum[i])
            {
                if (mouthObject != null)
                {
                    mouthObject.transform.SetParent(null);
                    mouthObject.GetComponent<Rigidbody2D>().isKinematic = false;
                    mouthObject.GetComponent<CircleCollider2D>().isTrigger = false;
                    mouthObject = null;
                }
                echos[i].Play();
                GameObject bub = Instantiate(triggerBubble, echos[i].transform.position, this.transform.rotation);
                bub.GetComponent<Bubble>().index = i;
                splashed = true;             
            }
        }


        audioSource.PlayOneShot(rippleFX, 0.7F);
    }

    void CameraZoom()
    {
        if (cam.orthographicSize < camSize)
        {
            cam.orthographicSize += Time.deltaTime * 10f;
        }
    }

    public void FishEvolve()
    {

        // whenver startcoroutin wait is ran change variable waiting to true, once wait is complete it will change to false;
        // then move to next section

        // start evolving
        if (fadeToEvolve)
        {
            // fade screen to white
            if (fade.color.a < 1.0f)
            {
                fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a + (Time.deltaTime / fadeTime));
            }
            //once full white screen move camera to evolve location and change varavles
            else if (fade.color.a >= 1.0f && !waiting)
            {
                //Debug.Log(cam.orthographicSize);
                cam.orthographicSize = 50f;
                cam.transform.position = new Vector3(5000f, 0f, -100f);
                cam.GetComponent<CameraFollow>().enabled = false;
                //cam.GetComponent<CameraFollow>().target = transformationPos;
                StartCoroutine(Wait(1));
                waiting = true;
                fadeToEvolve = false;
                fadeOutEvolve = true;
            }
        }
        // fade screen back
        else if (fadeOutEvolve)
        {
            if (!waiting && !waitOnFish)
            {
                fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a - (Time.deltaTime / fadeTime));
                // once screen visible again wait 5 secs then start evolve animations
                if (fade.color.a <= 0f)
                {
                    StartCoroutine(Wait(5));
                    waiting = true;
                    waitOnFish = true;
                }
            }
            else if(!waiting && waitOnFish)
            {
                fadeOutEvolve = false;
                evolveEffectStart = true;
                waiting = true;
                StartCoroutine(Wait(8));
                evolveEffectRing.Play();               
            }

        }
        else if (evolveEffectStart)
        {
            if (!waiting && !evolveFinished)
            {
               
                if (!changeSprites && !waiting)
                {
                    evolveEffectWhite.Play();
                    waiting = true;
                    StartCoroutine(Wait(2));
                    changeSprites = true;
                }
                else if (changeSprites && !waiting)
                {
                    evolveSprites[pondNumIndex].SetActive(false);
                    evolveSprites[pondNumIndex + 1].SetActive(true);
                    evolveFinished = true;
                    evolveEffectGold.Play();
                    audioSource.PlayOneShot(gong, 1F);
                    waiting = true;
                    StartCoroutine(Wait(5));
                }
            }
            else if (evolveFinished && !waiting)
            {

                if (fade.color.a < 1.0f && !fadeBack)
                {
                    fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a + (Time.deltaTime / (fadeTime * 3)));
                    if (fade.color.a >= 1.0f)
                    {
                        for (int i = 0; i < pondNum.Length - 1; i++)
                        {
                            if (pondNum[i])
                            {
                                if(i + 1 == pondNum.Length - 1)
                                {
                                    //dragon therefore turn off collider and change sorting layer
                                    cirColl.enabled = false;
                                    //because sorting layer change dragon is not hidden in waterfall.
                                    //maybe just hard code something. like start dragon moving early and play cutscene
                                    //turn offf controls nd so on.
                                    trail.sortingLayerName = "top";
                                }
                                else
                                {
                                    cirColl.radius = collRadii[i+1];
                                    cirColl.offset = new Vector2(collOffsetX[i+1], 0f);
                                }

                                trail.material = pondDragonMaterials[i + 1];
                                rotSpeed = pondRotSpeeds[i + 1];
                                trail.time = pondTrailTimes[i + 1];
                                trail.startWidth = pondTrailWidths[i + 1];

                                music.Play();
                                speed = pondSpeed[i + 1];
                                pondNum[i] = false;
                                pondNum[i + 1] = true;
                                cam.orthographicSize = pondCameraSize[i];
                                camSize = pondCameraSize[i + 1];
                                lm.PowerPetalCollected(false);
                                pondNumIndex += 1;
                                break;
                            }
                        }
                        petalCounter.SetActive(true);

                        evolveSprites[pondNumIndex].SetActive(false);
                        canMove = true;
                        cam.GetComponent<CameraFollow>().enabled = true;
                        //cam.GetComponent<CameraFollow>().target = this.gameObject;
                        cam.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -100f);
                        fadeBack = true;

                        //waiting = true;
                        //StartCoroutine(Wait(5));
                    }
                }
                else
                {
                    fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, fade.color.a - (Time.deltaTime / fadeTime * 3));
                    // once screen visible again wait 5 secs then start evolve animations
                    if (fade.color.a <= 0f)
                    {
                        evolveEffectStart = false;
                        fadeBack = false;
                    }
                }
            }
        }
        else
        {
            fishEvolving = false;
            fadeToEvolve = true;
            evolveFinished = false;
            waitOnFish = false;
            changeSprites = false;
        }
        
    }

    IEnumerator Wait(float secs)
    {
        yield return new WaitForSeconds(secs);
        waiting = false;
    }

}
