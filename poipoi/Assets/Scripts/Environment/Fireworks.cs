using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour {

    /// <summary>
    /// Added to firework petal on the winners screen
    /// when destroyed by player instantiates firework effect
    /// </summary>
    /// 
   // public Rigidbody2D rig;
    public float speed = 1f;
    public GameObject dieEffectsPrefab;
    public LevelManager lm;
    public AudioClip rippleFX;
    AudioSource audioSource;

    public bool move = false;
    public Vector3 newPositionAdd;
    private float secs = 0f;
    public float moveSecs = 2f;
    private CircleCollider2D cirColl;
    public bool randRot = true;

    public GameObject player;
    public bool magnet = false;
    private float magnetRange = 12f;
    private float magnetPower = 7f;
    void OnDestroy()
    {
        Instantiate(dieEffectsPrefab, this.transform.position, this.transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            lm.PowerPetalCollected(true);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    { 
        if (coll.gameObject.tag == "Player")
        {
            lm.PowerPetalCollected(true);
            Destroy(this.gameObject);
        }

        if (coll.gameObject.tag == "Bubble")
        {
            audioSource.PlayOneShot(rippleFX, 0.7F);
        }
    }

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        cirColl = this.GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        if(randRot)
        {
            transform.Rotate(0f, 0f, Random.Range(0f, 360f));
        }
        //rig = this.GetComponent<Rigidbody2D>();
        //rig.AddForce(new Vector3(0, -speed, 0));
        lm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
	
        if(move)
        {
            transform.Translate(newPositionAdd * speed * Time.deltaTime);
            secs += Time.deltaTime;
            if(secs > moveSecs)
            {
                move = false;
                magnet = true;
                cirColl.enabled = true;
            }
        }
        
        if(magnet)
        {
            if(Vector3.Distance(this.transform.position, player.transform.position) <= magnetRange)
            {
                Attract();
            }

        }

	}

    void Attract()
    {
        float step = magnetPower * Time.deltaTime;

        // move sprite towards the target location
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, step);
    
    }
}
