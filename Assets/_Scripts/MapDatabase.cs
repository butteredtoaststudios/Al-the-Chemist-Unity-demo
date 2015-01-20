using UnityEngine;
using System.Collections;

public class MapDatabase : MonoBehaviour {

	public MapLocation[,] mapData= new MapLocation[11,6];

	public int xSelected = 0;
	public int ySelected = 2;

	// Use this for initialization
	//void Start () {
	
	//}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void fillEmptyMapDBInfo()
	{
		for(int i=0; i<mapData.GetLength(0); i++)
		{
			for(int j=0; j<mapData.GetLength(1); j++)
			{
				mapData[i,j] = new MapLocation();
				mapData[i,j].name = "empty";
				mapData[i,j].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
				mapData[i,j].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
				mapData[i,j].hasBeenFound = false;
				mapData[i,j].connectedTo = new MapLocation[1];
				mapData[i,j].connectedTo[0] = mapData[0,0];
				mapData[i,j].xLocation = i;
				mapData[i,j].yLocation = j;
			}
		}
	}

	public void initializeMapDBInfo()
	{
		fillEmptyMapDBInfo ();

		//mapData[0,2] = new MapLocation();
		mapData[0,2].name = "Starting Room 1"; //Name
		mapData[0,2].locationSprite[0] = Resources.Load("Assets/Map/start_room2") as Texture2D; //Sprite name
		mapData[0,2].locationSprite[1] = Resources.Load("Assets/Map/start_room3") as Texture2D;
		mapData[0,2].locationNumber = 0; //Location Number
		mapData[0,2].roomType = 1; //Roomtype? Or Location
		mapData[0,2].hasBeenFound = true; //Location has been found
		mapData[0,2].isCurrentLocation = true; //isCurrentLocation 
		mapData[0,2].isSelected = true; //isSelected
		mapData[0,2].connectedTo = new MapLocation[1];

		//Debug.Log( (mapData[0,2].connectedTo[0]).name );

		//mapData[1,2] = new MapLocation();
		mapData[1,2].name = "Starting Room 2"; 
		mapData[1,2].locationSprite[0] = Resources.Load("Assets/Map/start_room2") as Texture2D;
		mapData[1,2].locationSprite[1] = Resources.Load("Assets/Map/start_room3") as Texture2D;
		mapData[1,2].locationNumber = 0; 
		mapData[1,2].roomType = 1; 
		mapData[1,2].hasBeenFound = true; 
		mapData[1,2].isCurrentLocation = false; 
		mapData[1,2].isSelected = false;
		mapData[1,2].connectedTo = new MapLocation[2];
		//Debug.Log( (mapData[0,2].connectedTo[0]).name );

		//mapData[1,3] = new MapLocation();
		mapData[1,3].name = "Starting Room 3"; 
		mapData[1,3].locationSprite[0] = Resources.Load("Assets/Map/start_room2") as Texture2D; 
		mapData[1,3].locationSprite[1] = Resources.Load("Assets/Map/start_room3") as Texture2D; 
		mapData[1,3].locationNumber = 0; 
		mapData[1,3].roomType = 1; 
		mapData[1,3].hasBeenFound = true; 
		mapData[1,3].isCurrentLocation = false; 
		mapData[1,3].isSelected = false;

		//mapData[2,2] = new MapLocation();
		mapData[2,2].name = "Path of Freedom"; 
		mapData[2,2].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[2,2].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D; 
		mapData[2,2].locationNumber = 0; 
		mapData[2,2].roomType = 1; 
		mapData[2,2].hasBeenFound = true; 
		mapData[2,2].isCurrentLocation = false; 
		mapData[2,2].isSelected = false; 
		mapData[2,2].connectedTo = new MapLocation[1];

		//mapData[3,2] = new MapLocation();
		mapData[3,2].name = "Anteroom of Omens"; 
		mapData[3,2].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[3,2].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D; 
		mapData[3,2].locationNumber = 0; 
		mapData[3,2].roomType = 1; 
		mapData[3,2].hasBeenFound = true; 
		mapData[3,2].isCurrentLocation = false; 
		mapData[3,2].isSelected = false; 
		mapData[3,2].connectedTo = new MapLocation[2];

		//mapData[4,2] = new MapLocation();
		mapData[4,2].name = "Transepts of Height"; 
		mapData[4,2].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[4,2].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D; 
		mapData[4,2].locationNumber = 0; 
		mapData[4,2].roomType = 1; 
		mapData[4,2].hasBeenFound = true; 
		mapData[4,2].isCurrentLocation = false; 
		mapData[4,2].isSelected = false;
		mapData[4,2].connectedTo = new MapLocation[3];

		//mapData[3,1] = new MapLocation();
		mapData[3,1].name = "Confessions of Icarus"; 
		mapData[3,1].locationSprite[0] = Resources.Load("Assets/Map/ruin_room2") as Texture2D;
		mapData[3,1].locationSprite[1] = Resources.Load("Assets/Map/ruin_room3") as Texture2D; 
		mapData[3,1].locationNumber = 0; 
		mapData[3,1].roomType = 1; 
		mapData[3,1].hasBeenFound = true; 
		mapData[3,1].isCurrentLocation = false; 
		mapData[3,1].isSelected = false;
		mapData[3,1].connectedTo = new MapLocation[1];

		//mapData[5,0] = new MapLocation();
		mapData[5,0].name = "Rooftops of Footing"; 
		mapData[5,0].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[5,0].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D; 
		mapData[5,0].locationNumber = 0; 
		mapData[5,0].roomType = 1; 
		mapData[5,0].hasBeenFound = true; 
		mapData[5,0].isCurrentLocation = false; 
		mapData[5,0].isSelected = false;
		mapData[5,0].connectedTo = new MapLocation[4];

		//mapData[8,0] = new MapLocation();
		mapData[8,0].name = "Vestibule of Reflexes"; 
		mapData[8,0].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[8,0].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D; 
		mapData[8,0].locationNumber = 0; 
		mapData[8,0].roomType = 1; 
		mapData[8,0].hasBeenFound = true; 
		mapData[8,0].isCurrentLocation = false; 
		mapData[8,0].isSelected = false;
		mapData[8,0].connectedTo = new MapLocation[1];

		//mapData[5,1] = new MapLocation();
		mapData[5,1].name = "Sacristy of Speech"; 
		mapData[5,1].locationSprite[0] = Resources.Load("Assets/Map/ruin_room2") as Texture2D;
		mapData[5,1].locationSprite[1] = Resources.Load("Assets/Map/ruin_room3") as Texture2D;
		mapData[5,1].locationNumber = 0; 
		mapData[5,1].roomType = 1; 
		mapData[5,1].hasBeenFound = true; 
		mapData[5,1].isCurrentLocation = false; 
		mapData[5,1].isSelected = false;
		mapData[5,1].connectedTo = new MapLocation[1];

		//mapData[6,1] = new MapLocation();
		mapData[6,1].name = "The Sulfur Sanctuary"; 
		mapData[6,1].locationSprite[0] = Resources.Load("Assets/Map/ruin_room2") as Texture2D;
		mapData[6,1].locationSprite[1] = Resources.Load("Assets/Map/ruin_room3") as Texture2D;
		mapData[6,1].locationNumber = 0; 
		mapData[6,1].roomType = 1; 
		mapData[6,1].hasBeenFound = true; 
		mapData[6,1].isCurrentLocation = false; 
		mapData[6,1].isSelected = false; 

		//mapData[7,1] = new MapLocation();
		mapData[7,1].name = "Nitric Keep"; 
		mapData[7,1].locationSprite[0] = Resources.Load("Assets/Map/ruin_room2") as Texture2D;
		mapData[7,1].locationSprite[1] = Resources.Load("Assets/Map/ruin_room3") as Texture2D; 
		mapData[7,1].locationNumber = 0; 
		mapData[7,1].roomType = 1; 
		mapData[7,1].hasBeenFound = true; 
		mapData[7,1].isCurrentLocation = false; 
		mapData[7,1].isSelected = false; 

		//mapData[5,2] = new MapLocation();
		mapData[5,2].name = "Stairs of the Chosen"; 
		mapData[5,2].locationSprite[0] = Resources.Load("Assets/Map/ruin_room2") as Texture2D;
		mapData[5,2].locationSprite[1] = Resources.Load("Assets/Map/ruin_room3") as Texture2D;
		mapData[5,2].locationNumber = 0; 
		mapData[5,2].roomType = 1; 
		mapData[5,2].hasBeenFound = true; 
		mapData[5,2].isCurrentLocation = false; 
		mapData[5,2].isSelected = false;
		mapData[5,2].connectedTo = new MapLocation[1];

		//mapData[6,2] = new MapLocation();
		mapData[6,2].name = "Temple of Flight"; 
		mapData[6,2].locationSprite[0] = Resources.Load("Assets/Map/ruin_room2") as Texture2D;
		mapData[6,2].locationSprite[1] = Resources.Load("Assets/Map/ruin_room3") as Texture2D;
		mapData[6,2].locationNumber = 0; 
		mapData[6,2].roomType = 1; 
		mapData[6,2].hasBeenFound = true; 
		mapData[6,2].isCurrentLocation = false; 
		mapData[6,2].isSelected = false; 

		//mapData[7,2] = new MapLocation();
		mapData[7,2].name = "Rafters of Convection"; 
		mapData[7,2].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[7,2].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[7,2].locationNumber = 0; 
		mapData[7,2].roomType = 1; 
		mapData[7,2].hasBeenFound = true; 
		mapData[7,2].isCurrentLocation = false; 
		mapData[7,2].isSelected = false;
		mapData[7,2].connectedTo = new MapLocation[1];

		//mapData[5,3] = new MapLocation();
		mapData[5,3].name = "Stairs of the Chosen"; 
		mapData[5,3].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[5,3].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[5,3].locationNumber = 0; 
		mapData[5,3].roomType = 1; 
		mapData[5,3].hasBeenFound = true; 
		mapData[5,3].isCurrentLocation = false; 
		mapData[5,3].isSelected = false; 

		//mapData[8,3] = new MapLocation();
		mapData[8,3].name = "Garderobe of Eternity"; 
		mapData[8,3].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[8,3].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[8,3].locationNumber = 0; 
		mapData[8,3].roomType = 1; 
		mapData[8,3].hasBeenFound = true; 
		mapData[8,3].isCurrentLocation = false; 
		mapData[8,3].isSelected = false; 

		//mapData[5,4] = new MapLocation();
		mapData[5,4].name = "Trial of Faith"; 
		mapData[5,4].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[5,4].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[5,4].locationNumber = 0; 
		mapData[5,4].roomType = 1; 
		mapData[5,4].hasBeenFound = true; 
		mapData[5,4].isCurrentLocation = false; 
		mapData[5,4].isSelected = false;
		mapData[5,4].connectedTo = new MapLocation[2];

		//mapData[6,4] = new MapLocation();
		mapData[6,4].name = "Trial of Faith"; 
		mapData[6,4].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[6,4].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[6,4].locationNumber = 0; 
		mapData[6,4].roomType = 1; 
		mapData[6,4].hasBeenFound = true; 
		mapData[6,4].isCurrentLocation = false; 
		mapData[6,4].isSelected = false;
		mapData[6,4].connectedTo = new MapLocation[2];

		//mapData[7,4] = new MapLocation();
		mapData[7,4].name = "Path of Anywhere"; 
		mapData[7,4].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[7,4].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[7,4].locationNumber = 0; 
		mapData[7,4].roomType = 1; 
		mapData[7,4].hasBeenFound = true; 
		mapData[7,4].isCurrentLocation = false; 
		mapData[7,4].isSelected = false;
		mapData[7,4].connectedTo = new MapLocation[3];

		//mapData[8,4] = new MapLocation();
		mapData[8,4].name = "Vault of Change"; 
		mapData[8,4].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[8,4].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[8,4].locationNumber = 0; 
		mapData[8,4].roomType = 1; 
		mapData[8,4].hasBeenFound = true; 
		mapData[8,4].isCurrentLocation = false; 
		mapData[8,4].isSelected = false;
		mapData[8,4].connectedTo = new MapLocation[1];

		//mapData[9,4] = new MapLocation();
		mapData[9,4].name = "Whims of Loki"; 
		mapData[9,4].locationSprite[0] = Resources.Load("Assets/Map/ruin_room2") as Texture2D;
		mapData[9,4].locationSprite[1] = Resources.Load("Assets/Map/ruin_room3") as Texture2D;
		mapData[9,4].locationNumber = 0; 
		mapData[9,4].roomType = 1; 
		mapData[9,4].hasBeenFound = true; 
		mapData[9,4].isCurrentLocation = false; 
		mapData[9,4].isSelected = false; 

		//mapData[5,5] = new MapLocation();
		mapData[5,5].name = "Lair of the Ant"; 
		mapData[5,5].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[5,5].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[5,5].locationNumber = 0; 
		mapData[5,5].roomType = 1; 
		mapData[5,5].hasBeenFound = true; 
		mapData[5,5].isCurrentLocation = false; 
		mapData[5,5].isSelected = false;
		mapData[5,5].connectedTo = new MapLocation[1];

		//mapData[6,5] = new MapLocation();
		mapData[6,5].name = "Holy Boiler Room"; 
		mapData[6,5].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[6,5].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[6,5].locationNumber = 0; 
		mapData[6,5].roomType = 1; 
		mapData[6,5].hasBeenFound = true; 
		mapData[6,5].isCurrentLocation = false; 
		mapData[6,5].isSelected = false;
		mapData[6,5].connectedTo = new MapLocation[2];

		//mapData[8,5] = new MapLocation();
		mapData[8,5].name = "Ambulatory of Distance"; 
		mapData[8,5].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[8,5].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D; 
		mapData[8,5].locationNumber = 0; 
		mapData[8,5].roomType = 1; 
		mapData[8,5].hasBeenFound = true; 
		mapData[8,5].isCurrentLocation = false; 
		mapData[8,5].isSelected = false;
		mapData[8,5].connectedTo = new MapLocation[1];

		//mapData[9,5] = new MapLocation();
		mapData[9,5].name = "Ambulatory of Distance Deuce"; 
		mapData[9,5].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[9,5].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[9,5].locationNumber = 0; 
		mapData[9,5].roomType = 1; 
		mapData[9,5].hasBeenFound = true; 
		mapData[9,5].isCurrentLocation = false; 
		mapData[9,5].isSelected = false;
		mapData[9,5].connectedTo = new MapLocation[1];

		//mapData[10,5] = new MapLocation();
		mapData[10,5].name = "Kapoof!! Room"; 
		mapData[10,5].locationSprite[0] = Resources.Load("Assets/Map/reg_room2") as Texture2D;
		mapData[10,5].locationSprite[1] = Resources.Load("Assets/Map/reg_room3") as Texture2D;
		mapData[10,5].locationNumber = 0; 
		mapData[10,5].roomType = 1; 
		mapData[10,5].hasBeenFound = true; 
		mapData[10,5].isCurrentLocation = false; 
		mapData[10,5].isSelected = false;


		mapData[0,2].connectedTo[0] = mapData[1,2];

		mapData[1,2].connectedTo[0] = mapData[1,3];
		mapData[1,2].connectedTo[1] = mapData[2,2];

		mapData[2,2].connectedTo[0] = mapData[3,2];

		mapData[3,2].connectedTo[0] = mapData[4,2];
		mapData[3,2].connectedTo[1] = mapData[3,1];

		mapData[4,2].connectedTo[0] = mapData[5,1];
		mapData[4,2].connectedTo[1] = mapData[5,3];
		mapData[4,2].connectedTo[2] = mapData[5,4];

		mapData[3,1].connectedTo[0] = mapData[5,0];

		mapData[5,0].connectedTo[0] = mapData[5,1];
		mapData[5,0].connectedTo[1] = mapData[6,1];
		mapData[5,0].connectedTo[2] = mapData[7,1];
		mapData[5,0].connectedTo[3] = mapData[8,0];

		mapData[8,0].connectedTo[0] = mapData[8,3];

		mapData[5,1].connectedTo[0] = mapData[5,2];
		
		mapData[5,2].connectedTo[0] = mapData[5,3];
		
		mapData[7,2].connectedTo[0] = mapData[8,0];

		mapData[5,4].connectedTo[0] = mapData[6,4];
		mapData[5,4].connectedTo[1] = mapData[5,5];
		
		mapData[6,4].connectedTo[0] = mapData[6,2];
		mapData[6,4].connectedTo[1] = mapData[7,4];

		mapData[7,4].connectedTo[0] = mapData[8,3];
		mapData[7,4].connectedTo[1] = mapData[8,4];
		mapData[7,4].connectedTo[2] = mapData[8,5];
		
		mapData[8,4].connectedTo[0] = mapData[9,4];

		mapData[5,5].connectedTo[0] = mapData[6,5];
		
		mapData[6,5].connectedTo[0] = mapData[7,2];
		mapData[6,5].connectedTo[1] = mapData[7,4];
		
		mapData[8,5].connectedTo[0] = mapData[9,5];
		
		mapData[9,5].connectedTo[0] = mapData[10,5];

	}



}
