using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFishSpawner : MonoBehaviour {

    public Vector3 spawnerLocation;
    public GameObject spawn;
    private float secs = 0f;
    public float frequency = 1f;
    public float startFrequency;
    public float spawnRange = 50f;
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
            frequency = Random.Range(startFrequency - 3f, startFrequency + 3f);
            Instantiate(spawn, new Vector3(spawnerLocation.x, Random.Range(spawnerLocation.y - spawnRange, spawnerLocation.y + spawnRange), 0), Quaternion.identity);
        }
    }


    public void Timer()
    {
        secs += Time.deltaTime;
    }
}
