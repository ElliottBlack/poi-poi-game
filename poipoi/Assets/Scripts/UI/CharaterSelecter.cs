using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterSelecter : MonoBehaviour {

    public Image player1Img;
    public Image player2Img;

    private int p1Index = 0;
    private int p2Index = 1;

    public MenuManager mm;

    public Sprite[] fishSprites;

    private bool canSelect = true;

    // Use this for initialization
    void Start () {
        mm = GameObject.FindGameObjectWithTag("MenuManager").GetComponent<MenuManager>();
        fishSprites = mm.fishSprites;
        player2Img.sprite = fishSprites[p2Index];
        player1Img.sprite = fishSprites[p1Index];
    }
	
	// Update is called once per frame
	void Update () {
        if (canSelect)
        {
            if (Input.GetKeyDown("a"))
            {
                p2Index -= 1;
                if (p2Index < 0)
                {
                    p2Index = fishSprites.Length - 1;
                }
                if (p2Index == p1Index)
                {
                    p2Index -= 1;
                    if (p2Index < 0)
                    {
                        p2Index = fishSprites.Length - 1;
                    }
                }
                player2Img.sprite = fishSprites[p2Index];
            }
            if (Input.GetKeyDown("d"))
            {
                p2Index += 1;
                if (p2Index >= fishSprites.Length)
                {
                    p2Index = 0;
                }
                if (p2Index == p1Index)
                {
                    p2Index += 1;
                    if (p2Index >= fishSprites.Length)
                    {
                        p2Index = 0;
                    }
                }
                player2Img.sprite = fishSprites[p2Index];
            }

            if (Input.GetKeyDown("left"))
            {
                p1Index -= 1;
                if (p1Index < 0)
                {
                    p1Index = fishSprites.Length - 1;
                }
                if (p2Index == p1Index)
                {
                    p1Index -= 1;
                    if (p1Index < 0)
                    {
                        p1Index = fishSprites.Length - 1;
                    }
                }
                player1Img.sprite = fishSprites[p1Index];
            }
            if (Input.GetKeyDown("right"))
            {
                p1Index += 1;
                if (p1Index >= fishSprites.Length)
                {
                    p1Index = 0;
                }
                if (p2Index == p1Index)
                {
                    p1Index += 1;
                    if (p1Index >= fishSprites.Length)
                    {
                        p1Index = 0;
                    }
                }
                player1Img.sprite = fishSprites[p1Index];
            }
        }
    }

    public void setCharacterNums()
    {
        mm.p1CharacterNum = p1Index;
        mm.p2CharacterNum = p2Index;
    }

    public void CanSelect() {
        canSelect = false;
    }
}
