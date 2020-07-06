using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJump : MonoBehaviour {


    private float secs = 0f;
    private Animator ani;
    public float thrustSpeed = 30000f;
    private Rigidbody2D rb2d;

    private bool turn = false;
    private float turnSecs = 0f;

    // Use this for initialization
    void Start () {
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        secs += Time.deltaTime;
        if(secs >= 10f && !turn)
        {
            turn = true;
        }

        if(turn)
        {
            turnSecs += Time.deltaTime;
            Turning();
            if(turnSecs > 3f)
            {
                secs = 0f;
                turnSecs = 0f;
                turn = false;
                ani.SetTrigger("Jump");
                Jump();
            }
        }
	}

    void Turning()
    {
        transform.Rotate(Vector3.forward * 1f);
    }

    void Jump()
    {
        rb2d.velocity = Vector2.zero;
        rb2d.AddForce(transform.up * thrustSpeed);
    }
}
