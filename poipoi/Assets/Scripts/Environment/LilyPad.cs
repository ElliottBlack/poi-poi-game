using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LilyPad : MonoBehaviour {


    public ParticleSystem lilyRipple;
    private bool move = false;
    private float angle = 0;
    private float speed = (2 * Mathf.PI) / 5;//2*PI in degress is 360, so you get 5 seconds to complete a circle
    private float radius = 1;
    private float x = 0f;
    private float y = 0f;
    private float a;
    private float b;

    public bool lotusPad = false;
    private ParticleSystem rip;

    private AudioSource aud;
    public AudioClip[] lilySounds;
    private int soundIndex = 0;
    public LevelManager lm;


    // the instantiated particle effect needs to destroy itself.
    // also thesize  of the ripple needs to be dependent on the size of the lily.

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "FishMouth")
        {
            move = true;
            rip = Instantiate(lilyRipple,this.transform.position, lilyRipple.transform.rotation);
            if(lotusPad)
            {
                rip.startLifetime = (1.5f * this.transform.localScale.x * 2f) / 7f;
            }
            else
            {
                rip.startLifetime = (1.5f * this.transform.localScale.x) / 7f;
            }

            soundIndex = Random.Range(0, lilySounds.Length);
            aud.PlayOneShot(lilySounds[soundIndex], lm.getSoundVolume());
        }
    }

    // Use this for initialization
    void Start () {
        aud = GetComponent<AudioSource>();
        a = this.transform.position.x;
        b = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		if(move)
        {
            CircleMove();
        }
	}

    void CircleMove()
    {
        angle += speed * Time.deltaTime; //if you want to switch direction, use -= instead of +=
        x = Mathf.Cos(angle) * radius + a - radius;
        y = Mathf.Sin(angle) * radius + b;
        this.transform.position = new Vector3(x, y, 0f);
        if ((angle * 180)/ Mathf.PI >= 360f)
        {
            move = false;
            angle = 0f;
        }
    }
}
