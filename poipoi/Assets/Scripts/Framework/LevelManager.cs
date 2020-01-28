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
    //0: hanami, 1: feed the frog, 2: Catch the Duck 3: woman, 4: shrine, 5:Baby TURTLE
    public TextMeshProUGUI[] questList;
    public string[] questNames;
    private bool questTextVisible = false;
    private bool skinTextVisible = false;
    private float fadeSecs = 0f;

    private bool paused = false;

    public Sprite[] fishSprites;
    public Material[] fishMaterials;
    public float soundVolume = 1f;
    public float musicVolume = 1f;

    public bool[] questsActive;
    public bool[] questsCompleted;

    // Use this for initialization
    void Start () {
        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        questText.text = "Press Esc for Menu";
        StartCoroutine(FadeTextToFullAlpha(1f, questText));
        questTextVisible = true;

        // check quests active list and apply active quest to menu.
        for (int i = 0; i < questsActive.Length; i++)
        {
            if (questsActive[i])
            {
                questList[i].text = questNames[i];

                if (questsCompleted[i])
                {
                    questList[i].text = "Completed";
                    charSelect.fishSpritesList.Add(fishSprites[2 + i]);
                    charSelect.fishMaterialsList.Add(fishMaterials[2 + i]);
                }
            }

        }
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
        //dont need string s in the method can take out
        //questText.text = s;
        if (questTextVisible)
        {
            Wait(5f);
        }
        questText.text = questNames[qn];
        questList[qn].text = questNames[qn];
        questsActive[qn] = true;
        StartCoroutine(FadeTextToFullAlpha(1f, questText));
        questTextVisible = true;
    }

    //add code to send info to gamemanager need this for saving
    public void QuestComplete(int qn)
    {
        if (questTextVisible)
        {
            Wait(5f);
        }
        questText.text = "Objective Complete";
        questList[qn].text = "Completed";
        questsCompleted[qn] = true;
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

    IEnumerator Wait(float secs)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(secs);
    }

    public void QuitToTitle()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

}
