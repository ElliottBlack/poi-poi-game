using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splash : MonoBehaviour {

    
    public AudioClip[] splashes;
    AudioSource audioSource;
    private float secs = 0f; 
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        Timer();
        if (secs > 5f)
        {
            audioSource.PlayOneShot(splashes[Random.Range(0, splashes.Length - 1)], 0.1F);
            secs = 0f;
        }
    }

    void Timer()
    {
        secs += Time.deltaTime;
    }
}
