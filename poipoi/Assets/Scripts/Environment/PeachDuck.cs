using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeachDuck : MonoBehaviour {

    private Animator ani;
    public Transform beackBone;
    public GameObject peach;
    public GameObject mouthPeach;
    private Vector3 peachPos;
    private Vector3 startPos;
    public Vector3 hidePos;
    private bool hasPeach = true;
    private float secs = 0f;

    private bool turnMove = false;
    private bool getPeach = false;
    private Quaternion from;
    private Quaternion to;
    public float rotSpeed = 1f;
    public float speed = 1f;
    public float moveAmount = 10f;
    private float t = 0f;
    private float step = 0f;

    private bool turning = true;
    private bool reverse = false;

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Bubble" && hasPeach)
        {
            hasPeach = false;

            //player flap animation
            //
            //drop peach
            mouthPeach.GetComponent<SpriteRenderer>().sortingLayerName = "RockShadow";
            mouthPeach.transform.parent = null;
            mouthPeach.GetComponent<CircleCollider2D>().enabled = true;
            mouthPeach.GetComponent<Rigidbody2D>().AddForce(transform.up * 1000f);
            ani.SetTrigger("Flap");
            // turn around
            //run
        }
    }

    // Use this for initialization
    void Start () {
        peachPos = mouthPeach.transform.localPosition;

        ani = GetComponent<Animator>();

        startPos = this.transform.position;
        hidePos = startPos + (transform.up * -moveAmount);

        from = this.transform.rotation;
        to = from*Quaternion.Euler(0, 0, 180f);

    }
	
	// Update is called once per frame
	void Update () {
		if(!hasPeach && !turnMove)
        {
            secs += Time.deltaTime;
            if(secs > 2f)
            {
                turnMove = true;
                ani.SetBool("Swimming", true);
            }
        }
        
        if(turnMove)
        {
            TurnMove();
        }
	}

    void TurnMove()
    {

        if(turning)
        {
            t += Time.deltaTime * rotSpeed;
            if(!reverse)
            {
                transform.rotation = Quaternion.Lerp(from, to, t);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(to, from, t);
            }
            
            if (t >= 1f)
            {
                turning = false;
                t = 0f;
            }
        }
        else
        {          
            step += speed * Time.deltaTime;
            if(!reverse)
            {
                transform.position = Vector3.Lerp(startPos, hidePos, step);
            }
            else
            {
                transform.position = Vector3.Lerp(hidePos, startPos, step);
            }

            if(step >= 1f)
            {
                step = 0f;
                turning = true;

                if(!reverse)
                {
                    mouthPeach = Instantiate(peach, beackBone.transform.position, Quaternion.identity);                   
                    mouthPeach.GetComponent<CircleCollider2D>().enabled = false;
                    mouthPeach.transform.SetParent(beackBone);
                    mouthPeach.transform.localPosition = peachPos;
                    mouthPeach.GetComponent<SpriteRenderer>().sortingLayerName = "Frog";
                }
                else
                {
                    hasPeach = true;
                    secs = 0f;
                    turnMove = false;
                    ani.SetBool("Swimming", false);
                }

                reverse = !reverse;
            }
        }
    }

}
