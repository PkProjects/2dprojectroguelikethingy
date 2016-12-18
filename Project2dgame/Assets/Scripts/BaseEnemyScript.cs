using UnityEngine;
using System.Collections;
using System;

public class BaseEnemyScript : MonoBehaviour
{

    public GameObject player;
    public int activationRadius = 3;
    public bool patrol = true;
    public bool readyShot = false;
    public int walkRange = 10;
    private int steps = 0;
    private SpriteRenderer sprite;
    private int updateCount = 0;
    private int caseNr;
    private Rigidbody2D rb;
    private Vector3 direction;
    private GameObject shootLoc;
    private bool switchDirection;

    [Range(0.01f, 0.1f)]
    public float speed = 0.05f;
    [Range(0f, 10f)]
    public float bulletSpeed = 2f;
    public GameObject bullet;
    private Quaternion rotDir;

    public float rayRange = 20;
    public RaycastHit2D hit;
    public float distance;


    // Use this for initialization
    void Start()
    {

        sprite = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        shootLoc = this.gameObject.transform.GetChild(0).gameObject;


    }



    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log(caseNr);
        Vector3 forward = transform.TransformDirection(Vector3.down) * 10;

        hit = Physics2D.Raycast(shootLoc.transform.position, forward, rayRange);
        Debug.DrawRay(shootLoc.transform.position, forward, Color.green);
        Debug.Log(hit.collider);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "wall")
            {
                Debug.Log(hit.collider.tag);
                changeDirection();
            }
        }



        Vector3 difference = gameObject.transform.position - player.transform.position;

        if (difference.magnitude < activationRadius)
        {
            Vector3 dir = gameObject.transform.position - player.transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            sprite.flipY = false;
            patrol = false;

            updateCount++;

            if (updateCount >= 100)
            {
                updateCount = 0;
                readyShot = true;
            }

            if (readyShot == true)
            {

                GameObject instBullet = Instantiate(bullet, shootLoc.transform.position, gameObject.transform.rotation) as GameObject;
                instBullet.GetComponent<ProjectileScript>().velocity = new Vector3(0, -1f * bulletSpeed, 0);
                readyShot = false;
            }



        }

        else
        {
            patrol = true;
        }

        if (patrol == true)
        {

            switch (caseNr)
            {
                case 0: //Walk up
                    gameObject.transform.Translate(new Vector2(0, -1) * speed);
                    sprite.transform.rotation = (Quaternion.AngleAxis(180, Vector3.forward));
                    break;


                case 1: //Walk down
                    gameObject.transform.Translate(new Vector2(0, -1) * speed);
                    sprite.transform.rotation = (Quaternion.AngleAxis(0, Vector3.forward));
                    break;



                case 2: //Walk up left
                    gameObject.transform.Translate(new Vector2(0, -1) * speed);
                    sprite.transform.rotation = (Quaternion.AngleAxis(225, Vector3.forward));
                    break;

                case 3: //Walk up right
                    gameObject.transform.Translate(new Vector2(0, -1) * speed);
                    sprite.transform.rotation = (Quaternion.AngleAxis(135, Vector3.forward));
                    break;


                case 4: //Walk down right
                    gameObject.transform.Translate(new Vector2(0, -1) * speed);
                    sprite.transform.rotation = (Quaternion.AngleAxis(45, Vector3.forward));
                    break;

                case 5: //Walk down left
                    gameObject.transform.Translate(new Vector2(0, -1) * speed);
                    sprite.transform.rotation = (Quaternion.AngleAxis(315, Vector3.forward));
                    break;

                case 6: //Walk right
                    gameObject.transform.Translate(new Vector2(0, -1) * speed);
                    sprite.transform.rotation = (Quaternion.AngleAxis(90, Vector3.forward));
                    break;

                case 7: //Walk left
                    gameObject.transform.Translate(new Vector2(0, -1) * speed);
                    sprite.transform.rotation = (Quaternion.AngleAxis(270, Vector3.forward));
                    break;

            }

           

            if (steps < walkRange)
            {
                steps++;

            }
            else
            {
                SwitchCase();
                steps = 0;
            }
        }

    }

    void SwitchCase()
    {
        caseNr = UnityEngine.Random.Range(0, 8);
        walkRange = UnityEngine.Random.Range(100, 200);

    }

    void changeDirection()
    {
        Debug.Log(caseNr);
      



        if (caseNr == 0 || caseNr == 2 || caseNr == 3)
        {
            Debug.Log("Should go down");
            caseNr = 1;
        }

        if (caseNr == 1 || caseNr == 4 || caseNr == 5)
        {
            Debug.Log("Should go up");
            caseNr = 0;
        }

        if (caseNr == 6)
        {

            Debug.Log("Should go left");
            caseNr = 7;
        } 

       if (caseNr == 7)
       {
           
           Debug.Log("Should go right");
           caseNr = 6;
        }

    }
}
                  
           

   


