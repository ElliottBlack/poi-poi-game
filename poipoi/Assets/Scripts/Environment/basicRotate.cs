using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicRotate : MonoBehaviour {

    public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.forward * speed);
	}
}
