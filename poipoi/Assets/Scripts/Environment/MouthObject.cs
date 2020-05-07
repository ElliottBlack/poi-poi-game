using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthObject : MonoBehaviour {


    private Rigidbody2D rb2d;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" && coll.gameObject.GetComponent<Fish>().mouthObject == null)
        {
            this.transform.SetParent(coll.transform);
            rb2d.isKinematic = true;
            this.GetComponent<CircleCollider2D>().isTrigger = true;
            rb2d.velocity = Vector2.zero;
            coll.gameObject.GetComponent<Fish>().mouthObject = this.gameObject;
        }
    }

    // Use this for initialization
    void Start () {
        rb2d = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
