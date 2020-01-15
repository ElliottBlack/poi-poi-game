using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : Spawner {

    /// <summary>
    /// Spawns the petals in different patterns
    /// only a few patterns added so far
    /// plan to add more later
    /// at the moment only spawns 1 pattern after given time used for testing 1 pattern at a time
    /// in future will need to add code for random different pattern everytime
    /// </summary>

    private bool spawning = false;
    private int spawnPatternIndex = 0;
    private GameObject pet;
    private Vector3 blobPos;

    /*
	// Use this for initialization
	void Start () {
		
	}*/
	
	// Update is called once per frame
	void Update () {
        Timer();
        if (secs >= frequency && !spawning)
        {
            spawning = true;
            secs = 0f;
            if (ranFreq)
            {
                frequency = Random.Range(startFrequency - 2f, startFrequency + 2f);
            }

            spawnPatternIndex = Random.Range(0,5);
            spawnPatternIndex = 5;
            if (spawnPatternIndex == 0)
            {
                CrossPattern();
            }
            else if (spawnPatternIndex == 1)
            {
                RainPattern();
            }
            else if (spawnPatternIndex == 2)
            {
                CirclePattern();
            }
            else if (spawnPatternIndex == 3)
            {
                SnowFlakePattern();
            }
            else if (spawnPatternIndex == 4)
            {
                SquarePattern();
            }
            else if (spawnPatternIndex == 5)
            {
                Blobs();
            }

        }
        else 
        {
           
        }
    }

    //working
    private void CrossPattern()
    {
        for (int i = 0; i < spawnRange; i++)
        {
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(-spawnRange + 2*i, spawnerLocation.y+i, 0), Quaternion.identity);          
        }

        for (int i = 0; i < spawnRange; i++)
        {
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnRange - 2*i, spawnerLocation.y + i, 0), Quaternion.identity);
        }
        spawning = false;
    }

    // spawn rain drops in random locaitons, staggered
    private void RainPattern()
    {
        for (int j = 0; j <5; j++)
        {
            float xPos = Random.Range(-spawnRange + spawnerLocation.x, spawnRange + spawnerLocation.x);
            for (int i = 0; i < 10; i++)
            {
                Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(xPos, spawnerLocation.y + 3*i + 10*j, 0), Quaternion.identity);
            }           
        }
        spawning = false;
    }

    //makes cicle outline, not perfect use polar coordinates would be better
    private void CirclePattern()
    {
        // random cicle center, start with at zero,zero
        float xPos = Random.Range(-spawnRange + spawnerLocation.x, spawnRange + spawnerLocation.x);

        for (int i = -60; i < 60; i++)
        {
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x + i, spawnerLocation.y+20 + Mathf.Sqrt(400f - i*i), 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x + i, spawnerLocation.y+20 - Mathf.Sqrt(400f - i*i), 0), Quaternion.identity);
        }


        spawning = false;
    }

    //makes 3 cross snowflakes (add better loop, ima code dulication bad programming) 
    private void SnowFlakePattern()
    {
        for (int i = -5; i < 5; i++)
        {
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x, spawnerLocation.y + 3*i + 20, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x + 3*i, spawnerLocation.y + 20, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x + 3*i, spawnerLocation.y + 3*i + 20, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x - 3*i, spawnerLocation.y + 3*i + 20, 0), Quaternion.identity);
        }

        for (int i = -5; i < 5; i++)
        {
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x -40, spawnerLocation.y + 3 * i + 50, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x + 3 * i-40, spawnerLocation.y + 50, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x + 3 * i-40, spawnerLocation.y + 3 * i + 50, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x - 3 * i-40, spawnerLocation.y + 3 * i + 50, 0), Quaternion.identity);
        }

        for (int i = -5; i < 5; i++)
        {
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x+40, spawnerLocation.y + 3 * i + 70, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x + 3 * i+40, spawnerLocation.y + 70, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x + 3 * i+40, spawnerLocation.y + 3 * i + 70, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x - 3 * i+40, spawnerLocation.y + 3 * i + 70, 0), Quaternion.identity);
        }

        spawning = false;
    }

    private void SquarePattern()
    {      
        for (int i = -20; i < 20; i++)
        {
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x+i, spawnerLocation.y, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x+i, spawnerLocation.y+40, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x+20, spawnerLocation.y+i+20, 0), Quaternion.identity);
            Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(spawnerLocation.x-20, spawnerLocation.y+i+20, 0), Quaternion.identity);
        }

        spawning = false;
    }

    private void Blobs()
    {

        float xPos = Random.Range(-spawnRange + spawnerLocation.x, spawnRange + spawnerLocation.x);
        for (int i = 0; i < 60; i++)
        {
            //pet = Instantiate(spawn[Random.Range(0, spawn.Length)], new Vector3(Random.Range(xPos - 10f, xPos + 10f), Random.Range(spawnerLocation.y + 3f, spawnerLocation.y + 23f), 0), Quaternion.identity);
            blobPos = Random.insideUnitCircle * 10;
            blobPos = new Vector3(blobPos.x + xPos, blobPos.y + spawnerLocation.y + 5f, 0f);
            pet = Instantiate(spawn[Random.Range(0, spawn.Length)], blobPos, Quaternion.identity);
            pet.GetComponent<Orb>().randomSpeed = false;
        }

        spawning = false;
    }


}
