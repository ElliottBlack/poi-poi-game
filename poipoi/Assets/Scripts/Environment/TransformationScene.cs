﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformationScene : MonoBehaviour {

    public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}