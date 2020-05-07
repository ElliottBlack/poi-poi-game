using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class frogSakura : MonoBehaviour {

    private Transform petal;
    public Transform mouth;
    private bool petalCaught = false;
    public float speed = 1f;
    private float step = 0f;
    public GameObject powerSakura;
    private bool petalEaten = false;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Petal" && !petalCaught) 
        {
            Debug.Log("hit");
            petalCaught = true;
            coll.transform.SetParent(null);
            petal = coll.GetComponent<Transform>();
            petal.GetComponent<CircleCollider2D>().enabled = false;
            petal.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            petal.GetComponent<FakeGravity>().enabled = false;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(petalCaught && !petalEaten)
        {
            step = speed * Time.deltaTime;
            petal.position = Vector3.MoveTowards(petal.position, mouth.position, step);
            if(Vector3.Distance(petal.position, mouth.position) <= 0.1)
            {
                Instantiate(powerSakura, petal.position, petal.rotation);
                Destroy(petal.gameObject);
                petalEaten = true;
            }
        }
		
	}
}
