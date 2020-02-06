using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePetalSpawner : MonoBehaviour {

    public GameObject petal;
    public float frequency = 1f;
    public float spawnRange = 5f;
    private float secs = 0f;
    private Vector3 petalPosition;
    private Quaternion petalRot;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        secs += Time.deltaTime;
        if(secs >= frequency)
        {
            petalPosition = new Vector3(this.transform.position.x + Random.Range(-spawnRange, spawnRange), this.transform.position.y + Random.Range(-spawnRange, spawnRange), 0f);
            petalRot = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            Instantiate(petal, petalPosition, petalRot);
            secs = 0f;
        }
	}
}
