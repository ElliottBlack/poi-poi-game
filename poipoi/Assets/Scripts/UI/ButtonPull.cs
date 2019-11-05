using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPull : MonoBehaviour {

    private SpringJoint2D spring;
    private float n = 1692f;
    private float y;

	// Use this for initialization
	void Start () {
        spring = this.GetComponent<SpringJoint2D>();
        
	}
	
	// Update is called once per frame
	void Update () {
       // y = 568f - (274f / 241f) * Screen.height;
      // this.transform.localPosition = new Vector3(this.transform.localPosition.x, y, 0f);
       // Debug.Log(Screen.height + "---" +  y);
        
        
	}
}
