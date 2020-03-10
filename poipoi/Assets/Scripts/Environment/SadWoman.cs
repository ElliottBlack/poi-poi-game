using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SadWoman : MonoBehaviour {

    public LevelManager lm;
    private CircleCollider2D cirColl;
    private bool questActive = false;
    private bool happy = false;
    private AudioSource crying;

    public GameObject sakura;
    private GameObject spawnedSakura;
    // Use this for initialization

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Flower" && !happy)
        {
            happy = true;
            crying.Stop();
            cirColl.enabled = false;
            spawnedSakura = Instantiate(sakura, coll.transform.position, Quaternion.identity);
            spawnedSakura.GetComponent<Rigidbody2D>().AddForce(transform.up * 3000f);
        }
    }

    void Start () {
        cirColl = this.GetComponent<CircleCollider2D>();
        crying = this.GetComponent<AudioSource>();
        cirColl.radius = 5f;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
