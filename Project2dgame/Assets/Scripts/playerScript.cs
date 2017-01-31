using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class playerScript : MonoBehaviour
{

    private Rigidbody2D rigid;
    public int moveSpeed = 10;
    public int health = 100;
	private int maxHealth;
    public Image health0;
    public Image health20;
    public Image health40;
    public Image health60;
    public Image health80;
    public float speedCap = 6;
	private int enemyKillCount = 29;
	public Text deathText;
	float oldTime;
	float newTime;
	bool respawn;
	bool alive = true;

    // Use this for initialization
    void Start()
    {
		maxHealth = health;
        rigid = GetComponent<Rigidbody2D>();
        updateHealth();
    }

	public void enemyKilled()
	{
		enemyKillCount++;
		Debug.Log (enemyKillCount);
	}

	public int getKillCount()
	{
		return enemyKillCount;
	}

	public void resetKills()
	{
		enemyKillCount = 0;
	}

    // Update is called once per frame
    void Update()
    {
		if (respawn) {
			if (Time.time - oldTime >= 2) {
				respawn = false;
				Debug.Log ("Test");
				MazeGenerator mazeGen = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MazeGenerator> ();
				mazeGen.startNextLevel (false);
				health = maxHealth; 
				updateHealth();
				deathText.enabled = false;
				alive = true;
			}
		}

		if (alive) {
			Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint (transform.position);
			float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis (angle - 90f, Vector3.forward);
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


			// When keys are released resets the direction's velocity
			if (Input.GetKeyUp (KeyCode.W)) {
				rigid.velocity = new Vector2 (rigid.velocity.x, 0);
			}

			if (Input.GetKeyUp (KeyCode.D)) {
				rigid.velocity = new Vector2 (0, rigid.velocity.y);
			}

			if (Input.GetKeyUp (KeyCode.A)) {
				rigid.velocity = new Vector2 (0, rigid.velocity.y);
			}

			if (Input.GetKeyUp (KeyCode.S)) {
				rigid.velocity = new Vector2 (rigid.velocity.x, 0);
			}


			if (Input.GetKey (KeyCode.A) && Input.GetKey (KeyCode.D)) {
				rigid.velocity = new Vector2 (0, rigid.velocity.y);
			}

			if (Input.GetKey (KeyCode.W) && Input.GetKey (KeyCode.S)) {
				rigid.velocity = new Vector2 (rigid.velocity.x, 0);
			}
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

       // healthText.text = health.ToString();
        
    }

    public void updateHealth()
    {
        if (health >80)
        {
            health0.color = Color.green;
            health20.color = Color.green;
            health40.color = Color.green;
            health60.color = Color.green;
            health80.color = Color.green;

        }else

        if (health >60 && health <=80)
        {
            health0.color = Color.green;
            health20.color = Color.green;
            health40.color = Color.green;
            health60.color = Color.green;
            health80.color = Color.red;
        }else

        if (health > 40 && health <= 60)
        {
            health0.color = Color.green;
            health20.color = Color.green;
            health40.color = Color.green;
            health60.color = Color.red;
            health80.color = Color.red;
        }else

        if (health > 20 && health <= 40)
        {
            health0.color = Color.green;
            health20.color = Color.green;
            health40.color = Color.red;
            health60.color = Color.red;
            health80.color = Color.red;
        }else

        if(health > 0 && health <=20)
        {
            health0.color = Color.green;
            health20.color = Color.red;
            health40.color = Color.red;
            health60.color = Color.red;
            health80.color = Color.red;
        }

        if (health <= 0 && alive)
        {
            health0.color = Color.red;
            Debug.Log("Respawn");
            //Add respawn code here;
			respawnPlayer();
        }
    }

	void respawnPlayer()
	{
		alive = false;
		oldTime = Time.time;
		resetKills ();
		deathText.enabled = true;
		respawn = true;
	}
}
