using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinnerPlayer : MonoBehaviour {

    public MenuManager man;

    private SpriteRenderer spriteRender;
    public Rigidbody2D rb2d;

    public float speed;             //Floating point variable to store the player's movement speed.
    private float moveVertical;
    private float moveHorizontal;
    public float rotSpeed = 3f;
    private bool start = true;
    private float startTime = 0f;
    private Vector3 startLoc;
    private CircleCollider2D cirColl;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        cirColl = GetComponent<CircleCollider2D>();
        cirColl.enabled = false;
        man = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        spriteRender = this.GetComponent<SpriteRenderer>();
        rb2d = GetComponent<Rigidbody2D>();
        startLoc = this.transform.position;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 60f, 0f);

        if (man.winnerFish == 1)
        {
            spriteRender.sprite = man.fishSprites[man.p1CharacterNum];
        }
        else
        {
            spriteRender.sprite = man.fishSprites[man.p2CharacterNum];
        }

    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (start)
        {

            transform.position = Vector3.Lerp(this.transform.position, startLoc, 0.03f);
            startTime += Time.deltaTime;
            if (startTime >= 2f)
            {
                start = false;
                cirColl.enabled = true;
            }
        }
        else
        {
            Movement();
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    void Movement()
    {
        if (man.winnerFish == 1)
        {
            //Store the current vertical input in the float moveVertical.
            moveVertical = Input.GetAxis("Vertical");
            //Store the current horizontal input in the float moveHorizontal.
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = 1f;
        }
        else
        {
            //Store the current vertical input in the float moveVertical.
            moveVertical = Input.GetAxis("Vertical2");
            //Store the current horizontal input in the float moveHorizontal.
            moveHorizontal = Input.GetAxis("Horizontal2");
            moveVertical = 1f;
        }

        // dont let the player rotate while not moving
        // kinda weird for a fish to do this but annoying
        //if (moveVertical != 0f)
        //{
        transform.Rotate(Vector3.forward * -moveHorizontal * rotSpeed);
        //}

        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = transform.right * moveVertical;

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
}
