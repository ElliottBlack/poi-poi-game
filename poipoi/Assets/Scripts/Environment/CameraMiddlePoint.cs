using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMiddlePoint : MonoBehaviour {

    public GameObject p1;
    public GameObject p2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.position = new Vector3((p1.transform.position.x + p2.transform.position.x)/2f, (p1.transform.position.y + p2.transform.position.y)/2f, 0f);
	    	
	}
}
