using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour {

    /// <summary>
    /// Added to firework petal on the winners screen
    /// when destroyed by player instantiates firework effect
    /// </summary>
    /// 
    public Rigidbody2D rig;
    public float speed = 1f;
    public GameObject dieEffectsPrefab;

    void OnDestroy()
    {
        Instantiate(dieEffectsPrefab, this.transform.position, this.transform.rotation);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "Player2")
        {
            Destroy(this.gameObject);
        }          
    }

    // Use this for initialization
    void Start () {
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
        rig = this.GetComponent<Rigidbody2D>();
        rig.AddForce(new Vector3(0, -speed, 0));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
