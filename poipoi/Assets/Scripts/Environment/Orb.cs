using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {

    /// <summary>
    /// sakura petal code. force applied at start
    /// ontrigger and on collision deals with different game modes.
    /// </summary>

    public Rigidbody2D rig;
    public float speed = 1f;

    public bool powerUp = false;

    public bool randomDir = false;
    public bool downDir = false;

    public Vector3 stayPos;
    public bool staying = false;
    public SpriteRenderer spriteRender;

    private bool noColor = true;
    public bool p1Color = false;
    public bool p2Color = false;

    public Manager man;

    private float secs = 0f;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        /// this is duplicated code both in trigger and collision. make method or take one out
        if (man.gameModeColours)
        {
            if (coll.gameObject.tag == "petalKiller")
            {
                Destroy(this.gameObject);
            }
            if (coll.gameObject.tag == "Player")
            {
                if (powerUp)
                {
                    Destroy(this.gameObject);
                }
                if (!p1Color)
                {
                    man.AddScore1(1);
                    if (!noColor)
                    {
                        man.AddScore2(-1);
                    }
                }
                man.score1Display();
                man.score2Display();
                noColor = false;
                p1Color = true;
                p2Color = false;
                spriteRender.color = new Color(0.54f, 1, 0.37f, 1);
            }
            else if (coll.gameObject.tag == "Player2")
            {
                if (powerUp)
                {
                    Destroy(this.gameObject);
                }
                if (!p2Color)
                {
                    man.AddScore2(1);
                    if (!noColor)
                    {
                        man.AddScore1(-1);
                    }
                }
                man.score1Display();
                man.score2Display();
                noColor = false;
                p1Color = false;
                p2Color = true;
                spriteRender.color = new Color(1, 0.74f, 0.24f, 1);
            }
        }
        else if (man.gameModeAllYouCanEat)
        {
            if (coll.gameObject.tag == "Player")
            {
                Destroy(this.gameObject);
                man.AddScore1(1);
                man.score1Display();
                man.score2Display();
            }
            else if (coll.gameObject.tag == "Player2")
            {
                Destroy(this.gameObject);
                man.AddScore2(1);
                man.score1Display();
                man.score2Display();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "petalKiller")
        {
            Destroy(this.gameObject);
        }
        if (man.gameModeColours)
        {
            if (coll.gameObject.tag == "Player")
            {
                if (powerUp)
                {
                    Destroy(this.gameObject);
                }
                if (!p1Color)
                {
                    man.AddScore1(1);
                    if (!noColor)
                    {
                        man.AddScore2(-1);
                    }
                }
                man.score1Display();
                man.score2Display();
                noColor = false;
                p1Color = true;
                p2Color = false;
                spriteRender.color = new Color(0.54f, 1, 0.37f, 1);
            }
            else if (coll.gameObject.tag == "Player2")
            {
                if (powerUp)
                {
                    Destroy(this.gameObject);
                }
                if (!p2Color)
                {
                    man.AddScore2(1);
                    if (!noColor)
                    {
                        man.AddScore1(-1);
                    }
                }
                man.score1Display();
                man.score2Display();
                noColor = false;
                p1Color = false;
                p2Color = true;
                spriteRender.color = new Color(1, 0.74f, 0.24f, 1);
            }
        }
        else if (man.gameModeAllYouCanEat)
        {
            if (coll.gameObject.tag == "Player")
            {
                Destroy(this.gameObject);                      
                man.AddScore1(1);
                man.score1Display();
                man.score2Display();
            }
            else if (coll.gameObject.tag == "Player2")
            {
                Destroy(this.gameObject);
                man.AddScore2(1);              
                man.score1Display();
                man.score2Display();
            }
        }
    }

    // Use this for initialization
    void Start () {
        man = GameObject.FindGameObjectWithTag("Manager").GetComponent<Manager>(); 
        spriteRender = this.GetComponent<SpriteRenderer>();
        rig = this.GetComponent<Rigidbody2D>();
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
        if (randomDir)
        {
            rig.AddForce(speed*Random.insideUnitCircle.normalized);
        }
        else
        {
            if (downDir)
            {
                rig.AddForce(new Vector3(0, -speed, 0));
            }
            else if (!downDir)
            {
                rig.AddForce(new Vector3(-speed, 0, 0));
            }

        }
        //gives petals a nicer looking flow
        //rig.AddForce(100 * Random.insideUnitCircle.normalized);

    }
	
	// Update is called once per frame
	void Update () {
       secs += Time.deltaTime;

        if (secs > 1)
        {
            //rig.AddForce(100*Vector3.right);
            //rig.AddForce(100* Random.insideUnitCircle.normalized);
            secs = 0f;
        }

       if (staying)
       {
           this.transform.localPosition = stayPos;
       }
       if (man.gameModeHorders)
       {
            if (this.transform.position.x > 10)
            {
                if (!p1Color)
                {
                    man.AddScore1(1);
                    man.score1Display();
                    p1Color = true;
                    spriteRender.color = new Color(0.54f, 1, 0.37f, 1);
                }
            }
            else if (this.transform.position.x < -10)
            {
                if (!p2Color)
                {
                    man.AddScore2(1);
                    man.score2Display();
                    p2Color = true;
                    spriteRender.color = new Color(1, 0.74f, 0.24f, 1);
                }
            }
            else
            {
                if (p1Color)
                {
                    man.AddScore1(-1);
                    p1Color = false;
                    man.score1Display();
                }
                else if (p2Color)
                {
                    man.AddScore2(-1);
                    p2Color = false;
                    man.score2Display();
                }
                spriteRender.color = new Color(1, 1, 1, 1);
            }
       }

    }

}
