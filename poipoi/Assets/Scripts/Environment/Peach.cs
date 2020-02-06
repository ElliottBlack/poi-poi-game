using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Peach : MonoBehaviour {

    public DeathBall frog;
    public LevelManager lm;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Frog" && frog.hungryFrog && frog.frogQuestActive)
        {
            lm.QuestComplete(1);
            frog.hungryFrog = false;
            frog.EnableCollider(false);
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
