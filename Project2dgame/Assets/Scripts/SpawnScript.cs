using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    private GameObject enemyPrefab;
    private int timer;
    public int spawnTime;


	// Use this for initialization
	void Start () {
        enemyPrefab = (GameObject)Resources.Load("FireEnemy");

    }
	
	// Update is called once per frame
	void Update () {

        timer++;

       

        if(timer >= spawnTime)
        {
            GameObject instEnemy = Instantiate(enemyPrefab, this.transform.position, this.transform.rotation) as GameObject;
            timer = 0;
        }
		
	}
}
