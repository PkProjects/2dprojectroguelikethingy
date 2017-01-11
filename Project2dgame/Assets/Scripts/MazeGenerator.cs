using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Maze generator.
/// Generates a 'simply connected maze'.
/// Contrary to most examples, walls are not
/// </summary>
public class MazeGenerator : MonoBehaviour 
{

	public GameObject floorTile;
	public GameObject wallTile;

	// The definitions of the wall
	const int WALL = 1;
	const int UNVISITED = -1;
	const int FLOOR = 0;

	//Level size
	[SerializeField]
	int width = 100;
	[SerializeField]
	int height = 100;

	//Maze related stuff
	int[,] maze;
	int[,] region;
	int currentRegion = 0;
	int[] regionConnected;
	Vector2 currentPos = new Vector2(0,0);
	Vector2 nextPos = new Vector2(0,0);
	Stack<Vector2> freePositions; 
	Stack<Vector2> deadEnds; 
	[SerializeField]
	int randomness = 4; //Between 1 and 4
	[SerializeField]
	bool activateRemoveDeadEnds = true;

	//Room settings
	[SerializeField]
	int minRoomSize = 3;
	[SerializeField]
	int maxRoomSize = 8;
	[SerializeField]
	int amountOfRooms = 5;


	/// <summary>
	/// Start this generation of the maze.
	/// </summary>
	void Start () 
	{
		initMaze();
		placeRooms ();
		buildPath ();
		connectRegions ();
		if (activateRemoveDeadEnds) {
			removeDeadEnds ();
		}
		visualise ();
	}

	/// <summary>
	/// Generates the empty maze. 
	/// The outer bounds are made to be walls, everything else 'unvisited'
	/// Sets the region for every tile to 0
	/// </summary>
	void initMaze()
	{
		maze = new int[width,height];
		region = new int[width, height];
		freePositions = new Stack<Vector2>( width * height);
		deadEnds = new Stack<Vector2>( width * height);
		regionConnected = new int[amountOfRooms+1];

		for (int row = 0; row < height; row++)
		{
			for (int col = 0; col < width; col++)
			{
				if (row == 0 || row == height - 1 || col == 0 || col == width - 1) {
					maze [col, row] = WALL;
				} else {
					maze [col, row] = UNVISITED;
				}
				region [col, row] = currentRegion;
			}
		}
	}

	//Checks if there are 3 walls around position, if so, it's a dead end so fill it up
	void removeDeadEnds()
	{
		while (!deadEnds.IsEmpty ()) {
			var deadEnd = deadEnds.Pop ();
			bool canContinue = true;
			while (canContinue) {
				int wallCount = 0;
				int currentX = (int)deadEnd.x;
				int currentY = (int)deadEnd.y;
				if (maze [currentX + 1, currentY] == WALL) { 
					wallCount++;
				} else {
					nextPos = new Vector2 (currentX + 1, currentY);
				}
				if (maze [currentX - 1, currentY] == WALL) { 
					wallCount++;
				} else {
					nextPos = new Vector2 (currentX - 1, currentY);
				}
				if (maze [currentX, currentY + 1] == WALL) { 
					wallCount++;
				} else {
					nextPos = new Vector2 (currentX, currentY + 1);
				}
				if (maze [currentX, currentY - 1] == WALL) { 
					wallCount++;
				} else {
					nextPos = new Vector2 (currentX, currentY - 1);
				}
				if (wallCount >= 3) {
					maze [currentX, currentY] = WALL;				
					if (deadEnd != nextPos) {
						deadEnd = nextPos;
					} else {
						canContinue = false;
					}
				} else {
					canContinue = false;
				}
			}
		}
	}

	// Checks the grid for walls with 2 floor tiles next to them, then checks if the floors belong to different regions and if one of those regions is unconnected
	// If so, it'll turn the wall into a floor and set both regions as connected
	// This will also turn any unvisited tile into a wall tile
	void connectRegions()
	{
		for (int x = 1; x < height - 1; x++)
		{
			for (int y = 1; y < width - 1; y++)
			{
				if (maze [x, y] == WALL ) {
					if (maze [x + 1, y] == FLOOR && maze [x - 1, y] == FLOOR) {
						if (region [x + 1, y] != region [x - 1, y]) {
							if (regionConnected [region [x + 1, y]] != 1 || regionConnected [region [x - 1, y]] != 1) {
								//Debug.Log ("Made floor at x: " + x + " y: " + y);
								maze [x, y] = FLOOR;
								regionConnected [region [x + 1, y]] = 1;
								regionConnected [region [x - 1, y]] = 1;
							}
						}
					} else if (maze [x, y + 1] == FLOOR && maze [x, y - 1] == FLOOR) {
						if (region [x, y + 1] != region [x, y - 1]) {
							if (regionConnected [region [x, y + 1]] != 1 || regionConnected [region [x, y - 1]] != 1) {
								maze [x, y] = FLOOR;
								regionConnected [region [x, y + 1]] = 1;
								regionConnected [region [x, y - 1]] = 1;
							}
						}
					}
				} else if (maze [x, y] == UNVISITED) {
					placePath (x, y);
					//maze [x, y] = WALL;
				}
			}
		}
	}

	void buildPath()
	{
		while (currentPos == new Vector2 (0, 0)) {
			int x = Mathf.RoundToInt (Random.Range (0, width));
			int y = Mathf.RoundToInt (Random.Range (0, height));
			if (maze [x, y] == UNVISITED) {
				currentPos = new Vector2 (x, y);
			}
		}
		int currentX = (int)currentPos.x;
		int currentY = (int)currentPos.y;
		placePath (currentX, currentY);
		while (!freePositions.IsEmpty()) {	
			currentPos = freePositions.Pop ();

			currentX = (int)currentPos.x;
			currentY = (int)currentPos.y;
			placePath (currentX, currentY);
		}
	}

	void placePath(int currentX, int currentY)
	{
		while (maze [currentX, currentY] == UNVISITED) {
			maze [currentX, currentY] = FLOOR;
			List<Vector2> neighbours = new List<Vector2> ();
			if (maze [currentX + 1, currentY] == UNVISITED) {
				neighbours.Add (new Vector2 (currentX + 1, currentY));
			}
			if (maze [currentX - 1, currentY] == UNVISITED) {
				neighbours.Add (new Vector2 (currentX - 1, currentY));
			}
			if (maze [currentX, currentY + 1] == UNVISITED) {
				neighbours.Add (new Vector2 (currentX, currentY + 1));
			}
			if (maze [currentX, currentY - 1] == UNVISITED) {
				neighbours.Add (new Vector2 (currentX, currentY - 1));
			}
			if (neighbours.Count > 0) {
				var random = Mathf.Min (neighbours.Count, randomness);
				int direction = Mathf.RoundToInt (Random.Range (0, random));
				nextPos = neighbours [direction];
				if (nextPos - currentPos == new Vector2 (1, 0) || nextPos - currentPos == new Vector2 (-1, 0)) {
					if (maze [currentX, currentY + 1] == UNVISITED) {
						maze [currentX, currentY + 1] = WALL;
						neighbours.Remove (new Vector2 (currentX, currentY + 1));
					}
					if (maze [currentX, currentY - 1] == UNVISITED) {
						maze [currentX, currentY - 1] = WALL;
						neighbours.Remove (new Vector2 (currentX, currentY - 1));
					}
				} else if (nextPos - currentPos == new Vector2 (0, 1) || nextPos - currentPos == new Vector2 (0, -1)) {
					if (maze [currentX + 1, currentY] == UNVISITED) {
						maze [currentX + 1, currentY] = WALL;
						neighbours.Remove (new Vector2 (currentX +1, currentY));
					}
					if (maze [currentX - 1, currentY] == UNVISITED) {
						maze [currentX - 1, currentY] = WALL;
						neighbours.Remove (new Vector2 (currentX -1, currentY));
					}
				}
				foreach (Vector2 neighbour in neighbours) {
					freePositions.Push (neighbour);
				}
				//neighbours.Clear ();
				currentPos = nextPos;
				currentX = (int)currentPos.x;
				currentY = (int)currentPos.y;
			}
		}
		deadEnds.Push (currentPos);
	}

	void placeRooms()
	{
		for (int room = 0; room < amountOfRooms; room++) {
			int roomWidth = Mathf.RoundToInt (Random.Range (minRoomSize, maxRoomSize));
			int roomHeight = Mathf.RoundToInt (Random.Range (minRoomSize, maxRoomSize));
			int x = Mathf.RoundToInt (Random.Range (0, width - roomWidth));
			int y = Mathf.RoundToInt (Random.Range (0, height - roomHeight));

			currentRegion++;
			for (int i = x; i < x + roomWidth; i++) {
				for (int j = y; j < y + roomHeight; j++) {
					if (i == x || i == x + roomWidth - 1 || j == y || j == y + roomHeight - 1) {
						if (region [i, j] == 0) {
							maze [i, j] = WALL;
							region [i, j] = currentRegion;
						}
					} else {
						maze [i, j] = FLOOR;
						region [i, j] = currentRegion;
					}
				}
			}
		}
	}

	void visualise()
	{
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				if (maze [x, y] == FLOOR) {
					var tempFloor = Instantiate (floorTile, new Vector2(x,y), Quaternion.identity);
				} else if (maze [x, y] == WALL) {
					var tempWall = Instantiate (wallTile, new Vector2(x,y), Quaternion.identity);
				}
			}
		}
	}
}
