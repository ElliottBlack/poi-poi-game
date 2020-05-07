using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour {

    public Rigidbody2D rb2d;
    public float speed = 1f;
    public float rotSpeed = 0.2f;
    public bool inShell = false;
    private bool found = false;
    public bool spawnPetal = false;
    public GameObject petal;
    public Transform head;
    public bool slide = false;
    private float secs = 0f;
    public float waitSecs = 3f;
    private bool waited = false;
    private bool move = true;

    public float petalx = 10f;
    public float petaly = 0f;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bubble" && inShell)
        {
            found = true;
        }
        if (coll.gameObject.tag == "Wall" && !inShell)
        {
            GetComponent<Animator>().SetBool("GoIn", true);
            rb2d.velocity = Vector3.zero;
            move = false;
        }

    }

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if(inShell)
        {
            if(found)
            {
                inShell = false;
                GetComponent<Animator>().SetBool("ComeOut", true);
                if(spawnPetal)
                {
                    GameObject p;
                    p = Instantiate(petal, new Vector3(head.position.x + petalx, head.position.y + petaly, 0), this.transform.rotation);
                    p.transform.SetParent(head);
                    p.GetComponent<CircleCollider2D>().isTrigger = true;
                }
            }
        }
        else if (move)
        {
            if(!waited)
            {
                secs += Time.deltaTime;
                if(secs >= waitSecs)
                {
                    waited = true;
                    
                }
            }
            else
            {
                Movement();
            }
        }
	}

    void Movement ()
    {
        //transform.Rotate(Vector3.forward * rotSpeed);
        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = transform.right;

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        if (slide)
        {
            rb2d.AddForce(movement * speed *300f);
            slide = false;
        }
        else
        {
            rb2d.AddForce(movement * speed);
        }

    }
}
