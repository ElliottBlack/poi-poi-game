using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevels : MonoBehaviour {

    /// <summary>
    /// managers scene transitions and sends information to the meanu manager.
    /// </summary>

    public MenuManager mm;
    public Image fade;
    private float fadeA = 0f;
    private float startTime = 0f;
    private float duration = 5f;
    private float t = 0f;
    private bool fadingOut = false;
    private string levelToLoad;
    public float buttonLoadY;
    public GameObject[] buttons;
    private bool allHidden = false;
    public bool hideBelow = true;

	// Use this for initialization
	void Start () {

        startTime = Time.time;
        mm = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();

    }
	
	// Update is called once per frame
	void Update () {
        if (fadingOut)
        {
            buttonHiddenCheck();
        }
    }

    //setting meanuManagervarables
    public void setAllYouCanEat()
    {
        mm.gameModeAllYouCanEat = true;
        mm.gameModeColours = false;
        mm.gameModeHorders = false;
        loadMenu2();
    }
    //setting meanuManagervarables
    public void setColours()
    {
        mm.gameModeAllYouCanEat = false;
        mm.gameModeColours = true;
        mm.gameModeHorders = false;
        loadMenu2();
    }
    //setting meanuManagervarables
    public void setHorders()
    {
        mm.gameModeAllYouCanEat = false;
        mm.gameModeColours = false;
        mm.gameModeHorders = true;
        loadMenu2();
    }

    //setting meanuManagervarables
    public void setBigFish()
    {
        mm.gameModeBigFish = true;
        mm.gameModeSchool = false;
        mm.gameModeExtremePowerUps = false;
        mm.gameModeTooManyPowerUps = false;
        loadCharacterSelect();
    }
    //setting meanuManagervarables
    public void setSchool()
    {
        mm.gameModeBigFish = false;
        mm.gameModeSchool = true;
        mm.gameModeExtremePowerUps = false;
        mm.gameModeTooManyPowerUps = false;
        loadCharacterSelect();
    }
    //setting meanuManagervarables
    public void setExtremePowerUps()
    {
        mm.gameModeBigFish = false;
        mm.gameModeSchool = false;
        mm.gameModeExtremePowerUps = true;
        mm.gameModeTooManyPowerUps = false;
        loadCharacterSelect();
    }
    //setting meanuManagervarables
    public void setTooManyPowerUps()
    {
        mm.gameModeBigFish = false;
        mm.gameModeSchool = false;
        mm.gameModeExtremePowerUps = false;
        mm.gameModeTooManyPowerUps = true;
        loadCharacterSelect();
    }
    //setting meanuManagervarables
    public void setClassic()
    {
        mm.gameModeBigFish = false;
        mm.gameModeSchool = false;
        mm.gameModeExtremePowerUps = false;
        mm.gameModeTooManyPowerUps = false;
        loadCharacterSelect();
    }

    public void loadMenu1()
    {
        //make method for this
        //fade.color = new Color(0, 0, 0, 0);
        //fade.gameObject.SetActive(true);
        //fadeA = 0f;


        fadingOut = true;
        levelToLoad = "Menu1";
        //fadeOut();
        //SceneManager.LoadScene("Menu1", LoadSceneMode.Single);
    }

    public void loadMenu2()
    {
        fadingOut = true;
        levelToLoad = "Menu2";
    }

    public void loadNormalLevel()
    {
        fadingOut = true;
        levelToLoad = "NormalLevel";
    }

    public void loadWinnersLvl()
    {
        fadingOut = true;
        levelToLoad = "WinnersLvl";
    }

    public void loadCharacterSelect()
    {
        fadingOut = true;
        levelToLoad = "CharacterSelect";
        //SceneManager.LoadScene("CharacterSelect", LoadSceneMode.Single);
    }

    IEnumerator Fade()
    {
        //add variables for time and shit. not acutally secs?
        while (t < 10f)
        {
            fadeA = Mathf.SmoothStep(0f, 1f, t / 10);
            Debug.Log(fadeA);
            fade.color = new Color(0, 0, 0, fadeA);
            t += Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
        }
        
    }

    public void fadeOut()
    {

        // add varaibles
        StartCoroutine("Fade");
        if (t >= 10f)
        {
           // SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
        }
        
    }

    public void buttonHiddenCheck()
    {

        allHidden = true;
        if (hideBelow)
        {
            foreach (GameObject b in buttons)
            {
                if (b.transform.localPosition.y > buttonLoadY)
                {
                    allHidden = false;
                }
            }
        }
        else
        {
            foreach (GameObject b in buttons)
            {
                if (b.transform.localPosition.y < buttonLoadY)
                {
                    allHidden = false;
                }
            }
        }


        if (allHidden)
        {
            //LoadScene
            SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
        
        }
    }

    //might not need anything below here
    public void loadLevel1()
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void loadLevel2()
    {
        SceneManager.LoadScene("Level2", LoadSceneMode.Single);
    }

    public void loadTitle()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }


    public void QuitGame()
    {
        Application.Quit();
    }



}
