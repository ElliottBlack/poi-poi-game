using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Manager : MonoBehaviour {

    /// <summary>
    /// game manager stores all information about game.
    /// doesnt get destroyed. handles end of game, scoring and so on
    /// </summary>

    private int score1 = 0;
    private int score2 = 0;

    private int goalCounter = 0;
    public int goalNumber = 5;
    private int playersDead = 0;
    public GameObject wall;

    public GameObject endPanel;
    private CanvasGroup endP;
    private bool fadeOnce = false;
    public TextMeshProUGUI p1Score;
    private bool p1ScoreIncrease = true;
    private float p1DisScore = 0f;
    public TextMeshProUGUI p2Score;
    private float p2DisScore = 0f;
    private bool p2ScoreIncrease = true;
    private bool scoringFinished = false;

    public GameObject gamePanel;
    public Text txt1;
    public Text txt2;
    public Text timeDisplay;
    private bool endLevel = false;
    private float secs = 0f;
    public float waitSeconds = 5f;

    private float gameTime = 0f;
    public float gameMaxTime = 100f;

    public Sprite player1Sprite;
    public Sprite player2Sprite;
    public Image p1DisSprite;
    public Image p2DisSprite;
    public GameObject p1Image;
    public GameObject p2Image;
    private Vector3 p1StartPos;
    private Vector3 p2StartPos;
    private Vector3 p1EndPos;
    private Vector3 p2EndPos;
    private bool startScoring = false;

    public GameObject player1;
    public GameObject player2;

    public bool gameModeColours = false;
    public bool gameModeHorders = false;
    public bool gameModeAllYouCanEat = false;
    public bool gameModePickyEaters = false;

    public bool gameModeBigFish = false;
    public bool gameModeSchool = false;
    public bool gameModeExtremePowerUps = false;
    public bool gameModeTooManyPowerUps = false;

    public int p1CharacterNum = 0;
    public int p2CharacterNum = 1;

    public Sprite[] fishSprites;
    public Material[] fishMaterials;

    public MenuManager mm;

    private void Awake()
    {
        mm = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        gameModeColours = mm.gameModeColours;
        gameModeHorders = mm.gameModeHorders;
        gameModeAllYouCanEat = mm.gameModeAllYouCanEat;
        gameModePickyEaters = mm.gameModePickyEaters;
        gameModeBigFish = mm.gameModeBigFish;
        gameModeSchool = mm.gameModeSchool;
        gameModeExtremePowerUps = mm.gameModeExtremePowerUps;
        gameModeTooManyPowerUps = mm.gameModeTooManyPowerUps;
        p1CharacterNum = mm.p1CharacterNum;
        p2CharacterNum = mm.p2CharacterNum;
        fishSprites = mm.fishSprites;
    }
    // Use this for initialization
    void Start () {
        endP = endPanel.GetComponent<CanvasGroup>();
        Time.timeScale = 1f;
        // txt1.text = goalNumber.ToString();
        p1StartPos = p1Image.transform.localPosition;
        p1Image.transform.localPosition = new Vector3(p1Image.transform.localPosition.x, p1Image.transform.localPosition.y - 400f, 0f);
        p2StartPos = p2Image.transform.localPosition;
        p2Image.transform.localPosition = new Vector3(p2Image.transform.localPosition.x, p2Image.transform.localPosition.y - 400f, 0f);
        endPanel.SetActive(false);
        gamePanel.SetActive(true);


    }
	
	// Update is called once per frame
	void Update () {

        gameTime += Time.deltaTime;
        timeDisplay.text = (gameMaxTime - gameTime).ToString();
        if (gameTime >= gameMaxTime)
        {
            EndLevel();
        }

        if (endLevel||playersDead >= 2)
        {
            secs += Time.deltaTime;
            if (secs >= waitSeconds)
            {
                SceneManager.LoadScene("Title", LoadSceneMode.Single);
            }
        }
    }


    //method gets called at the end of the game handles all ui changes and transtion to winners screen done automatically.
    // should be split into smaller methods. also lotss of code runs mutliple times can group and add bool.
    public void EndLevel()
    {
        Time.timeScale = 0;
        //player1.SetActive(false);
        //player2.SetActive(false);



        p1DisSprite.sprite = fishSprites[p1CharacterNum];

        p2DisSprite.sprite = fishSprites[p2CharacterNum];


        if (score1 > score2)
        {
            mm.winnerFish = 1;
        }
        else if (score2 > score1)
        {
            mm.winnerFish = 2;
        }
        else
        {
            //tie do something
        }


        if (!fadeOnce)
        {
            endP.alpha = 0f;
            endPanel.SetActive(true);
            FadeIn();
            fadeOnce = true;
        }
        gamePanel.SetActive(false);

        if(startScoring)
        {
            if (p1ScoreIncrease && endP.alpha >= 1)
            {
                p1Score.text = Mathf.Round((p1DisScore)).ToString();
                p1DisScore += 0.4f;
                if (p1DisScore >= score1)
                {
                    p1Score.text = (score1).ToString();
                    p1ScoreIncrease = false;
                    if (score1 > score2)
                    {
                        scoringFinished = true;
                    }

                }
            }

            if (p2ScoreIncrease && endP.alpha >= 1)
            {
                p2Score.text = Mathf.Round((p2DisScore)).ToString();
                p2DisScore += 0.4f;
                if (p2DisScore >= score2)
                {
                    p2Score.text = (score2).ToString();
                    p2ScoreIncrease = false;
                    if (score2 > score1)
                    {
                        scoringFinished = true;
                    }
                }
            }
        }

        if (endP.alpha >= 1 && !startScoring && !scoringFinished)
        {
            p1Image.transform.localPosition = Vector3.Lerp(p1Image.transform.localPosition, p1StartPos, 0.02f);
            p2Image.transform.localPosition = Vector3.Lerp(p2Image.transform.localPosition, p2StartPos, 0.02f);
            if (Vector3.Distance(p1Image.transform.localPosition, p1StartPos) <= 5f)
            {
                p1EndPos = new Vector3(p1Image.transform.localPosition.x, p1Image.transform.localPosition.y + 600f, 0f);
                p2EndPos = new Vector3(p2Image.transform.localPosition.x, p2Image.transform.localPosition.y + 600f, 0f);
                p1StartPos = new Vector3(p1Image.transform.localPosition.x, p1Image.transform.localPosition.y - 400f, 0f);
                p2StartPos = new Vector3(p2Image.transform.localPosition.x, p2Image.transform.localPosition.y - 400f, 0f);
                startScoring = true;
            }
        }

        if (scoringFinished)
        {
            //p1 rise p2 fall
            if (mm.winnerFish == 1)
            {
                p1Image.transform.localPosition = Vector3.Lerp(p1Image.transform.localPosition, p1EndPos, 0.01f);
                p2Image.transform.localPosition = Vector3.Lerp(p2Image.transform.localPosition, p2StartPos, 0.01f);
                p2Image.transform.Rotate(Vector3.forward * 0.8f);
                if (Vector3.Distance(p1Image.transform.localPosition, p1EndPos) <= 100f)
                {
                    SceneManager.LoadScene("WinnersLvl", LoadSceneMode.Single);
                }
            }
            //p2 rise p1 fall
            else
            {
                p2Image.transform.localPosition = Vector3.Lerp(p2Image.transform.localPosition, p2EndPos, 0.01f);
                p1Image.transform.localPosition = Vector3.Lerp(p1Image.transform.localPosition, p1StartPos, 0.01f);
                p1Image.transform.Rotate(Vector3.back * 0.8f);
                if (Vector3.Distance(p2Image.transform.localPosition, p2EndPos) <= 100f)
                {
                    SceneManager.LoadScene("WinnersLvl", LoadSceneMode.Single);
                }
            }
        }


    }

    public void FadeIn()
    {
        StartCoroutine(DoFade());
    }

    IEnumerator DoFade()
    {
        while (endP.alpha < 1)
        {
            endP.alpha += 0.0005f;
            if (endP.alpha > 0.01f)
            {
                endP.alpha += 0.02f;
            }
            yield return null;
        }
        yield return null;
    }

    public void GetScore1(int s1)
    {
        score1 = s1;
        score1Display();

    }
    public void GetScore2(int s2)
    {
        score2 = s2;
        score2Display();

    }

    public void AddScore1(int s1)
    {
        score1 += s1;
    }
    public void AddScore2(int s2)
    {
        score2 += s2;

    }

    public void score1Display()
    {

        txt1.text = score1.ToString();

    }

    public void score2Display()
    {

        txt2.text = score2.ToString();

    }

    public void incrementGoalCounter()
    {
        goalCounter += 1;
        goalNumber -= 1; 
        if(goalNumber <= 0)
        {
            txt1.text = "Omedatou";
            if (wall != null)
            {
                Destroy(wall.gameObject);
                endLevel = true;
            }

        }
        else
        {
            txt1.text = goalNumber.ToString();
        }

    }

    public int getGoalCounter()
    {
        return goalCounter;
    }

    public int getGoalNumber()
    {
        return goalNumber;
    }

    public void playerDied()
    {
        playersDead += 1;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("Menu1", LoadSceneMode.Single);
    }
}
