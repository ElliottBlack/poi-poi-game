using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class LevelManager : MonoBehaviour {

    public CharaterSelecter charSelect;
    public GameManager gm;
    public GameObject pausePanel;
    public TextMeshProUGUI questText;
    public TextMeshProUGUI skinText;

    //need to keep track of what quest is what index
    //0: hanami, 1: feed the frog
    public TextMeshProUGUI[] questList;

    private bool questTextVisible = false;
    private bool skinTextVisible = false;
    private float fadeSecs = 0f;

    private bool paused = false;

    public Sprite[] fishSprites;
    public Material[] fishMaterials;

    // Use this for initialization
    void Start () {
        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        questText.text = "Press Esc for Menu";
        StartCoroutine(FadeTextToFullAlpha(1f, questText));
        questTextVisible = true;
    }
	
	// Update is called once per frame
	void Update () {

        if(questTextVisible)
        {
            fadeSecs += Time.deltaTime;
            if (fadeSecs >= 3f)
            {
                if (skinTextVisible)
                {
                    skinTextVisible = false;
                    StartCoroutine(FadeTextToZeroAlpha(1f, skinText));
                }
                questTextVisible = false;
                fadeSecs = 0f;
                StartCoroutine(FadeTextToZeroAlpha(1f, questText));
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            paused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }


    }

    //add code to send info to gamemanager need this for saving
    public void QuestActivate(string s,int qn)
    {
        questText.text = s;
        questList[qn].text = s;
        StartCoroutine(FadeTextToFullAlpha(1f, questText));
        questTextVisible = true;
    }

    //add code to send info to gamemanager need this for saving
    public void QuestComplete(int qn)
    {
        questText.text = "Objective Complete";
        questList[qn].text = "Completed";
        StartCoroutine(FadeTextToFullAlpha(1f, questText));
        StartCoroutine(FadeTextToFullAlpha(1f, skinText));
        questTextVisible = true;
        skinTextVisible = true;

        charSelect.fishSpritesList.Add(fishSprites[2 + qn]);
        charSelect.fishMaterialsList.Add(fishMaterials[2 + qn]);
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 1);
        while (i.color.a > 0.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }

}
