﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    private GameObject enemyPrefab;
    private int timer;
    public int spawnTime;
    private int enemyType;
	MazeGenerator mazeGen;
    
    //1 = Normal Guard
    //2 = Air Guard
    //3 = Water Guard
    //4 = Earth Guard
    //5 = Fire Guard


	// Use this for initialization
	void Start () {

		mazeGen = GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<MazeGenerator> ();
		GameObject maze = GameObject.FindGameObjectWithTag ("levelHolder");
        enemyType = Random.Range(1,5);

        if (enemyType == 1)
        {
            enemyPrefab = (GameObject)Resources.Load("NG");
        }

        if (enemyType == 2)
        {
            enemyPrefab = (GameObject)Resources.Load("AG");
        }

        if (enemyType == 3)
        {
            enemyPrefab = (GameObject)Resources.Load("WG");
        }

        if (enemyType == 4)
        {
            enemyPrefab = (GameObject)Resources.Load("EG");
        }

        if (enemyType == 5)
        {
            enemyPrefab = (GameObject)Resources.Load("FG");
        }


		GameObject instEnemy = Instantiate(enemyPrefab, this.transform.position, this.transform.rotation) as GameObject;
		instEnemy.GetComponent<BaseEnemyScript>().type = enemyType;
		instEnemy.transform.parent = maze.transform;
		instEnemy.GetComponent<BaseEnemyScript>().currentLevel = mazeGen.getCurrentLevel();



    }
	
}
