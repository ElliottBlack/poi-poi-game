using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckTrigger : MonoBehaviour {

    public Duck duck;
    public bool pos1;

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Player" && !duck.move)
        {
            if(pos1 && duck.atPos1)
            {
                duck.circle = true;
            }
            else if (!pos1 && !duck.atPos1)
            {
                duck.circle = true;
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
