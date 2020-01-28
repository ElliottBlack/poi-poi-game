using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck : MonoBehaviour {

    public Transform pos1;
    public Transform pos2;
    public Transform pos3;
    private Vector3 pos3Start;
    public bool atPos1 = true;
    public bool move = false;
    public float speed = 1f;
    private float step = 0f;
    public bool circle = false;
    public LevelManager lm;
    private CircleCollider2D cirColl;
    private BoxCollider2D boxColl;
    private bool questActive = false;
    public Vector2 normalOffset;
    public Vector2 normalSize;


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            lm.QuestComplete(2);
            cirColl.enabled = false;
            boxColl.enabled = false;
            pos1.gameObject.SetActive(false);
            pos2.gameObject.SetActive(false);
            pos3.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player" && !move && questActive)
        {
            move =true;
        }
        if (coll.gameObject.tag == "Player" && !questActive)
        {
            questActive = true;
            boxColl.offset = normalOffset;
            boxColl.size = normalSize;
            lm.QuestActivate("Catch the Duck", 2);
        }
    }


    // Use this for initialization
    void Start () {
        pos3Start = pos3.position;
        cirColl = this.GetComponent<CircleCollider2D>();
        boxColl = this.GetComponent<BoxCollider2D>();
        if (lm.questsActive[2])
        {
            questActive = true;
            boxColl.offset = normalOffset;
            boxColl.size = normalSize;
        }
        if (lm.questsCompleted[2])
        {
            cirColl.enabled = false;
            boxColl.enabled = false;
            pos1.gameObject.SetActive(false);
            pos2.gameObject.SetActive(false);
            pos3.gameObject.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
        if(move)
        {
            if(atPos1)
            {
                if(circle)
                {
                    CircleMove(pos2.position);
                }
                else
                {
                    Movement(pos2.position);
                }
            }
            else
            {
                if (circle)
                {
                    CircleMove(pos1.position);
                }
                else
                {
                    Movement(pos1.position);
                }
            }
        }

	}

    void Movement(Vector3 to)
    {
        step = speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(this.transform.position, to, step);
        if (this.transform.position == to)
        {
            move = false;
            atPos1 = !atPos1;
        }
    }

    void CircleMove(Vector3 to)
    {
        //need to add code from duckpos to set circle to true on trigger with player
        //duck mybae not circle just go box movement or i can program a kinda circle.
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(this.transform.position, pos3.position, step);
        pos3.position = Vector3.MoveTowards(pos3.position, to, step);
        if (this.transform.position == to)
        {
            move = false;
            circle = false;
            pos3.position = pos3Start;
            atPos1 = !atPos1;
        }
    }
}
