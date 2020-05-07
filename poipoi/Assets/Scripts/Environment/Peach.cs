using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peach : MonoBehaviour {

    public DeathBall frog;
    public LevelManager lm;
    public GameObject sakura;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Frog" && frog.hungryFrog)
        {
            Instantiate(sakura, this.transform.position, this.transform.rotation);

            frog.hungryFrog = false;
            //frog.EnableCollider(false);
            Destroy(this.gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Frog" && frog.hungryFrog)
        {
            Instantiate(sakura, this.transform.position, this.transform.rotation);

            frog.hungryFrog = false;
            //frog.EnableCollider(false);
            Destroy(this.gameObject);
        }

    }


    // Use this for initialization
    void Start () {

	}

    // Update is called once per frame
    void Update()
    {


    }

    }
