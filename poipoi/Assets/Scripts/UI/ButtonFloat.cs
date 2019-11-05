using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFloat : MonoBehaviour {

    public GameObject anchor;

    private Vector3 startMarker;
    private Vector3 endMarker;
    

    public float speed = 1.0F;

    private float startTime;

    private float journeyLength;

    private Rigidbody2D rig;
    private float g;
    private bool calG = true;

    // Use this for initialization
    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;

        startMarker = this.transform.position;
        endMarker = anchor.transform.position;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker, endMarker);

        rig = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        if (calG)
        {
            g = Screen.height * 0.0722222f - 0.05f;
            rig.gravityScale = g;
        }

       // Debug.Log(Screen.height + "---" + rig.gravityScale);

        // Distance moved = time * speed.
        float distCovered = (Time.time - startTime) * speed;

        // Fraction of journey completed = current distance divided by total distance.
        float fracJourney = distCovered / journeyLength;

        // Set our position as a fraction of the distance between the markers.
       // transform.position = Vector3.Lerp(this.transform.position, endMarker, fracJourney);
    }

    public void destroyAnchor() {
        calG = false;
        Destroy(anchor.gameObject);
        rig.gravityScale = rig.gravityScale*4f;
    }
}
