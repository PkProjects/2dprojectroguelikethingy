using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class elevatorDoor : MonoBehaviour {

	public Text centerText;
	bool showText;
	float oldTime;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "bullet") {
			GameObject player = GameObject.FindGameObjectWithTag ("player");
			centerText = GameObject.FindGameObjectWithTag("text").GetComponent<Text>();
			if (player.GetComponent<playerScript> ().getKillCount () >= 30) {
				MazeGenerator mazeGen = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MazeGenerator> ();
				player.GetComponent<playerScript> ().resetKills ();
				mazeGen.startNextLevel (true);
			} else {
				// Kill more enemies to go to the next level!
				centerText.text = "You need to kill " + (30 - player.GetComponent<playerScript>().getKillCount () ) + " more enemies";
				centerText.enabled = true;
				showText = true;
				oldTime = Time.time;
			}
		}
	}

	void Update()
	{
		if (showText) {
			if (Time.time - oldTime > 3) {
				centerText.enabled = false;
				showText = false;
			}
		}
	}
}
