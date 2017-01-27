using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPrisoner : MonoBehaviour
{

    private GameObject enemyPrefab;
    private int timer;
    public int spawnTime;
    private int enemyType;

    
    //2 = Air Prisoner
    //3 = Water Prisoner
    //4 = Earth Prisoner
    //5 = Fire Prisoner


    // Use this for initialization
    void Start()
    {

        enemyType = Random.Range(2, 5);
        
        if (enemyType == 2)
        {
            enemyPrefab = (GameObject)Resources.Load("AP");
        }

        if (enemyType == 3)
        {
            enemyPrefab = (GameObject)Resources.Load("WP");
        }

        if (enemyType == 4)
        {
            enemyPrefab = (GameObject)Resources.Load("EP");
        }

        if (enemyType == 5)
        {
            enemyPrefab = (GameObject)Resources.Load("FP");
        }


        GameObject instEnemy = Instantiate(enemyPrefab, this.transform.position, this.transform.rotation) as GameObject;
        instEnemy.GetComponent<PrisonerScript>().type = enemyType;

    }
}
