using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileScript : MonoBehaviour {

    public Vector3 velocity = new Vector3(1f,0f,0f);
    public GameObject player;
    
    private Vector3 origin;    
    private float distance;

	// Use this for initialization
	void Start () {

        origin = this.transform.position;
        this.gameObject.AddComponent<Rigidbody2D>();
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // Debug.Log("collision");
        Destroy(gameObject);


        if (collision.collider.tag == "player")
        {
            Destroy(gameObject);
            FindObjectOfType<playerScript>().health -= 10;
        }

       
    }

    // Update is called once per frame
    void Update () {

        transform.Translate(velocity * Time.deltaTime * 2);
        distance = Vector3.Distance(origin, this.transform.position);

        if (distance >= 10)
        {
            Destroy(this.gameObject);
        }
		
	}
}
