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
    private Animator ani;

    public GameObject sakura;
    private GameObject spawnedSakura;

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {

            spawnedSakura = Instantiate(sakura, this.transform.position, Quaternion.identity);
            Destroy(sakura);
            spawnedSakura.transform.localScale = new Vector3(1,1,1);
            spawnedSakura.GetComponent<Rigidbody2D>().AddForce(transform.up*100000f);

            cirColl.enabled = false;
            boxColl.enabled = false;
            pos1.gameObject.SetActive(false);
            pos2.gameObject.SetActive(false);
            pos3.gameObject.SetActive(false);
            ani.SetBool("Scared", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player" && !move && questActive)
        {
            move = true;
        }
    }


    // Use this for initialization
    void Start () {
        ani = GetComponent<Animator>();
        pos3Start = pos3.position;
        cirColl = this.GetComponent<CircleCollider2D>();
        boxColl = this.GetComponent<BoxCollider2D>();

        boxColl.offset = normalOffset;
        boxColl.size = normalSize;
        questActive = true;

        if (!questActive)
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
            cirColl.enabled = false;
            if (atPos1)
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
        ani.SetBool("Scared", true);
        step = speed * Time.deltaTime; 
        transform.position = Vector3.MoveTowards(this.transform.position, to, step);

        Vector3 diff = to - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        if (Vector3.Distance(this.transform.position, to) <= 0.1)
        {
            ani.SetBool("Scared", false);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            Debug.Log("moved");
            move = false;
            cirColl.enabled = true;
            atPos1 = !atPos1;
        }
    }

    void CircleMove(Vector3 to)
    {
        //need to add code from duckpos to set circle to true on trigger with player
        //duck mybae not circle just go box movement or i can program a kinda circle.
        ani.SetBool("Swimming", true);
        step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(this.transform.position, pos3.position, step);
        pos3.position = Vector3.MoveTowards(pos3.position, to, step);

        Vector3 diff = pos3.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        

        if (Vector3.Distance(this.transform.position, to) <= 0.1)
        {       
            ani.SetBool("Swimming", false);
            transform.localRotation = Quaternion.Euler(0f, 0f, 90f);           
            move = false;
            circle = false;
            pos3.position = pos3Start;
            atPos1 = !atPos1;
            cirColl.enabled = true;
        }
    }
}
