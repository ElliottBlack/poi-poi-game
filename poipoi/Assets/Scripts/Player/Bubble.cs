using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player2" || coll.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        if (coll.gameObject.tag == "Wall" || coll.gameObject.tag == "DeathBall")
        {
            Destroy(this.gameObject);

        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Wall")
        {
            Destroy(this.gameObject);
           
        }          
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
