using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BellsManager : MonoBehaviour {

    public int numBells = 3;
    private int bellsGlowing = 0;
    public GameObject sakura;
    private bool sakuraSpawned = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void addBellGlowing()
    {
        bellsGlowing += 1;
        if (bellsGlowing >= numBells && !sakuraSpawned)
        {
            sakuraSpawned = true;
            Instantiate(sakura, this.transform.position, this.transform.rotation);
        }
    }

    public void bellStoppedGlowing()
    {
        bellsGlowing -= 1;
    }

}
