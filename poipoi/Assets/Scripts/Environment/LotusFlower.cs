using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LotusFlower : MonoBehaviour {

    private Animator ani;
    private bool opened = false;

    public bool spawnPetal = false;
    public GameObject petal;

    private bool opening = false;
    private float secs = 0f;
    private float waitSecs = 1f;

    public float moveSpeed = 1f;
    public float moveSecs = 5f; 

    private void OnTriggerEnter2D(Collider2D coll)
    {

        if (coll.gameObject.tag == "Bubble" && !opened)
        {
            ani.SetBool("open", true);
            opened = true;
            opening = true;
        }
    }

    // Use this for initialization
    void Start () {
        ani = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		if (opening)
        {
            secs += Time.deltaTime;
            if (secs > waitSecs)
            {
                opening = false;
                GameObject f = Instantiate(petal, this.transform.position, Quaternion.identity);
                f.GetComponent<Fireworks>().moveSecs = moveSecs;
                f.GetComponent<Fireworks>().speed = moveSpeed;

            }
        }
	}
}
