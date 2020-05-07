using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandRot : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
