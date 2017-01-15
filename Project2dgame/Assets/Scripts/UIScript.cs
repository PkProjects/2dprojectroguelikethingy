using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour {

    private GameObject eOne;
    private GameObject eTwo;
    private GameObject eThree;

    public playerSkills skill;

    private Image eOneImage;
    private Image eTwoImage;
    private Image eThreeImage;

    private Sprite air;
    private Sprite water;
    private Sprite earth;
    private Sprite fire;

    private GameObject airG;
    private GameObject waterG;
    private GameObject earthG;
    private GameObject fireG;
    

    private string currentOne;


	// Use this for initialization
	void Start () {

        airG = (GameObject)Resources.Load("Air");
        waterG = (GameObject)Resources.Load("Water");
        earthG = (GameObject)Resources.Load("Earth");
        fireG = (GameObject)Resources.Load("Fire");

        air = airG.GetComponent<SpriteRenderer>().sprite;
        water = waterG.GetComponent<SpriteRenderer>().sprite;
        earth = earthG.GetComponent<SpriteRenderer>().sprite;
        fire = fireG.GetComponent<SpriteRenderer>().sprite;

        eOne = GameObject.Find("Element_One");
        eOneImage = eOne.GetComponent<Image>();
        eTwo = GameObject.Find("Element_Two");
        eTwoImage = eTwo.GetComponent<Image>();
        eThree = GameObject.Find("Element_Three");
        eThreeImage = eThree.GetComponent<Image>();
        
	}

  
	// Update is called once per frame
	void Update () {

        Debug.Log(skill.elementOne.name);
        if (skill.elementOne.name != null)
        {
            if (skill.elementOne.name == "AirPrefab")
            {
               eOneImage.sprite = air;
            }

            if (skill.elementOne.name == "WaterPreFab")
            {
                eOneImage.sprite = water;
            }

            if (skill.elementOne.name == "EarthPreFab")
            {
                eOneImage.sprite = earth;
            }

            if (skill.elementOne.name == "FirePrefab")
            {
                eOneImage.sprite = fire;
            }
        }

        if (skill.elementTwo.name != null)
        {
            if (skill.elementTwo.name == "AirPrefab")
            {
                eTwoImage.sprite = air;
            }

            if (skill.elementTwo.name == "WaterPreFab")
            {
                eTwoImage.sprite = water;
            }

            if (skill.elementTwo.name == "EarthPreFab")
            {
                eTwoImage.sprite = earth;
            }

            if (skill.elementTwo.name == "FirePrefab")
            {
                eTwoImage.sprite = fire;
            }
        }







    }
}
