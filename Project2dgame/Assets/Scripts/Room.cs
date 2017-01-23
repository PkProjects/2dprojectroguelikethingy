using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room {

	Vector2 startPoint;
	//Vector2 endPoint;
	int roomWidth;
	int roomHeight;
	int roomNumber;

	public Room()
	{
		//Empty for reasons
	}
	public Room(Vector2 start, int width, int height, int number)
	{
		startPoint = start;
		roomWidth = width;
		roomHeight = height;
		roomNumber = number;
	}
}
