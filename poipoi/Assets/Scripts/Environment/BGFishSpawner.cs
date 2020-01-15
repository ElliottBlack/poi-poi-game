using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFishSpawner : MonoBehaviour {

    public Vector3 spawnerLocation;
    public GameObject spawn;
    private float secs = 0f;
    public float frequency = 1f;
    public float startFrequency;
    // Use this for initialization
    void Start () {
        spawnerLocation = this.transform.localPosition;

        startFrequency = frequency;

        
        
    }
	
	// Update is called once per frame
	void Update () {
        Timer();
        if (secs >= frequency)
        {
            secs = 0f;
            frequency = Random.Range(startFrequency - 0.5f, startFrequency + 0.5f);
            Instantiate(spawn, new Vector3(spawnerLocation.x, Random.Range(spawnerLocation.y - 30f, spawnerLocation.y + 30f), 0), Quaternion.identity);
        }
    }


    public void Timer()
    {
        secs += Time.deltaTime;
    }
}
