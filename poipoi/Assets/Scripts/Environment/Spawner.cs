using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    
    /// <summary>
    /// spawns petals in random location just above camera. 
    /// small random change in frequency
    /// </summary>
    
    public Manager man;
    public bool powerUpSpawner = false;
    public GameObject[] spawn;
    public float frequency = 1f;
    public bool ranFreq = false;
    public float startFrequency;
    public float spawnRange = 10f;
    public int numberSpawn = 2;
    public Vector3 spawnerLocation;

    public bool downSpawn = false;
    public bool winnersLvl = false;

    public float secs = 0f;

    public bool disableColl = false;

	// Use this for initialization
	void Start () {
        spawnerLocation = this.transform.localPosition;


        startFrequency = frequency;

        if (ranFreq)
        {
            frequency = Random.Range(startFrequency - 2f, startFrequency + 2f);
        }

    }
	
	// Update is called once per frame
	void Update () {
        Timer();
        if (secs >= frequency && !downSpawn)
        {
            secs = 0f;
            for (int i = 0; i < numberSpawn; i++)
            {
                Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x, Random.Range(-spawnRange + spawnerLocation.y, spawnRange + spawnerLocation.y), 0), Quaternion.identity);
            }   
        }
        else if (secs >= frequency && downSpawn)
        {
            secs = 0f;
            if (ranFreq)
            {
                frequency = Random.Range(startFrequency - 2f, startFrequency + 2f);
            }
            for (int i = 0; i < numberSpawn; i++)
            {
                Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(Random.Range(-spawnRange + spawnerLocation.x, spawnRange + spawnerLocation.x), spawnerLocation.y, 0), Quaternion.identity);
            }
        }

	}

    public void Timer() {
        secs += Time.deltaTime;
    }
}
