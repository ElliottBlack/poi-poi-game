using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public int levelSelected = 1;
    private Material p1Material;
    private Material p2Material;

    public Material[] fishTrails;

    private int level1Score = 0;
    private int level2Score = 0;
    private int level3Score = 0;
    private int level4Score = 0;
    private int level5Score = 0;

    private bool paused = false;

    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
        
    }
	
	// Update is called once per frame
	void Update () {
        /*pause code (ccould add menu and what not but maybe just leave out, not needed for prototype)
		if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            paused = true;
            Time.timeScale = 0;     
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            paused = false;
            Debug.Log("asd");
            Time.timeScale = 1;
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (SceneManager.GetActiveScene().name == "Title") 
            {
                //ExitApp();
            }
            else
            {
                //SceneManager.LoadScene("Title", LoadSceneMode.Single);
            }
            
        }
        */
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    //do same for p2
    public void setP1Material(int index)
    {
        p1Material = fishTrails[index];
    }

    public Material getP1Material()
    {
        return p1Material;
    }

    public void SetLevelSelected(int levelNum)
    {
        levelSelected = levelNum;
    }

    public int GetLevelSelected()
    {
        return levelSelected;
    }

    public void setScore(int score, int level)
    {
        if(level == 1)
        {
            level1Score = score;
        }
        else if (level == 2)
        {
            level2Score = score;
        }
        else if (level == 3)
        {
            level3Score = score;
        }
        else if (level == 4)
        {
            level4Score = score;
        }
        else if (level == 5)
        {
            level5Score = score;
        }
    }

    public int getScore(int level)
    {
        if (level == 1)
        {
            return level1Score;
        }
        else if (level == 2)
        {
            return level2Score;
        }
        else if (level == 3)
        {
            return level3Score;
        }
        else if (level == 4)
        {
            return level4Score;
        }
        else 
        {
            return level5Score;
        }
    }
}
