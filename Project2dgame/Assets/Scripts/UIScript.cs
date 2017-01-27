using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    private GameObject eOne;
    private GameObject eTwo;
    private GameObject eThree;
    private GameObject sOne;
    private GameObject sTwo;

    public playerSkills skill;

    private Image eOneImage;
    private Image eTwoImage;
    private Image eThreeImage;
    private Image sOneImage;
    private Image sTwoImage;

    private Sprite air;
    private Sprite water;
    private Sprite earth;
    private Sprite fire;
    private Sprite unused;
    private Sprite locked;

    private GameObject airG;
    private GameObject waterG;
    private GameObject earthG;
    private GameObject fireG;
    private GameObject unusedG;
    private GameObject lockedG;



    // Use this for initialization
    void Start()
    {

        airG = (GameObject)Resources.Load("Air");
        waterG = (GameObject)Resources.Load("Water");
        earthG = (GameObject)Resources.Load("Earth");
        fireG = (GameObject)Resources.Load("Fire");
        unusedG = (GameObject)Resources.Load("Unused");
        lockedG = (GameObject)Resources.Load("Locked");

        air = airG.GetComponent<SpriteRenderer>().sprite;
        water = waterG.GetComponent<SpriteRenderer>().sprite;
        earth = earthG.GetComponent<SpriteRenderer>().sprite;
        fire = fireG.GetComponent<SpriteRenderer>().sprite;
        unused = unusedG.GetComponent<SpriteRenderer>().sprite;
        locked = lockedG.GetComponent<SpriteRenderer>().sprite;


        eOne = GameObject.Find("Element_One");
        eOneImage = eOne.GetComponent<Image>();
        eTwo = GameObject.Find("Element_Two");
        eTwoImage = eTwo.GetComponent<Image>();
        eThree = GameObject.Find("Element_Three");
        eThreeImage = eThree.GetComponent<Image>();
        sOne = GameObject.Find("Special_One");
        sOneImage = sOne.GetComponent<Image>();
        sTwo = GameObject.Find("Special_Two");
        sTwoImage = sTwo.GetComponent<Image>();


        eThreeImage.sprite = locked;
        sOneImage.sprite = locked;
        sTwoImage.sprite = locked;


    }

    public void updateUI()
    {

            if (skill.elementOne.name == "uSPreFab" && eOneImage.sprite != unused)
            {
                eOneImage.sprite = unused;

            }

            if (skill.elementOne.name == "AirPrefab" && eOneImage.sprite != air)
            {
                eOneImage.sprite = air;
            }


            if (skill.elementOne.name == "WaterPrefab" && eOneImage.sprite != water)
            {
                eOneImage.sprite = water;
            }

            if (skill.elementOne.name == "EarthPrefab" && eOneImage.sprite != earth)
            {
                eOneImage.sprite = earth;
            }

            if (skill.elementOne.name == "FirePrefab" && eOneImage.sprite != fire)
            {
                eOneImage.sprite = fire;
           
        }

        if (skill.elementTwo.name != null)
        {
            Debug.Log(skill.elementTwo);
            if (skill.elementTwo.name == "uSPrefab" && eTwoImage.sprite != unused)
            {
                eTwoImage.sprite = unused;

            }

            if (skill.elementTwo.name == "AirPrefab" && eTwoImage.sprite != air)
            {
                eTwoImage.sprite = air;
            }

            if (skill.elementTwo.name == "WaterPrefab" && eTwoImage.sprite != water)
            {
                eTwoImage.sprite = water;
            }

            if (skill.elementTwo.name == "EarthPrefab" && eTwoImage.sprite != earth)
            {
                eTwoImage.sprite = earth;
            }

            if (skill.elementTwo.name == "FirePrefab" && eTwoImage.sprite != fire)
            {
                eTwoImage.sprite = fire;
            }
        }

        if (skill.elementThree.name != null)
        {
            if (skill.elementThree.name == "uSPreFab" && eThreeImage.sprite != unused)
            {
                eThreeImage.sprite = unused;

            }

            if (skill.elementThree.name == "AirPrefab" && eThreeImage.sprite != air)
            {
                eThreeImage.sprite = air;
            }

            if (skill.elementThree.name == "WaterPrefab" && eThreeImage.sprite != water)
            {
                eThreeImage.sprite = water;
            }

            if (skill.elementThree.name == "EarthPrefab" && eThreeImage.sprite != earth)
            {
                eThreeImage.sprite = earth;
            }

            if (skill.elementThree.name == "FirePrefab" && eThreeImage.sprite != fire)
            {
                eThreeImage.sprite = fire;
            }
        }

    }
}

