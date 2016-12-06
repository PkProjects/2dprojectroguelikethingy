using UnityEngine;
using System.Collections;

public class playerSkills : MonoBehaviour {

	public int skillOne = 1;
	public int skillTwo = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)) {
			skillsList(skillOne);
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			skillsList(skillTwo);
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
