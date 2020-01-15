using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFish : MonoBehaviour {

    public Rigidbody2D rb2d;
    public float speed = 1f;
    private float secs = 0f;
    public float rotSpeed = 0.2f;
    private bool goUp = false;
    public Material[] sprites;
    public TrailRenderer trail;
    private float deathSecs = 0f;
    private float turnSecs = 0.3f;
    private float rotIncrease = 0.05f;
    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();

        rotSpeed = Random.Range(-0.15f, 0.15f);
        turnSecs = Random.Range(0.2f, 0.4f);
        rotIncrease = Random.Range(0.03f, 0.07f);

        if (rotSpeed <= 0)
        {
            goUp = false;
        }
        else
        {
            goUp = true;
        }
        trail.sharedMaterial = sprites[Random.Range(0, 6)];
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        deathSecs += Time.deltaTime;
        if(deathSecs >= 20f)
        {
            Destroy(this.gameObject);
        }
	}
    void Movement()
    {


        secs += Time.deltaTime;
        if (secs >= turnSecs)
        {
            if (goUp)
            {
                rotSpeed += rotIncrease;
            }
            else
            {
                rotSpeed -= rotIncrease;
            }


            if (rotSpeed <= -0.2)
            {
                goUp = true;
            }
            else if (rotSpeed >= 0.2)
            {
                goUp = false;
            }
            secs = 0;
        }



        transform.Rotate(Vector3.forward * rotSpeed);

        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = transform.right;

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
}
