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
        this.gameObject.AddComponent<Rigidbody2D>();
        origin = this.transform.position;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

       
        if (collision.collider.tag == "wall")
        {
            Destroy(gameObject);
        }

        if (collision.collider.tag == "enemy")
        {
            Destroy(gameObject);

            if (collision.collider.GetComponent<BaseEnemyScript>().type == eType)
            {
                collision.collider.GetComponent<BaseEnemyScript>().health -= 5;
                Debug.Log(collision.collider.GetComponent<BaseEnemyScript>().health);
                collision.collider.GetComponent<BaseEnemyScript>().getPlayerDirection();
            }

            else
            {
                collision.collider.GetComponent<BaseEnemyScript>().health -= 10;
                Debug.Log(collision.collider.GetComponent<BaseEnemyScript>().health);
                collision.collider.GetComponent<BaseEnemyScript>().getPlayerDirection();

            }
        }

        if (collision.collider.tag == "prisoner")
        {
            Destroy(gameObject);
            if (collision.collider.GetComponent<PrisonerScript>().type == eType)
            {
                collision.collider.GetComponent<PrisonerScript>().health -= 5;
            }

            else
            {
                collision.collider.GetComponent<PrisonerScript>().health -= 10;

            }
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