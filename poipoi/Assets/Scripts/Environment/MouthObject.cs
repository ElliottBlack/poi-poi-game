using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthObject : MonoBehaviour {

    private SpriteRenderer spr;
    private Rigidbody2D rb2d;
    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "PlayerMouth" && coll.transform.parent.gameObject.GetComponent<Fish>().mouthObject == null)
        {
            this.transform.SetParent(coll.transform);
            rb2d.isKinematic = true;
            this.GetComponent<CircleCollider2D>().isTrigger = true;
            rb2d.velocity = Vector2.zero;
            coll.transform.parent.gameObject.GetComponent<Fish>().mouthObject = this.gameObject;
            spr.sortingLayerName = "petal";
        }

    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "PlayerMouth" && coll.gameObject.GetComponent<Fish>().mouthObject == null)
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
        spr = this.GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
