using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyTurtle : MonoBehaviour {

    public LevelManager lm;
    private CircleCollider2D cirColl;
    private bool questActive = false;
    private bool mamaFound = false;
    public float speed = 1f;
    private float step = 0f;
    public Transform fish;
    public Transform mama;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player" && !questActive)
        {
            questActive = true;
            lm.QuestActivate("Help Baby Turtle", 5);
            Debug.Log(questActive);
        }

        if (coll.gameObject.tag == "MamaTurtle" && questActive)
        {
            Debug.Log("nope");
            lm.QuestComplete(5);
            mamaFound = true;
            cirColl.enabled = false;
        }
    }

    // Use this for initialization
    void Start () {
        cirColl = this.GetComponent<CircleCollider2D>();
        if (lm.questsActive[5])
        {
            questActive = true;
        }
        if (lm.questsCompleted[5])
        {
            cirColl.enabled = false;
            mamaFound = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(questActive && !mamaFound)
        {
            FollowPlayer();
        }

        if(mamaFound)
        {
            HugMum();
        }
	}
    void FollowPlayer()
    {
        Vector3 diff = fish.transform.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(this.transform.position, fish.position, step);
    }
    void HugMum()
    {
        Vector3 diff = mama.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z);

        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(this.transform.position, mama.position, step);
    }
}
