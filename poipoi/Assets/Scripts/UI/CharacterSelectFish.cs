using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectFish : MonoBehaviour {

    private Vector3 endPos;
    private Vector3 startPos;

    public float speed = 1.0F;

    // Use this for initialization
    void Start () {

        endPos = this.transform.position;
        startPos = new Vector3(endPos.x,endPos.y-1000,endPos.z);
        this.transform.position = startPos;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector3.Lerp(this.transform.position, endPos, speed);

    }

    public void MoveUp()
    {
        endPos = new Vector3(endPos.x, 2000f, endPos.z);
    }
}
