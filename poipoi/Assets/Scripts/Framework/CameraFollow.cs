using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    //speed between 0 and 1. 1 being fast.
    public float speed = 0.25f;
    private float startSpeed;
    public float interpVelocity;
    public float minDistance;
    public float followDistance;
    public GameObject target;
    public Vector3 offset;
    Vector3 targetPos;

    public bool[] pond;
    private float secs = 0f;
    private bool lookAtShrine = true;
    private Camera cam;
    public LevelManager lm;
    // Use this for initialization
    void Start()
    {
        lm = this.GetComponent<LevelManager>();
        targetPos = transform.position;
        startSpeed = speed;
        cam = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(pond[0])
        {
            pond1Opening();
        }

        if (target)
        {
            Vector3 posNoZ = transform.position;
            posNoZ.z = target.transform.position.z;

            Vector3 targetDirection = (target.transform.position - posNoZ);

            interpVelocity = targetDirection.magnitude * 1f;

            targetPos = transform.position + (targetDirection.normalized * interpVelocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, targetPos + offset, speed);

        }
    }

    void pond1Opening()
    {
        if(lookAtShrine)
        {
            speed = 0f;

            if (secs >= 2f)
            {
                lookAtShrine = false;
            }
        }
        else
        {
            if(speed < 3f)
            {
                speed += 0.0001f;
                if(lm.powerPetalsSum > 0)
                {
                    speed = 3f;
                    lm.popupMessage("Collect the Gold Sakura");
                }
            }

            if(cam.orthographicSize >50f)
            {
                cam.orthographicSize -= 0.1f;
                if(cam.orthographicSize <= 50f)
                {
                    cam.orthographicSize = 50f;
                    pond[0] = false;
                }
            }
        }



        secs += Time.deltaTime;
        //start paused on shrine 
        // zoom in and follow gold petal to fish at bottom of waterfall.

    }

}
