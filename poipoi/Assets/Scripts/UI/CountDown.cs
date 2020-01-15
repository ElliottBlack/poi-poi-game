using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    private float startTime = 0f;
    public TextMeshProUGUI countdown;
    private bool spawnersActive = false;
    public GameObject petalSpawner;
    public GameObject powerSpawner;
    // Use this for initialization
    void Start()
    {
        countdown = this.GetComponent<TextMeshProUGUI>();
        //petalSpawner.SetActive(false);
        //powerSpawner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        startTime += Time.deltaTime;
        countdown.text = Mathf.Ceil(5 - startTime).ToString();
        if (startTime >= 4f && !spawnersActive)
        {
            //petalSpawner.SetActive(true);
            //powerSpawner.SetActive(true);
            spawnersActive = true;
        }

        if (startTime >= 5f)
        {
            Destroy(this.gameObject);
        }

    }
}
