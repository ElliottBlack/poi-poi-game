using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bells : MonoBehaviour {

    public float litSecs = 1f;
    public bool glow = false;
    private float secs = 0f;
    private SpriteRenderer spriteRen;

    public BellsManager bellm;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Bubble" && !glow)
        {
            glow = true;
            bellm.addBellGlowing();
        }
    }

    // Use this for initialization
    void Start () {
        spriteRen = this.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(glow)
        {
            spriteRen.color = new Color(1,0,1);
            secs += Time.deltaTime;
            if(secs >= litSecs)
            {
                bellm.bellStoppedGlowing();
                secs = 0f;
                glow = false;
                spriteRen.color = new Color(1, 1, 1);
            }
        }
	}
}
