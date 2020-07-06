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
    public Slider soundSlider;
    public Slider musicSlider;
    public TextMeshProUGUI displayText;

    //need to keep track of what quest is what index
    //0: hanami, 1: feed the frog, 2: Catch the Duck 3: woman, 4: shrine, 5:Baby TURTLE
    public TextMeshProUGUI[] questList;
    public string[] questNames;
    private bool displayTextVisible = false;
    private bool fadeInText = true;
    private float fadeSecs = 0f;

    private bool paused = false;

    public Sprite[] fishSprites;
    public Material[] fishMaterials;
    private float soundVolume = 1f;
    private float musicVolume = 1f;

    public TextMeshProUGUI sakuraCounter;

    public TextMeshProUGUI[] sakuraQuestsList;

    public bool[] pond1Active;
    public bool[] pond1Completed;

    public int[] pondSakuraNumber;

    public Collider2D[] waterfallColliders;

    public SpriteRenderer[] waterfallRocks;
    public SpriteRenderer[] waterfallPond1;
    public SpriteRenderer[] waterfallPond2;
    public SpriteRenderer[] waterfallPond3;
    public SpriteRenderer[] waterfallPond4;

    public TrailRenderer fishTrail;
    public Fish fish;

    public int powerPetalsSum = 0;


    // Use this for initialization
    void Start () {

        //gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

        //start of game drisplay message on how to puase probs dont need
        //displayText.text = "Press Esc for Menu";
        //StartCoroutine(FadeTextToFullAlpha(1f, displayText));

    }
	
	// Update is called once per frame
	void Update () {

        if (displayText.color.a == 0)
        {
            displayTextVisible = false;
        }
        
        if(fadeInText)
        {
            fadeSecs += Time.deltaTime;
            if (fadeSecs >= 3f)
            {
                fadeInText = false;
                fadeSecs = 0f;
                StartCoroutine(FadeTextToZeroAlpha(1f, displayText));
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            //pausePanel.SetActive(true);
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            // pausePanel.SetActive(false);
            Unpause();
        }


    }

    /*
    //add code to send info to gamemanager need this for saving
    public void QuestActivate(int qn)
    {
        //dont need string s in the method can take out
        //questText.text = s;
        if (displayTextVisible)
        {
            StartCoroutine(Wait(5, qn, true));
        }
        else
        {
            if (fish.pondNum[0])
            {
                displayText.text = pond1QuestNames[qn];
                pond1Quests[qn].text = pond1QuestNames[qn];
                pond1Active[qn] = true;
            }
            //displayText.text = questNames[qn];
            //questList[qn].text = questNames[qn];
            //questsActive[qn] = true;
            StartCoroutine(FadeTextToFullAlpha(1f, displayText));

        }

    }

    //add code to send info to gamemanager need this for saving
    public void QuestComplete(int qn)
    {
        if (displayTextVisible)
        {
            StartCoroutine(Wait(5f, qn, false));
        }
        else
        {

            displayText.text = "Objective Complete";
            questList[qn].text = "Completed";
            questsCompleted[qn] = true;
            StartCoroutine(FadeTextToFullAlpha(1f, displayText));


            charSelect.fishSpritesList.Add(fishSprites[2 + qn]);
            charSelect.fishMaterialsList.Add(fishMaterials[2 + qn]);

            foreach (bool q in questsCompleted)
            {
                if (q)
                {
                    numQuentsCompleted += 1;
                }
            }
            if (numQuentsCompleted == questsCompleted.Length - 1)
            {
                QuestActivate(6);
            }
            else if(numQuentsCompleted >= questsCompleted.Length)
            {
                fishTrail.time = 3f;
                fish.speed += 800f;
            }
            else
            {
                numQuentsCompleted = 0;
            }
        }


    }
    */

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        displayTextVisible = true;
        fadeInText = true;
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

    //waits until text has left the screen
    IEnumerator Wait(float secs, int qn, bool act)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(secs);
        /*
        if (act)
        {
            QuestActivate(qn);
        }
        else
        {
            QuestComplete(qn);
        }
        */
    }

    public void PowerPetalCollected(bool add)
    {
        if(add)
        {
            powerPetalsSum += 1;
        }

        for (int i = 0; i < fish.pondNum.Length; i++)
        {
            if (fish.pondNum[i])
            {
                sakuraCounter.text = powerPetalsSum + " / " + pondSakuraNumber[i];
                if (powerPetalsSum >= pondSakuraNumber[i])
                {
                    waterfallColliders[i].enabled = false;

                    //needs array of rocks for each waterfall

                    if(i == 0)
                    {
                        waterfallRocks = waterfallPond1;
                    }
                    else if(i == 1)
                    {
                        waterfallRocks = waterfallPond2;
                    }
                    else if (i == 2)
                    {
                        waterfallRocks = waterfallPond3;
                    }
                    else if (i == 3)
                    {
                        waterfallRocks = waterfallPond4;
                    }

                    foreach (SpriteRenderer rock in waterfallRocks)
                    {
                        rock.sortingLayerName = "bottom";
                    }

                    //needs array of strings foreach level.
                    displayText.text = "Climb the Waterfall";
                    StartCoroutine(FadeTextToFullAlpha(1f, displayText));
                }
            }
        }  
    }

    public void QuitToTitle()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    public void Pause()
    {
        paused = true;
        soundSlider.value = soundVolume;
        musicSlider.value = musicVolume;
        pausePanel.SetActive(true);
        Time.timeScale = 0;

    }
    public void Unpause()
    {
        paused = false;
        soundVolume = soundSlider.value;
        musicVolume = musicSlider.value;
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void popupMessage(string s)
    {
        displayText.text = s;
        StartCoroutine(FadeTextToFullAlpha(1f, displayText));
    }

    public void setSoundVolume(float x)
    {
        soundVolume = x;
    }

    public float getSoundVolume()
    {
        return soundVolume;
    }


    public void setMusicVolume(float x)
    {
        musicVolume = x;
    }

    public float getMusicVolume()
    {
        return musicVolume;
    }

}
