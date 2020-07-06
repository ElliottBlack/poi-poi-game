using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGravity : MonoBehaviour {
    public Rigidbody2D rb2d;
    private float secs = 0f;
    public float gravityStrength = 20f;
    private float deathTime = 0f;
    public Vector3 gravityOrigin;
    public float spawnForce = 500f;
    public Sprite[] petalSprites;
    private SpriteRenderer spriteRen;
    public float deathRangeUp = 180f;
    public float deathRangeLow = 60f;
    // Use this for initialization
    void Start () {
        spriteRen = this.GetComponent<SpriteRenderer>();
        spriteRen.sprite = petalSprites[Random.Range(0,4)];
        rb2d = GetComponent<Rigidbody2D>();
        transform.Rotate(0f, 0f, Random.Range(0f, 360f));
        deathTime = Random.Range(deathRangeLow, deathRangeUp);

        rb2d.AddForce(spawnForce * Random.insideUnitCircle.normalized);
    }
	
	// Update is called once per frame
	void Update () {
        //Define our gravity origin in world space. (This could be an objects transform.position )
       // Vector3 gravityOrigin = new Vector3(0.0f, 0.5f, 0.0f);

        //Get a vector to our gravity origin from object and normalize.
        Vector3 toGravityOriginFromObject = gravityOrigin - gameObject.transform.position;
        toGravityOriginFromObject.Normalize();

        //Multiply vector so that the magnitude is equal to the force we wish to apply.
        float accelerationDueToGravity = gravityStrength;
        toGravityOriginFromObject *= accelerationDueToGravity * rb2d.mass * Time.deltaTime;

        //Apply our acceleration.
        rb2d.AddForce(toGravityOriginFromObject, ForceMode2D.Force);

        secs += Time.deltaTime;
        if (secs >= deathTime)
        {
            Destroy(this.gameObject);
        }
    }
}
