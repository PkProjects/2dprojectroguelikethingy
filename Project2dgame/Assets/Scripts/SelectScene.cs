using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScene : MonoBehaviour {

    public GameObject startButton;
    public GameObject continueButton;

    public static int element;
	// Use this for initialization
	void Start () {

        continueButton.gameObject.SetActive(false);		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //Select ability
    public void selectAbility()
    {
        startButton.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(true);
        Debug.Log("Select ability");
    }

    //Select scene
    public void SceneSelect(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void saveElement(int e)
    {
        element = e;
    }

    //Quit game
    public void quitGame()
    {
        Application.Quit();
    }
}
