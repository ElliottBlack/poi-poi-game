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
            Debug.Log("bubble hit");
        }
    }

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
        //rig = this.GetComponent<Rigidbody2D>();
        //rig.AddForce(new Vector3(0, -speed, 0));
        lm = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<LevelManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
