using UnityEngine;
using System.Collections;

public class playerSkills : MonoBehaviour {

	public int skillOne = 1;
	public int skillTwo = 2;

    public float elementSpeed = 2f;

    public GameObject elementOne;
    public GameObject elementTwo;

    public GameObject shootLocOne;
    public GameObject shootLocTwo;

    public GameObject airPreFab;
    public GameObject waterPreFab;
    public GameObject earthPreFab;
    public GameObject firePreFab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetKeyDown (KeyCode.Q)) {
            if (elementOne != null)
            {
                GameObject eleOne = Instantiate(elementOne, shootLocOne.transform.position, gameObject.transform.rotation) as GameObject;
                eleOne.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
                Debug.Log(new Vector3(0, -1f * elementSpeed, 0));
                
                
            }
		}
		if (Input.GetKeyDown (KeyCode.E)) {
            if (elementTwo != null)
            {
                GameObject eleTwo = Instantiate(elementTwo, shootLocTwo.transform.position, gameObject.transform.rotation) as GameObject;
                eleTwo.GetComponent<ElementScript>().velocity = new Vector3(0, 1f * elementSpeed, 0);
            }
		    
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

	void skillsList(int skill)
	{
		switch (skill) {
		case 1: Debug.Log("skillone");
			break;
		case 2: Debug.Log("twoooo");
			break;
		}
	}

}
