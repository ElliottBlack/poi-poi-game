using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClone : Player1 {

    private float secs = 0f;
    private float deathTime = 7;
	
	// Update is called once per frame
	void Update () {
        PowerUp();
        secs += Time.deltaTime;
        if (secs > deathTime)
        {
            Destroy(this.gameObject);
        }
	}

}
