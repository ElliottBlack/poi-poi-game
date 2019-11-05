using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailRendererHack : MonoBehaviour {

    /// <summary>
    /// attempt to make trail renderer distance based not time based.
    /// so when player stop so does the trail.
    /// still looked weird so not using at the moment
    /// </summary>

    float trailTime = 1.0f;
    float pauseTime;
    float resumeTime;

    private float testTime = 1.0f;

    public Player1 p;

    TrailRenderer trail;

    void Awake()
    {
        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("w"))
        {
           // PauseTrail();
        }
        if (Input.GetKeyDown("w"))
        {
            //ResumeTrail();
        }

        if (p.rb2d.velocity.magnitude > 0.0001)
        {
            trail.time = testTime;
        }
        else
        {
            PauseTrail();
        }
        testTime = 22/p.rb2d.velocity.magnitude;
        //Debug.Log(testTime);
    }

    void PauseTrail()
    {
        pauseTime = Time.time;
        trail.time = Mathf.Infinity;
    }

    void ResumeTrail()
    {
        resumeTime = Time.time;
        trail.time = (resumeTime - pauseTime) + trailTime;
        Invoke("SetTrailTime", trailTime);
    }

    void SetTrailTime()
    {
        trail.time = trailTime;
    }
}
