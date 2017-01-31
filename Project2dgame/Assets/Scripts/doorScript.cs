using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorScript : MonoBehaviour {

	public GameObject floor;

	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "bullet") {
			GameObject maze = GameObject.FindGameObjectWithTag ("levelHolder");
			var x = gameObject.transform.position.x;
			var y = gameObject.transform.position.y;
			var tempFloor = Instantiate (floor, new Vector2 (x, y), Quaternion.identity);
			tempFloor.transform.parent = maze.transform;
			Destroy (gameObject);
		}
	}
}
