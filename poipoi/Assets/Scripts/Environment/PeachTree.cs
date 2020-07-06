using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachTree : MonoBehaviour {

    public Vector3 peachSpeed;

    public GameObject peach;
    private CircleCollider2D peachCollider;
    private Rigidbody2D peachRigidbody;
    private SpriteRenderer peachSpriteRenderer;
    private Animator ani;
    public AudioClip crow;
    public AudioClip water;
    public LevelManager lm;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            peachCollider.enabled = true;
            ani.Play("PeachShake", 0, 0);
            peachRigidbody.AddForce(peachSpeed);
            peachSpriteRenderer.sortingLayerName = "petal";
            this.GetComponent<AudioSource>().PlayOneShot(crow, lm.getSoundVolume());
            this.GetComponent<AudioSource>().PlayOneShot(water, lm.getSoundVolume());
        }
    }
    void OnCollisionEnter2D(Collision2D coll)
    {

    }

    // Use this for initialization
    void Start () {
        ani = GetComponent<Animator>();
        peachCollider = peach.GetComponent<CircleCollider2D>();
        peachRigidbody = peach.GetComponent<Rigidbody2D>();
        peachSpriteRenderer = peach.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
