using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class playerSkills : MonoBehaviour
{

    public int skillOne = 1;
    public int skillTwo = 2;

    public float elementSpeed = 2f;

    public GameObject elementOne = null;
    public GameObject elementTwo = null;
    public GameObject elementThree = null;

    public bool unlockTwo;
    public bool unlockThree;

    public GameObject shootLocOne;
    public GameObject shootLocTwo;
    public GameObject shootLocthree;

    public GameObject airPreFab;
    public GameObject waterPreFab;
    public GameObject earthPreFab;
    public GameObject firePreFab;
    public GameObject uSPreFab;
    public GameObject icePreFab;
    public GameObject steamPreFab;
    public GameObject dustPreFab;
    public GameObject infernoPreFab;
    public GameObject naturePreFab;
    public GameObject lavaPreFab;

    private GameObject UI;



    // Use this for initialization
    void Start()
    {

        airPreFab = (GameObject)Resources.Load("Elements/AirPrefab");
        waterPreFab = (GameObject)Resources.Load("Elements/WaterPrefab");
        earthPreFab = (GameObject)Resources.Load("Elements/EarthPrefab");
        firePreFab = (GameObject)Resources.Load("Elements/FirePrefab");
        icePreFab = (GameObject)Resources.Load("Elements/IcePrefab");
        steamPreFab = (GameObject)Resources.Load("Elements/SteamPrefab");
        dustPreFab = (GameObject)Resources.Load("Elements/DustPrefab");
        infernoPreFab = (GameObject)Resources.Load("Elements/LavaPrefab");
        naturePreFab = (GameObject)Resources.Load("Elements/NaturePrefab");
        lavaPreFab = (GameObject)Resources.Load("Elements/LavaPrefab");
        uSPreFab = (GameObject)Resources.Load("Elements/uSPrefab");



        elementOne = uSPreFab;
        elementTwo = uSPreFab;
        elementThree = uSPreFab;

        UI = GameObject.Find("UI");




    }

    public void activeAbility(int type)
    {
        if (!unlockTwo)
        {
            if (type == 2)
            {
                elementTwo = airPreFab;
            }

            if (type == 3)
            {
                elementTwo = waterPreFab;
            }

            if (type == 4)
            {
                elementTwo = earthPreFab;
            }

            if (type == 5)
            {
                elementTwo = firePreFab;
            }

            UI.GetComponent<UIScript>().updateUI();
        }

        if (unlockTwo && !unlockThree)
        {
            if (type == 2)
            {
                elementThree = airPreFab;
            }

            if (type == 3)
            {
                elementThree = waterPreFab;
            }

            if (type == 4)
            {
                elementThree = earthPreFab;
            }

            if (type == 5)
            {
                elementThree = firePreFab;
            }

            UI.GetComponent<UIScript>().updateUI();
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Left_Mouse buttton normal attack
        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (elementOne != uSPreFab)
            {
                GameObject eleOne = Instantiate(elementOne, shootLocOne.transform.position, gameObject.transform.rotation) as GameObject;
                eleOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
               
                if (elementOne == airPreFab)
                {
                    eleOne.GetComponent<ElementScript>().eType = 2;
                }
                else


                if (elementOne == waterPreFab)
                {
                    eleOne.GetComponent<ElementScript>().eType = 3;
                }
                else


                if (elementOne == earthPreFab)
                {
                    eleOne.GetComponent<ElementScript>().eType = 4;
                }
                else


                if (elementOne == firePreFab)
                {
                    eleOne.GetComponent<ElementScript>().eType = 5;
                }
            }
        }

        //Right_Mouse button normal attack
        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (elementTwo != uSPreFab)
            {
                GameObject eleTwo = Instantiate(elementTwo, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                eleTwo.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);

                if (elementTwo == airPreFab)
                {
                    eleTwo.GetComponent<ElementScript>().eType = 2;
                }
                else


           if (elementOne == waterPreFab)
                {
                    eleTwo.GetComponent<ElementScript>().eType = 3;
                }
                else


           if (elementOne == earthPreFab)
                {
                    eleTwo.GetComponent<ElementScript>().eType = 4;
                }
                else


           if (elementOne == firePreFab)
                {
                    eleTwo.GetComponent<ElementScript>().eType = 5;
                }
            }

        }

        //Ability reset
        if (Input.GetKeyUp(KeyCode.Mouse1) && unlockTwo == false)
        {
            elementTwo = uSPreFab;
            Debug.Log(elementTwo);
            UI.GetComponent<UIScript>().updateUI();

        }

        //Middle_Mouse button normal attack
        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse2))
        {
            if (elementThree != uSPreFab)
            {
                GameObject eleThree = Instantiate(elementThree, shootLocthree.transform.position, gameObject.transform.rotation) as GameObject;
                eleThree.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);

                if (elementThree == airPreFab)
                {
                    eleThree.GetComponent<ElementScript>().eType = 2;
                }
                else


           if (elementThree == waterPreFab)
                {
                    eleThree.GetComponent<ElementScript>().eType = 3;
                }
                else


           if (elementThree == earthPreFab)
                {
                    eleThree.GetComponent<ElementScript>().eType = 4;
                }
                else


           if (elementThree == firePreFab)
                {
                    eleThree.GetComponent<ElementScript>().eType = 5;
                }
            }

        }

        //ability reset
        if (Input.GetKeyUp(KeyCode.Mouse2) && unlockTwo == true && unlockThree == false)
        {
            elementThree = uSPreFab;
            UI.GetComponent<UIScript>().updateUI();

        }

        //Special attacks
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Ice
            if (elementOne == airPreFab && elementTwo == waterPreFab)
            {
                GameObject comOne = Instantiate(icePreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 6;
            }

            //Dust
            if (elementOne == airPreFab && elementTwo == earthPreFab)
            {
                GameObject comOne = Instantiate(dustPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 7;
            }

            //Inferno
            if (elementOne == airPreFab && elementTwo == firePreFab)
            {
                GameObject comOne = Instantiate(infernoPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 8;
            }

            //Nature
            if (elementOne == waterPreFab && elementTwo == earthPreFab)
            {
                GameObject comOne = Instantiate(naturePreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 9;
            }

            //Steam
            if (elementOne == waterPreFab && elementTwo == firePreFab)
            {
                GameObject comOne = Instantiate(steamPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 10;
            }

            //Lava
            if (elementOne == earthPreFab && elementTwo == firePreFab)
            {
                GameObject comOne = Instantiate(lavaPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 11;
            }


        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Ice
            if (elementThree == airPreFab && elementTwo == waterPreFab)
            {
                GameObject comOne = Instantiate(icePreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 6;
            }

            //Dust
            if (elementThree == airPreFab && elementTwo == earthPreFab)
            {
                GameObject comOne = Instantiate(dustPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 7;
            }

            //Inferno
            if (elementThree == airPreFab && elementTwo == firePreFab)
            {
                GameObject comOne = Instantiate(infernoPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 8;
            }

            //Nature
            if (elementThree == waterPreFab && elementTwo == earthPreFab)
            {
                GameObject comOne = Instantiate(naturePreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 9;
            }

            //Steam
            if (elementThree == waterPreFab && elementTwo == firePreFab)
            {
                GameObject comOne = Instantiate(steamPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 10;
            }

            //Lava
            if (elementThree == earthPreFab && elementTwo == firePreFab)
            {
                GameObject comOne = Instantiate(lavaPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 11;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse2))
        {
            //Ice
            if (elementOne == airPreFab && elementThree == waterPreFab)
            {
                GameObject comOne = Instantiate(icePreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 6;
            }

            //Dust
            if (elementOne == airPreFab && elementThree == earthPreFab)
            {
                GameObject comOne = Instantiate(dustPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 7;
            }

            //Inferno
            if (elementOne == airPreFab && elementThree == firePreFab)
            {
                GameObject comOne = Instantiate(infernoPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 8;
            }

            //Nature
            if (elementOne == waterPreFab && elementThree == earthPreFab)
            {
                GameObject comOne = Instantiate(naturePreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 9;
            }

            //Steam
            if (elementOne == waterPreFab && elementThree == firePreFab)
            {
                GameObject comOne = Instantiate(steamPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 10;
            }

            //Lava
            if (elementOne == earthPreFab && elementThree == firePreFab)
            {
                GameObject comOne = Instantiate(lavaPreFab, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                comOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                comOne.GetComponent<ElementScript>().eType = 11;
            }
        }


        // To select the first ability
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            elementOne = airPreFab;
            UI.GetComponent<UIScript>().updateUI();

        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            elementOne = waterPreFab;
            UI.GetComponent<UIScript>().updateUI();
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            elementOne = earthPreFab;
            UI.GetComponent<UIScript>().updateUI();
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            elementOne = firePreFab;
            UI.GetComponent<UIScript>().updateUI();
        }


        //To select the second ability
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            elementTwo = airPreFab;

            UI.GetComponent<UIScript>().updateUI();
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            elementTwo = waterPreFab;
            UI.GetComponent<UIScript>().updateUI();
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            elementTwo = earthPreFab;
            UI.GetComponent<UIScript>().updateUI();
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            elementTwo = firePreFab;
            UI.GetComponent<UIScript>().updateUI();
        }



        //To select the third ability
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            elementThree = airPreFab;
            UI.GetComponent<UIScript>().updateUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            elementThree = waterPreFab;
            UI.GetComponent<UIScript>().updateUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            elementThree = earthPreFab;
            UI.GetComponent<UIScript>().updateUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            elementThree = firePreFab;
            UI.GetComponent<UIScript>().updateUI();
        }
    }

}
