using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	private Rigidbody2D rigid;
	public int moveSpeed = 10;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); 
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; 
		transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward); 
		if (Input.GetKey (KeyCode.W)) {
			rigid.AddForce (Vector2.up * moveSpeed);
		}
		if (Input.GetKey (KeyCode.D)) {
			rigid.AddForce (Vector2.right * moveSpeed);
		}
		if (Input.GetKey (KeyCode.A)) {
			rigid.AddForce (-Vector2.right * moveSpeed);
		}
		if (Input.GetKey (KeyCode.S)) {
			rigid.AddForce (-Vector2.up * moveSpeed);
		}
	}
}
