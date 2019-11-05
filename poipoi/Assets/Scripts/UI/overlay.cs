using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlay : MonoBehaviour {

    private SpriteRenderer spRen;
    private Color tmp;
    private float ac = 0f;
    private bool touched = false;
    private float secs = 0f;
    public float timeAsleep = 1;
    

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("hit");

        tmp.a = 0f;
        spRen.color = tmp;
        touched = true;


    }

    // Use this for initialization
    void Start () {
        spRen = GetComponent<SpriteRenderer>();
        tmp = spRen.color;
    }
	
	// Update is called once per frame
	void Update () {

        if (touched)
        {
            secs += Time.deltaTime;
            if (secs >= timeAsleep)
            {
                touched = false;
                secs = 0f;
                tmp.a = 1f;
                spRen.color = tmp;
            }
        }
        

    }
}
