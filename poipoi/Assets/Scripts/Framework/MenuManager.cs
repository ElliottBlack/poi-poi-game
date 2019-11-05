using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    /// <summary>
    /// stores variables from the menu selection.
    /// </summary>
    /// 

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
    public int winnerFish = 1;

    public Sprite[] fishSprites;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
