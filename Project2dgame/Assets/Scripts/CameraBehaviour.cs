﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public GameObject player;
    private Vector3 position;

	// Use this for initialization
	void Start () {

        
		
	}
	
	// Update is called once per frame
	void Update () {

       
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

    }
}
