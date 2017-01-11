using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationScript : MonoBehaviour {

    private GameObject tilePathTB;
    private GameObject tilePathSW;
    private GameObject tilePathCR;

    public int pathTiles = 10;
    public int direction;
    private Vector3 placeLoc;

	// Use this for initialization
	void Start () {
    

		
	}
	
	// Update is called once per frame
	void Update () {

        for (int i = 0; i < pathTiles; i++)
        {
            direction = Random.Range(0, 4);
            Debug.Log(direction);
            if (direction == 0) // Direction up
            {

            }

            if (direction == 1) // Direction Right
            {

            }

            if (direction == 2) // Direct Down;
            {

            }

            if (direction == 3) // Direction left
            {

            }
        }
		
	}
}
