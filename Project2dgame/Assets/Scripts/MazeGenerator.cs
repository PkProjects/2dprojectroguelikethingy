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
	//prefabs for visualisation of the grid
	public GameObject floorTile;
	public GameObject wallTile;
	public GameObject doorTile;
	public GameObject prisonDoor;
	public GameObject prisonWall;
	public GameObject enemyPrefab1;
	public GameObject player;
	public GameObject elevatorDoor;
	public GameObject elevatorFloor;

	// The definitions of the wall
	const int WALL = 1;
	const int UNVISITED = -1;
	const int FLOOR = 0;
	const int DOOR = 2;
	const int PRISONDOOR = 3;
	const int PRISONWALL = 4;
	const int ELEVATORFLOOR = 5;
	const int ELEVATORDOOR = 6;

	//Level size
	[SerializeField]
	int width = 100;
	[SerializeField]
	int height = 100;

	//Maze related stuff
	int[,] maze;
	int[,] region;
	int[,] enemies;
	int currentRegion;
	int elevatorRegion;
	int[] regionConnected; // 0 is unconnected, 1 is connected
	Vector2 currentPos; 
	Vector2 nextPos;
	Vector2 previousPos;
	Stack<Vector2> freePositions; 
	Stack<Vector2> deadEnds; 
	[SerializeField]
	int randomness = 4; //Between 1 and 4
	[SerializeField]
	bool activateRemoveDeadEnds = true;
	[SerializeField]
	bool loopUntillConnected = true;

	//Room settings
	[SerializeField]
	int minRoomSize = 3;
	[SerializeField]
	int maxRoomSize = 8;
	[SerializeField]
	int amountOfRooms = 40;
	[SerializeField]
	int amountOfCells = 10;
	List<Room> rooms = new List<Room>();
	[SerializeField]
	int enemiesPerRoom = 2;

	//Tracks which floor the player is currently on
	int currentFloor = 0;

	/// <summary>
	/// Start this generation of the maze.
	/// </summary>
	void Start () 
	{
		buildDungeon ();
		visualise ();
	}

	// this exists just for ease of use
	void buildDungeon()
	{
		initMaze();
		placeRooms ();
		buildPath (0);
        connectRegions ();
		if (activateRemoveDeadEnds) {
			removeDeadEnds ();
		}
		if (loopUntillConnected) {
			checkConnections ();
		}
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
		enemies = new int[width, height];
		freePositions = new Stack<Vector2>( width * height);
		deadEnds = new Stack<Vector2>( width * height);
		currentRegion = 0;
		elevatorRegion = amountOfRooms + amountOfCells + 1;
		currentPos = previousPos = nextPos = new Vector2(0,0);
		regionConnected = new int[amountOfRooms+amountOfCells+3];

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
				enemies [col, row] = 0;
			}
		}
	}

	void checkConnections()
	{
		//Debug.Log ("Rebuilding level");
		for (int i = 0; i < regionConnected.Length - 1; i++) {
			if (regionConnected [i] == 0) {
				buildDungeon ();
				//Debug.Log("Path placed");
				//buildPath(amountOfRooms + 2);
				//connectToConnectedRegion ();
				//connectRegions();
				break;
			}
		}
	}

	void connectToConnectedRegion()
	{
		for (int x = 1; x < height - 1; x++) {
			for (int y = 1; y < width - 1; y++) {
				if (maze [x, y] == WALL && region [x, y] == (amountOfRooms+2)) {
					if (maze [x + 1, y] == FLOOR && maze [x - 1, y] == FLOOR) {
						if (region [x + 1, y] != region [x - 1, y]) {
							if (regionConnected [region [x + 1, y]] == 1 || regionConnected [region [x - 1, y]] == 1) {
								//Debug.Log ("Made floor at x: " + x + " y: " + y);
								maze [x, y] = DOOR;
								regionConnected [region [x + 1, y]] = 1;
								regionConnected [region [x - 1, y]] = 1;
							} else {
								var random = Random.Range (0, 10);
								if (random < 1) {
									maze [x, y] = DOOR;
								}
							}
						}
					} else if (maze [x, y + 1] == FLOOR && maze [x, y - 1] == FLOOR) {
						if (region [x, y + 1] != region [x, y - 1]) {
							if (regionConnected [region [x, y + 1]] == 1 || regionConnected [region [x, y - 1]] == 1) {
								maze [x, y] = DOOR;
								regionConnected [region [x, y + 1]] = 1;
								regionConnected [region [x, y - 1]] = 1;
							}
						}
					}
				} else if (maze [x, y] == UNVISITED) {
					//placePath (x, y);
					maze [x, y] = WALL;
				}
			}
		}
	}

	// Get dead end from a stack, and checks if there are 3 walls around position, if so, it's a dead end so fill it up
	void removeDeadEnds()
	{
		while (!deadEnds.IsEmpty ()) {
			var deadEnd = deadEnds.Pop ();
			bool canContinue = true;
			while (canContinue) {
				int wallCount = 0;
				int currentX = (int)deadEnd.x;
				int currentY = (int)deadEnd.y;
				if (maze [currentX + 1, currentY] == WALL || maze [currentX + 1, currentY] == PRISONWALL) { 
					wallCount++;
				} else {
					nextPos = new Vector2 (currentX + 1, currentY);
				}
				if (maze [currentX - 1, currentY] == WALL || maze [currentX - 1, currentY] == PRISONWALL ) { 
					wallCount++;
				} else {
					nextPos = new Vector2 (currentX - 1, currentY);
				}
				if (maze [currentX, currentY + 1] == WALL || maze [currentX, currentY + 1] == PRISONWALL) { 
					wallCount++;
				} else {
					nextPos = new Vector2 (currentX, currentY + 1);
				}
				if (maze [currentX, currentY - 1] == WALL || maze [currentX, currentY - 1] == PRISONWALL) { 
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
	// If unconnected, it'll turn the wall into a floor and set both regions as connected, if connected there's a chance to carve a door anyways
	// This will also turn any unvisited tile into a wall tile
	void connectRegions()
	{
		elevatorRegion = amountOfRooms + amountOfCells + 1;
		for (int x = 1; x < height - 1; x++)
		{
			for (int y = 1; y < width - 1; y++)
			{
				if (maze [x, y] == WALL || maze [x, y] == PRISONWALL ) {
					if ( (maze [x + 1, y] == FLOOR || maze [x + 1, y] == ELEVATORFLOOR) && (maze [x - 1, y] == FLOOR || maze [x - 1, y] == ELEVATORFLOOR)) {
						if (region [x + 1, y] != region [x - 1, y]) {
							if (regionConnected [region [x + 1, y]] != 1 || regionConnected [region [x - 1, y]] != 1) {
								//Debug.Log ("Made floor at x: " + x + " y: " + y);
								if ( (elevatorRegion > region [x + 1, y] && region [x + 1, y] >= amountOfRooms) || ( elevatorRegion > region [x - 1, y] && region [x - 1, y] >= amountOfRooms)) {
									maze [x, y] = PRISONDOOR;
								} else if (region [x + 1, y] >= elevatorRegion || region [x - 1, y] >= elevatorRegion) {
									maze [x, y] = ELEVATORDOOR;
								} else {
									maze [x, y] = DOOR;
								}
								regionConnected [region [x + 1, y]] = 1;
								regionConnected [region [x - 1, y]] = 1;
							} else {
								var random = Random.Range (0, 10);
								if (random < 1) {
									if ( (elevatorRegion > region [x + 1, y] && region [x + 1, y] >= amountOfRooms) || (elevatorRegion > region [x - 1, y] && region [x - 1, y] >= amountOfRooms)) {
										maze [x, y] = PRISONDOOR;
									} else if (region [x + 1, y] >= elevatorRegion || region [x - 1, y] >= elevatorRegion) {
										maze [x, y] = ELEVATORDOOR;
									} else {
										maze [x, y] = DOOR;
									}
								}
							}
						}
					} else if ( (maze [x, y + 1] == FLOOR || maze [x, y + 1] == ELEVATORFLOOR) && (maze [x, y - 1] == FLOOR || maze [x, y - 1] == ELEVATORFLOOR )) {
						if (region [x, y + 1] != region [x, y - 1]) {
							if (regionConnected [region [x, y + 1]] != 1 || regionConnected [region [x, y - 1]] != 1) {
								if ( (elevatorRegion > region [x, y+1] && region [x, y+1] >= amountOfRooms) || (elevatorRegion > region [x, y - 1] && region [x, y - 1] >= amountOfRooms)) {
									maze [x, y] = PRISONDOOR;
								} else if (region [x, y+1] >= elevatorRegion || region [x, y - 1] >= elevatorRegion) {
									maze [x, y] = ELEVATORDOOR;
								} else {
									maze [x, y] = DOOR;
								}
								regionConnected [region [x, y + 1]] = 1;
								regionConnected [region [x, y - 1]] = 1;
							}
						}
					}
				} else if (maze [x, y] == UNVISITED) {
					//placePath (x, y);
					maze [x, y] = WALL;
				}
			}
		}
	}

	void buildPath(int pathRegion)
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
		freePositions.Push (currentPos);
		placePath (currentX, currentY, pathRegion);
		while (!freePositions.IsEmpty()) {	
			currentPos = freePositions.Pop ();

			currentX = (int)currentPos.x;
			currentY = (int)currentPos.y;
			placePath (currentX, currentY, pathRegion);
		}
	}

	void placePath(int currentX, int currentY, int pathRegion)
	{

		while (maze [currentX, currentY] == UNVISITED) {
			int exits = 0;
			if (maze [currentX + 1, currentY] == FLOOR) {
				exits++;
			}
			if (maze [currentX - 1, currentY] == FLOOR) {
				exits++;
			}
			if (maze [currentX, currentY + 1] == FLOOR) {
				exits++;
			}
			if (maze [currentX, currentY - 1] == FLOOR) {
				exits++;
			}
			if (exits < 2) {
				maze [currentX, currentY] = FLOOR;
				region [currentX, currentY] = pathRegion;
			} else {
				maze [currentX, currentY] = WALL;
				region [currentX, currentY] = pathRegion;
				deadEnds.Push (previousPos);
				break;
			}
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
				previousPos = currentPos;
				currentPos = nextPos;
				currentX = (int)currentPos.x;
				currentY = (int)currentPos.y;
			}
		}
		deadEnds.Push (currentPos);
	}

	// Randomly places rooms, and spawns enemies in them. If the new room overlaps an old one, set its region to connected
	void placeRooms()
	{
		for (int room = 0; room < amountOfRooms; room++) {
			int roomWidth = Mathf.RoundToInt (Random.Range (minRoomSize, maxRoomSize));
			int roomHeight = Mathf.RoundToInt (Random.Range (minRoomSize, maxRoomSize));
			int x = Mathf.RoundToInt (Random.Range (0, width - roomWidth));
			int y = Mathf.RoundToInt (Random.Range (0, height - roomHeight));

			currentRegion++;
			Room tempRoom = new Room (new Vector2 (x, y), roomWidth, roomHeight, currentRegion);
			rooms.Add (tempRoom);
			int enemiesPlaced = 0;
			for (int i = x; i < x + roomWidth; i++) {
				for (int j = y; j < y + roomHeight; j++) {
					if (i == x || i == x + roomWidth - 1 || j == y || j == y + roomHeight - 1) { //in first and last row and column place wall, unless it's already a region (which means there is a room overlap)
						if (region [i, j] == 0) {
							maze [i, j] = WALL;
							region [i, j] = currentRegion;
						} else {
							if (maze [i, j] == FLOOR) {
								regionConnected [currentRegion] = 1;
							}
						}
					} else {
						maze [i, j] = FLOOR;
						region [i, j] = currentRegion;
						if (enemiesPlaced < enemiesPerRoom) {
							float spawnChance = Random.Range (0, 10);
							if (spawnChance < 0.5) {
								enemies [i, j] = 1;
								enemiesPlaced++;
							}
						}
					}
				}
			}
		}
		//Place 1(first floor) or 2 elevators randomly with the ELEVATOR region and sets enemies to 0 (we don't want any enemies in the elevator or walls)
		for (int elevator = 0; elevator < Mathf.Min(currentFloor + 1, 2); elevator++) {
			int elevatorWidth = 4;
			int elevatorHeight = 4;
			int x = Mathf.RoundToInt (Random.Range (0, width - elevatorWidth));
			int y = Mathf.RoundToInt (Random.Range (0, height - elevatorHeight));
			if (currentFloor != 0) {
				player.transform.position = new Vector2 (x+1, y+1);
			}
			for (int i = x; i < x + elevatorWidth; i++) {
				for (int j = y; j < y + elevatorHeight; j++) {
					if (i == x || i == x + elevatorWidth - 1 || j == y || j == y + elevatorHeight - 1) { //in first and last row and column place wall
						maze [i, j] = WALL;
						region [i, j] = elevatorRegion;
						enemies [i, j] = 0;
					} else {
						maze [i, j] = ELEVATORFLOOR;
						region [i, j] = elevatorRegion;
						enemies [i, j] = 0;
					}
				}
			}
			elevatorRegion++;
		}
		//Place cells randomly with a new region and sets enemies to 0 (we don't want any enemies in the cells or walls)
		for (int cell = 0; cell < amountOfCells; cell++) {
			int cellWidth = 4;
			int cellHeight = 4;
			int x = Mathf.RoundToInt (Random.Range (0, width - cellWidth));
			int y = Mathf.RoundToInt (Random.Range (0, height - cellHeight));

			if (currentFloor == 0 && cell == 0) {
				player.transform.position = new Vector2 (x+1, y+1);
			}
			currentRegion++;
			for (int i = x; i < x + cellWidth; i++) {
				for (int j = y; j < y + cellHeight; j++) {
					if (i == x || i == x + cellWidth - 1 || j == y || j == y + cellHeight - 1) { //in first and last row and column place wall
						maze [i, j] = PRISONWALL;
						region [i, j] = currentRegion;
						enemies [i, j] = 0;
					} else {
						maze [i, j] = FLOOR;
						region [i, j] = currentRegion;
						enemies [i, j] = 0;
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
					Instantiate (floorTile, new Vector2 (x, y), Quaternion.identity);
				} else if (maze [x, y] == WALL) {
					Instantiate (wallTile, new Vector2 (x, y), Quaternion.identity);
				} else if (maze [x, y] == DOOR) {
					Instantiate (doorTile, new Vector2 (x, y), Quaternion.identity);
				} else if (maze [x, y] == PRISONDOOR) {
					Instantiate (prisonDoor, new Vector2 (x, y), Quaternion.identity);
				} else if (maze [x, y] == PRISONWALL) {
					Instantiate (prisonWall, new Vector2 (x, y), Quaternion.identity);
				} else if (maze [x, y] == ELEVATORDOOR) {
					Instantiate (elevatorDoor, new Vector2 (x, y), Quaternion.identity);
				} else if (maze [x, y] == ELEVATORFLOOR) {
					Instantiate (elevatorFloor, new Vector2 (x, y), Quaternion.identity);
				}

				if (enemies [x, y] == 1) {
					Instantiate (enemyPrefab1, new Vector2(x,y), Quaternion.identity);
				}
			}
		}
	}
}
