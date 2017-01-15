using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class playerSkills : MonoBehaviour {

	public int skillOne = 1;
	public int skillTwo = 2;

    public float elementSpeed = 2f;

    public GameObject elementOne = null;
    public GameObject elementTwo = null;
    

    public GameObject shootLocOne;
    public GameObject shootLocTwo;

    public GameObject airPreFab;
    public GameObject waterPreFab;
    public GameObject earthPreFab;
    public GameObject firePreFab;
    public GameObject uSPreFab;
    
    

	// Use this for initialization
	void Start () {

        elementOne = uSPreFab;
        elementTwo = uSPreFab;
	
	}
	
	// Update is called once per frame
	void Update () {


		if (!Input.GetKey(KeyCode.LeftShift) &&  Input.GetKeyDown (KeyCode.Mouse0)) {
            if (elementOne != uSPreFab)
            {
                GameObject eleOne = Instantiate(elementOne, shootLocOne.transform.position, gameObject.transform.rotation) as GameObject;
                eleOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                Debug.Log(new Vector3(0, -1f * elementSpeed, 0));

                if (elementOne == airPreFab)
                {
                    eleOne.GetComponent<ElementScript>().eType = 2;
                }else


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
		if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown (KeyCode.Mouse1)) {
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



        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Combination of First and second ability");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse1))
        {
            Debug.Log("Combination of second and third ability");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Mouse2))
        {
            Debug.Log("Combination of first and third ability");
        }
        





        // To select the first ability
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            elementOne = airPreFab;
            Debug.Log("First Air");
        }

        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            elementOne = waterPreFab;
            Debug.Log("First Water");
        }

        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            elementOne = earthPreFab;
            Debug.Log("First Earth");
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            elementOne = firePreFab;
            Debug.Log("First Fire");
        }






        //To select the second ability
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            elementTwo = airPreFab;
            Debug.Log("Second Air");
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            elementTwo = waterPreFab;
            Debug.Log("Second Air");
        }

        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            elementTwo = earthPreFab;
            Debug.Log("Second Air");
        }

        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            elementTwo = firePreFab;
            Debug.Log("Second Air");
        }
    }
    
}
