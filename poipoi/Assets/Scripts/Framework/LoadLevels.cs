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
    public float fadeTime = 0f;
    private string levelToLoad;
    public GameObject[] buttons;
    private bool allHidden = false;
    public bool hideBelow = true;

    public GameManager gm;

    // Use this for initialization
    void Start () {
        
        startTime = Time.time;
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();

    }
	
	// Update is called once per frame
	void Update () {
        
    }

    public void setLevelSelected(int num)
    {
        gm.SetLevelSelected(num);
    }

    public void loadLevelSelect1()
    {
        fadingOut = true;
        levelToLoad = "levelSelect1";
        fadeOut();
        //SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }
    public void load1PCharactor()
    {
        fadingOut = true;
        levelToLoad = "1PCharactor";
        fadeOut();
        //SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }
    public void loadTitle()
    {
        fadingOut = true;
        levelToLoad = "Title";
        fadeOut();
        //SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }
    public void loadScoreScene()
    {
        fadingOut = true;
        levelToLoad = "ScoreScene";
        fadeOut();
        //SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }

    public void loadLevel()
    {

        fadingOut = true;
        if (gm.GetLevelSelected() == 1)
        {
            levelToLoad = "Level" + gm.GetLevelSelected().ToString();
        }
        else
        {
            levelToLoad = "FlowLevel";
        }
        
        fadeOut();
        //SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
    }

    public void LoadingLevel (int sceneindex)
    {
        StartCoroutine(LoadAsynchronously(sceneindex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while(!operation.isDone)
        {
            Debug.Log(operation.progress);

            yield return null;
        }
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
        while (t < fadeTime)
        {
            fadeA = Mathf.SmoothStep(0f, 1f, t / fadeTime);
            fade.color = new Color(0, 0, 0, fadeA);
            t += Time.deltaTime;
            yield return new WaitForSeconds(0.01f);          
        }
        if (t >= fadeTime)
        {
            SceneManager.LoadScene(levelToLoad, LoadSceneMode.Single);
        }
    }

    public void fadeOut()
    {

        // add varaibles
        fade.gameObject.SetActive(true);
        StartCoroutine("Fade");       
    }

    public void QuitGame()
    {
        Application.Quit();
    }



}
