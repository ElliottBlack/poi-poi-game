using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowFish : MonoBehaviour {

    public Rigidbody2D rb2d;
    private float speed = 1f;
    public float rotSpeed = 0.2f;
    private float turnTime = 5f;

    private Quaternion from;
    private Quaternion to;

    public bool follower = false;
    public FlowFish leader;

    public float delaySecs = 0.5f;
    private Vector3 followVec;
    private float followX = 0f;
    private float followY = 0f;
    private float followRange = 40f;

    private Vector3 OriginVec;
    private bool CenterMove = false;
    private float originSecs = 0f;

    public bool changeFollow = false;
    private float changeSecs = 0f;
    // can add time for both lead and follow to choose how long each fish follows the school vs does its own thing
    public float changeTime = 10f;

    private float secs = 0f;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Wall" && !follower)
        {
            OriginVec = Vector3.zero - transform.position;
            CenterMove = true;
        }
    }
    // Use this for initialization
    void Start()
    {
        speed = Random.Range(7f, 9f);
        if (!follower)
        {
            speed = 9.5f;
        }
        rb2d = GetComponent<Rigidbody2D>();

        from = this.transform.rotation;
        to = Quaternion.Euler(0f, 0f, Random.Range(-360f, 360f));

        followX = Random.Range(-followRange, followRange);
        followY = Random.Range(-followRange, followRange);

        changeTime = Random.Range(changeTime, changeTime + 60f);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        secs += Time.deltaTime;
        if(secs > turnTime)
        {

            if(follower)
            {
                followX = Random.Range(-followRange, followRange);
                followY = Random.Range(-followRange, followRange);
            }
            else 
            {
                from = this.transform.rotation;
                to = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                rotSpeed = Random.Range(-1f, 1f);
            }

            turnTime = Random.Range(5f,10f);
            secs = 0f;
        }
        if(changeFollow)
        {
            changeSecs += Time.deltaTime;
            if (changeSecs >= changeTime)
            {
                changeSecs = 0f;
                follower = !follower;
            }
        }


    }
    void Movement()
    {
        if (follower)
        {
            followVec = leader.transform.position;
            followVec = new Vector3(followVec.x + followX, followVec.y + followY, 0);


            Vector3 dir = followVec - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        else if (!CenterMove)
        {
            transform.Rotate(Vector3.forward * rotSpeed);
        }
        else
        {
            float angle = Mathf.Atan2(OriginVec.y, OriginVec.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            originSecs += Time.deltaTime;
            if(originSecs >= 30f)
            {
                originSecs = 0f;
                CenterMove = false;
            }
        }
        //this.transform.rotation = Quaternion.Slerp(from, to, rotSpeed);
        

        //Use the two store floats to create a new Vector2 variable movement.
        Vector3 movement = transform.right;

        //Call the AddForce function of our Rigidbody2D rb2d supplying movement multiplied by speed to move our player.
        rb2d.AddForce(movement * speed);
    }
}
