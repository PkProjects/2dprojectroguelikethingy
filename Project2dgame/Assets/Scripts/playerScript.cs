using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{

    private Rigidbody2D rigid;
    public int moveSpeed = 10;
    public int health = 100;
    public Text healthText;
    public float speedCap = 6;

    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(Vector2.up * moveSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(Vector2.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(-Vector2.right * moveSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rigid.AddForce(-Vector2.up * moveSpeed);
        }


        // When keys are released resets the direction's velocity
        if (Input.GetKeyUp(KeyCode.W))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
        }


        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            rigid.velocity = new Vector2(0, rigid.velocity.y);
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.S))
        {
            rigid.velocity = new Vector2(rigid.velocity.x, 0);
        }

        if (rigid.velocity.x >= speedCap)
        {
            rigid.velocity = new Vector2(speedCap, rigid.velocity.y);
        }

        if (rigid.velocity.x <= -speedCap)
        {
            rigid.velocity = new Vector2(-speedCap, rigid.velocity.y);
        }

        if (rigid.velocity.y >= speedCap)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, speedCap);
        }

        if (rigid.velocity.y <= -speedCap)
        {
            rigid.velocity = new Vector2(rigid.velocity.x, -speedCap);
        }

        healthText.text = health.ToString();
        
    }
}
