using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour {

    private CircleCollider2D trig;
    public float[] speed;
    private float secs = 0f;
    public float[] aliveSecs;
    public int index = 0;

    // Use this for initialization
    void Start () {
        trig = this.GetComponent<CircleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        trig.radius += Time.deltaTime * speed[index];
        secs += Time.deltaTime;
        if (secs >= aliveSecs[index])
        {
            Destroy(this.gameObject);
        }
	}
}
