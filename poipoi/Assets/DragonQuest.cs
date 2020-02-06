using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonQuest : MonoBehaviour {

    public LevelManager lm;
    private BoxCollider2D boxColl;
    private bool questComplete = false;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            lm.QuestComplete(6);
            questComplete = true;
            boxColl.enabled = false;
        }
    }

    // Use this for initialization
    void Start () {
        boxColl = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if(lm.questsActive[6] && !questComplete)
        {
            boxColl.enabled = true;
        }
	}
}
