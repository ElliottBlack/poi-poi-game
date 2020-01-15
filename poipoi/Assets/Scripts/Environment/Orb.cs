using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : MonoBehaviour {

    /// <summary>
    /// sakura petal code. force applied at start
    /// ontrigger and on collision deals with different game modes.
    /// </summary>

    public Rigidbody2D rig;
    public bool randomSpeed = true;
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

    private float secs = 0f;

    private LevelManager lm;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "petalKiller")
        {
            Destroy(this.gameObject);
        }

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "petalKiller")
        {
            Destroy(this.gameObject);
        }

    }

    // Use this for initialization
    void Start () {
        lm = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();

        if (randomSpeed)
        {
            speed = Random.Range(speed - 100, speed + 100);
        }
        

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
      

    }

}
