using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turtle : MonoBehaviour {

    public Rigidbody2D rb2d;
    public float speed = 1f;
    public float rotSpeed = 0.2f;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement ()
    {
        transform.Rotate(Vector3.forward * rotSpeed);
        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = transform.right;

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
}
