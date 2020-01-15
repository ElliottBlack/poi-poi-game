using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachTree : MonoBehaviour {

    public Vector3 peachSpeed;

    public GameObject peach;
    private CircleCollider2D peachCollider;
    private Rigidbody2D peachRigidbody;
    private SpriteRenderer peachSpriteRenderer;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            peachCollider.enabled = true;
            peachRigidbody.AddForce(peachSpeed);
            peachSpriteRenderer.sortingLayerName = "petal";
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {

    }

    // Use this for initialization
    void Start () {
        peachCollider = peach.GetComponent<CircleCollider2D>();
        peachRigidbody = peach.GetComponent<Rigidbody2D>();
        peachSpriteRenderer = peach.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
