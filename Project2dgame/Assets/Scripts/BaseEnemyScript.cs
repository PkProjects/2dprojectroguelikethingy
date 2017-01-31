using UnityEngine;
using System.Collections;
using System;

public class BaseEnemyScript : MonoBehaviour
{
    public int health = 30;
    private GameObject player;
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

    private Vector3 shotDir;
    private float shotAngle;

    public Vector3 hitDirection;

    [Range(0.01f, 0.1f)]
    public float speed = 0.05f;
    [Range(0f, 10f)]
    public float bulletSpeed = 2f;
    private GameObject bullet;
    private Quaternion rotDir;

    private float rayRange =0.5f;
    private float longRayRange = 6;
    public RaycastHit2D hit;
    public RaycastHit2D longHit;
    public float distance;
    private float playerDistance;
    private bool followPlayer;
    public int type;
	private bool isActive = false;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        
        bullet = (GameObject)Resources.Load("Bullet");
       
        sprite = gameObject.GetComponent<SpriteRenderer>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        shootLoc = this.gameObject.transform.GetChild(0).gameObject;


    }

    
    void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "player") {
			isActive = true;
		}
	}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            isActive = false;
        }
    }



    // Update is called once per frame
    void FixedUpdate()
    {
		if (isActive) {
			Vector3 forward = transform.TransformDirection (Vector3.down) * 10;

        hit = Physics2D.Raycast(gameObject.transform.position, forward, rayRange);
       

        Debug.DrawRay(gameObject.transform.position, forward, Color.green);
        

			if (hit.collider != null) {
				if (hit.collider.tag == "wall" || hit.collider.tag == "enemy") {
                    if (followPlayer == false)
                    {
                        
                        SwitchCase();
                    }
				}
			}

			if (longHit.collider != null) {
				if (longHit.collider.tag == "player") {
					playerDistance = Vector3.Distance (this.transform.position, player.transform.position);
					
					if (playerDistance >= 1) {
						followPlayer = true;
						
					} else if (playerDistance < 1) {
						followPlayer = false;
						
					}
				}
			}

			Vector3 difference = gameObject.transform.position - player.transform.position;

			if (difference.magnitude < activationRadius) {
                longHit = Physics2D.Raycast(gameObject.transform.position, forward, longRayRange);
                
               
                if (longHit.collider != null)
                {
                    
                    if (longHit.collider.tag != "wall")
                    {
                        Vector3 dir = gameObject.transform.position - player.transform.position;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
                        sprite.flipY = false;

                       // if (longHit.collider.tag == "player")
                       // {
                            
                            patrol = false;


                            if (followPlayer == true)
                            {
                                this.transform.Translate(-dir * speed);
                            }

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
                        //}
                    }
                }

                if(longHit.collider == false)
                {
                    patrol = true;
                }
               
             

			} else {
				patrol = true;
			}

			if (patrol == true) {

				switch (caseNr) {
				case 0: //Walk up
					gameObject.transform.Translate (new Vector2 (0, -1) * speed);
					sprite.transform.rotation = (Quaternion.AngleAxis (180, Vector3.forward));
					break;


				case 1: //Walk down
					gameObject.transform.Translate (new Vector2 (0, -1) * speed);
					sprite.transform.rotation = (Quaternion.AngleAxis (0, Vector3.forward));
					break;



				case 2: //Walk up left
					gameObject.transform.Translate (new Vector2 (0, -1) * speed);
					sprite.transform.rotation = (Quaternion.AngleAxis (225, Vector3.forward));
                    
					break;

				case 3: //Walk up right
					gameObject.transform.Translate (new Vector2 (0, -1) * speed);
					sprite.transform.rotation = (Quaternion.AngleAxis (135, Vector3.forward));
                    
					break;


				case 4: //Walk down right
					gameObject.transform.Translate (new Vector2 (0, -1) * speed);
					sprite.transform.rotation = (Quaternion.AngleAxis (45, Vector3.forward));
                    
					break;

				case 5: //Walk down left
					gameObject.transform.Translate (new Vector2 (0, -1) * speed);
					sprite.transform.rotation = (Quaternion.AngleAxis (315, Vector3.forward));
                    
					break;

				case 6: //Walk right
					gameObject.transform.Translate (new Vector2 (0, -1) * speed);
					sprite.transform.rotation = (Quaternion.AngleAxis (90, Vector3.forward));
                    
					break;

				case 7: //Walk left
					gameObject.transform.Translate (new Vector2 (0, -1) * speed);
					sprite.transform.rotation = (Quaternion.AngleAxis (270, Vector3.forward));
                    
					break;

                case 8: // Investigate shot

                        gameObject.transform.Translate(new Vector2(0, -1) * speed);
                        sprite.transform.rotation = Quaternion.AngleAxis(shotAngle - 90f, Vector3.forward);
                        Debug.Log("going");
                        
                        break;
				}

           

				if (steps < walkRange) {
					steps++;

				} else {
					SwitchCase ();
					steps = 0;
				}
			}

			if (health <= 0) {
				Destroy (this.gameObject);
			}
		}

    }

    void SwitchCase()
    {
        caseNr = UnityEngine.Random.Range(0, 8);
        walkRange = UnityEngine.Random.Range(100, 200);

    }

    public void getPlayerDirection()
    {
        shotDir =    this.transform.position - player.transform.position;
        shotAngle = Mathf.Atan2(shotDir.y, shotDir.x) * Mathf.Rad2Deg;
       
        caseNr = 8;
        walkRange = UnityEngine.Random.Range(100, 200);

       

        Debug.Log(shotDir);
    }


   }

                  
           

   


