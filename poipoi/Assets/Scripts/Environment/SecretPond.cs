using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretPond : MonoBehaviour {

    private CircleCollider2D pondCollider;
    public LevelManager lm;
    private bool viewedPond = false;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            if (!viewedPond)
            {
                lm.QuestComplete(4);
                viewedPond = true;
                pondCollider.enabled = false;
            }
        }
    }

    // Use this for initialization
    void Start () {
        if (lm.questsCompleted[4])
        {
            viewedPond = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
