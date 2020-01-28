using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadWoman : MonoBehaviour {

    public LevelManager lm;
    private CircleCollider2D cirColl;
    private bool questActive = false;
    private bool happy = false;
    private AudioSource crying;

    // Use this for initialization

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Flower" && questActive && !happy)
        {
            happy = true;
            crying.Stop();
            lm.QuestComplete(3);
            cirColl.enabled = false;
        }
        if (coll.gameObject.tag == "Player" && !questActive)
        {
            questActive = true;
            cirColl.radius = 5f;
            lm.QuestActivate("Cheer up the Woman", 3);
        }
    }

    void Start () {
        cirColl = this.GetComponent<CircleCollider2D>();
        crying = this.GetComponent<AudioSource>();
        if (lm.questsActive[3])
        {
            questActive = true;
            cirColl.radius = 5f;
        }
        if (lm.questsCompleted[3])
        {
            crying.Stop();
            happy = true;
            cirColl.enabled = false;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
