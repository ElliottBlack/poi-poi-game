using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBall : MonoBehaviour {

    /*Frog Script
     * Things to do : Change from old deathball name to Frog
     * ATM frog can swim anywhere. no collider
     * Add code to check for wall trigger and swim towards centre for abit to re center
     * use the trigger collider. trigger collider is being turned off after eating peach so change that
     * 
     * 
     */


    public float increaseSizeAmount = 0.1f;
    private Rigidbody2D rb2d;
    public float thrustTime = 1f;
    public float thrustSpeed = 30000f;
    public float rotSpeed = 50f;
    private float secs = 0f;

    public bool spawnTadpoles = false;

    private Quaternion from;
    private Quaternion to;

    public GameObject player;

    private Animator ani;

    public LevelManager lm;

    public bool hungryFrog = false;
    public bool frogQuestActive = false;

    public CircleCollider2D frogCollider;
    private CircleCollider2D frogTrigger;
    public GameObject peach;
    private bool eatPeach = false;
    

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && hungryFrog && !frogQuestActive)
        {
            lm.QuestActivate("Feed the Frog", 1);
            frogTrigger.radius = 25f;
            frogQuestActive = true;
        } 
        if (coll.gameObject.tag == "Peach" && hungryFrog && frogQuestActive)
        {
            frogCollider.enabled = true;
            ani.Play("FrogJump", 0, 0);
            rb2d.AddForce(transform.up * thrustSpeed * 2f);
            eatPeach = true;
            frogTrigger.enabled = false;
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {

    }

    // Use this for initialization
    void Start () {
        ani = GetComponent<Animator>();
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        frogTrigger = GetComponent<CircleCollider2D>();
        //transform.Rotate(0f, 0f, Random.Range(0f, 360f));
        //rb2d.AddForce(transform.up * thrustSpeed);

        from = this.transform.rotation;
        to = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
    }
	
	// Update is called once per frame
	void Update () {
        
        if(hungryFrog)
        {
            Vector3 diff = peach.transform.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            if (eatPeach)
            {
                secs += Time.deltaTime;
                if (secs >= thrustTime)
                {
                    ani.Play("FrogJump", 0, 0);
                    secs = 0;
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(transform.up * thrustSpeed);
                }
            }
        }
        else if(!hungryFrog)
        {
            this.transform.rotation = Quaternion.Slerp(from, to, secs / thrustTime);

            secs += Time.deltaTime;
            if (secs >= thrustTime)
            {
                ani.Play("FrogJump", 0, 0); 
                from = this.transform.rotation;
                to = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
                //rotSpeed = Random.Range(50f, 200f);
                secs = 0;
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(transform.up * thrustSpeed);

            }
        }    
	}

    public void EnableCollider(bool tf)
    {
        frogCollider.enabled = tf;
    }
}
