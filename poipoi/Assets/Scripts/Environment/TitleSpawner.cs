using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSpawner : MonoBehaviour {

    /// <summary>
    /// Spawn mass amount of random petals on the title screen for effect
    /// not used at the moment.
    /// </summary>

    public GameObject[] spawn;
    public float spawnRange = 10f;
    public int numberSpawn = 2;
    private Vector3 spawnerLocation;

    // Use this for initialization
    void Start () {
        spawnerLocation = this.transform.localPosition;

        for (int i = 0; i < numberSpawn; i++)
        {
            GameObject petal = Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(Random.Range(-spawnRange + spawnerLocation.x - 30f, spawnRange + spawnerLocation.x + 30f), Random.Range(-spawnRange + spawnerLocation.y, spawnRange + spawnerLocation.y), 0), Quaternion.identity);
            Orb petalOrb = petal.GetComponent<Orb>();
            petalOrb.randomDir = true;
            petalOrb.speed = 100f;
            
        }


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
