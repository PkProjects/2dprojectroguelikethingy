using UnityEngine;
using System.Collections;

public class BaseEnemyScript : MonoBehaviour {

	public GameObject player;
	public int activationRadius = 3;
	public bool patrol = true;
	public int walkRange = 10;
	private int steps = 0;
	private SpriteRenderer sprite;
	private int updateCount = 0;

	// Use this for initialization
	void Start () {
		sprite = gameObject.GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 difference = gameObject.transform.position - player.transform.position;
		if (difference.magnitude < activationRadius) {
			Vector3 dir = gameObject.transform.position - player.transform.position; 
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg; 
			transform.rotation = Quaternion.AngleAxis (angle - 90f, Vector3.forward); 
			sprite.flipY = false;
			patrol = false;
		} else {
			patrol = true;
		}
		if (patrol && updateCount % 2 == 0) {
			if (steps < walkRange) {
				gameObject.transform.Translate (Vector2.up * 0.1f);
				steps++;
				sprite.flipY = true;
			} else if (steps < 2*walkRange) {
				gameObject.transform.Translate (Vector2.down * 0.1f);
				steps++;
				sprite.flipY = false;
			} else {
				steps = 0;
			}
		}
		if (updateCount < 100) {
			updateCount++;
		} else {
			updateCount = 0;
		}
	}
}
