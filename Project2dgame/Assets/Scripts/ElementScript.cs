using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ElementScript : MonoBehaviour
{

    public Vector3 velocity = new Vector3(0f, -2f, 0f);
    public GameObject player;

    private Vector3 origin;
    private float distance;
    public int eType;

    // Use this for initialization
    void Start()
    {

        origin = this.transform.position;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Debug.Log("collision");
        Destroy(gameObject);
        Debug.Log(collision.collider.tag);

        if (collision.collider.tag == "enemy")
        {
            if (collision.collider.GetComponent<BaseEnemyScript>().type == eType)
            {
                collision.collider.GetComponent<BaseEnemyScript>().health -= 5;
            }

            else
            {
                collision.collider.GetComponent<BaseEnemyScript>().health -= 10;
                Destroy(this.gameObject);
            }
           
        }

        if (collision.collider.tag == "wall")
        {
            Destroy(this.gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {

        
        transform.Translate(velocity * Time.deltaTime * 2);
       
        distance = Vector3.Distance(origin, this.transform.position);

        if (distance >= 10)
        {
            Destroy(this.gameObject);
        }

    }
}