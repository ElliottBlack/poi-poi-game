using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bells : MonoBehaviour {

    //notes
    //need to make sound effect volume same as pause sound efffect. need to link.

    public float litSecs = 1f;
    public bool glow = false;
    private float secs = 0f;
    private SpriteRenderer spriteRen;
    private Animator ani;
    private AudioSource aud;
    public AudioClip flowerSound;
    public LevelManager lm;

    public BellsManager bellm;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bubble" && !glow)
        {
            glow = true;
            aud.PlayOneShot(flowerSound, lm.getSoundVolume());
            bellm.addBellGlowing();
        }
    }

    // Use this for initialization
    void Start () {
        aud = this.GetComponent<AudioSource>();
        spriteRen = this.GetComponent<SpriteRenderer>();
        ani = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(glow)
        {
            //
            //spriteRen.color = new Color(1,0,1);
            ani.SetBool("open", true);
            ani.SetBool("close", false);
            

            secs += Time.deltaTime;
            if(secs >= litSecs && !bellm.allBellsGlowing)
            {
                bellm.bellStoppedGlowing();
                secs = 0f;
                glow = false;
                //
                //spriteRen.color = new Color(1, 1, 1);
                ani.SetBool("open", false);
                ani.SetBool("close", true);
                aud.PlayOneShot(flowerSound, 1f);
            }
        }
	}
}
