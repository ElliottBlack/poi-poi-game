using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakuraTree : MonoBehaviour {

    private CircleCollider2D treeCollider;
    public LevelManager lm;
    public CameraFollow camFoll;
    public Camera camMain;
    public GameObject player;
    public float camSizeSpeed = 1f;
    private bool viewedSakura = false;
    private bool viewingSakura = false;

    private float camNormalSize;
    public float camSakuraSize = 200f;
    private float camSize;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {

            camFoll.target = this.gameObject;
            viewingSakura = true;
            viewedSakura = true;
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            camFoll.target = player;
            viewingSakura = false;
        }
    }

    // Use this for initialization
    void Start () {
        camNormalSize = camMain.orthographicSize;
        camSize = camNormalSize;
    }
	
	// Update is called once per frame
	void Update () {
		if(viewingSakura)
        {
            if (camMain.orthographicSize < camSakuraSize)
            {
                camMain.orthographicSize += Time.deltaTime * camSizeSpeed;
            }
        }
        else
        {
            if (camMain.orthographicSize > camNormalSize)
            {
                camMain.orthographicSize -= Time.deltaTime * camSizeSpeed;
            }
        }
	}
}
