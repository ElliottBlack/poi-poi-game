using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaterSelecter : MonoBehaviour {

    public Image player1Img;

    private int p1Index = 0;

    public LevelManager lm;

    public Sprite[] fishSprites;
    public Material[] fishMaterials;

    public List<Sprite> fishSpritesList = new List<Sprite>();
    public List<Material> fishMaterialsList = new List<Material>();

    private bool canSelect = true;

    public Fish fish;

    // Use this for initialization
    void Start () {       
        player1Img.sprite = fishSprites[p1Index];
        //use loop, baka
        fishSpritesList.Add(lm.fishSprites[0]);
        fishSpritesList.Add(lm.fishSprites[1]);

        fishMaterialsList.Add(lm.fishMaterials[0]);
        fishMaterialsList.Add(lm.fishMaterials[1]);
    }
	
	// Update is called once per frame
	void Update () {
        if (canSelect)
        {
            /* 2 player character select
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
            */

            if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
            {
                p1Index -= 1;
                if (p1Index < 0)
                {
                    p1Index = fishSpritesList.Count - 1;
                    //p1Index = fishSprites.Length - 1;
                }              
                player1Img.sprite = fishSpritesList[p1Index];
                fish.SetSkin(fishMaterialsList[p1Index]);
            }
            if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
            {
                p1Index += 1;
                if (p1Index >= fishSpritesList.Count)
                {
                    p1Index = 0;
                }                
                player1Img.sprite = fishSpritesList[p1Index];
                fish.SetSkin(fishMaterialsList[p1Index]);
            }
        }
    }

    public void CanSelect() {
        canSelect = false;
    }
}
