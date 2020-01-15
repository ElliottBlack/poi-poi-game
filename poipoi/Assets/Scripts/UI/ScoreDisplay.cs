using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreDisplay : MonoBehaviour {

    public int level = 1;
    public GameManager gm;
    private TextMeshProUGUI scoreDisplay;

    private float secs = 0f;
    private int score = 0;
    private bool scoreAdded = false;

    public Image star1;
    public Image star2;
    public Image star3;

    public int star1Par = 10;
    public int star2Par = 30;
    public int star3Par = 50;

    // Use this for initialization
    void Start () {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        scoreDisplay = this.GetComponent<TextMeshProUGUI>();
        scoreDisplay.text = score.ToString();
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!scoreAdded)
        {
            secs += Time.deltaTime;
        }

        if (secs > 0.03f && !scoreAdded)
        {
            secs = 0f;
            score += 1;
            scoreDisplay.text = score.ToString();
            if (score >= gm.getScore(level))
            {
                scoreAdded = true;
                if (score >= star1Par)
                {
                    star1.color = new Color32(255, 255, 255, 255);
                }
                if (score >= star2Par)
                {
                    star2.color = new Color32(255, 255, 255, 255);
                }
                if (score >= star3Par)
                {
                    star3.color = new Color32(255, 255, 255, 255);
                }
            }
        }

        

	}
}
