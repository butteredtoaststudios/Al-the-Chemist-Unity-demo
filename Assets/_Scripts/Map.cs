using UnityEngine;
using System.Collections;

public class Map : MapDatabase {

	public Texture2D mapBg;
	public Texture2D currentLocationNode;
	public Texture2D currentLocationSelectedNode;
	public Texture2D linePixel;

	public bool canHasMap;

	public int startX = 0;
	public int startY = 0;

	public int mapStartX;
	public int mapStartY;

	public string mapTitle = "";
	public string currentLocation = "";
	public string locationSelected = "";

	public int currentLocationX = 0;
	public int currentLocationY = 2;
	public int locationSelectedX = 0;
	public int locationSelectedY = 2;

	// Use this for initialization
	void Start () {
		initializeMapDBInfo();

		mapTitle = "MAP";

		currentLocationNode = Resources.Load("Assets/Map/al_normal2") as Texture2D;
		currentLocationSelectedNode = Resources.Load("Assets/Map/al_focused2") as Texture2D;

		linePixel = Resources.Load("Assets/Map/square_black") as Texture2D;

		canHasMap = false;
		mapStartX = startX + 15;
		mapStartY = startY + 45;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			if( canHasMap == false )
				canHasMap = true;
			else
				canHasMap = false;
		}
		
		if( canHasMap == true )
			getInput();
	}

	void OnGUI()
	{
		if( canHasMap == true )
		{
			GUI.Box(new Rect(startX, startY, mapBg.width, mapBg.height), mapBg, "");

			GUI.Label (new Rect (startX+80, startY+10, 325, 200), mapTitle);

			plotMap();

			//GUI.Box(new Rect(20, 40, 100, 100), "");

			//GUI.Box(new Rect(startX, startY, mapBg.width, mapBg.height), mapData[0,2].locationSprite, "");
		}
	}

	public void plotMap()
	{
		plotLines();
		plotLocations();
	}

	public void plotLocations()
	{	
		for(int i=0; i<mapData.GetLength(0); i++)
		{
			for(int j=0; j<mapData.GetLength(1); j++)
			{
				if( mapData[i,j].hasBeenFound == true )
				{
					if( mapData[i,j].isCurrentLocation == true && mapData[i,j].isSelected == true )
					{
						GUI.Box(new Rect(mapStartX+(i*60)+8, mapStartY+(j*55)+8, currentLocationSelectedNode.width, currentLocationSelectedNode.height), currentLocationSelectedNode, "");
						currentLocation = mapData[i,j].name;
						locationSelected = mapData[i,j].name;
					}
					else if( mapData[i,j].isCurrentLocation == true )
					{
						GUI.Box(new Rect(mapStartX+(i*60)+8, mapStartY+(j*55)+8, currentLocationNode.width, currentLocationNode.height), currentLocationNode, "");	
						currentLocation = mapData[i,j].name;
					}
					else if( mapData[i,j].isSelected == true )
					{
						GUI.Box(new Rect(mapStartX+(i*60), mapStartY+(j*55), (mapData[i,j].locationSprite[1]).width, (mapData[i,j].locationSprite[1]).height), mapData[i,j].locationSprite[1], "");	
						locationSelected = mapData[i,j].name;
					}
					else
					{
						GUI.Box(new Rect(mapStartX+(i*60), mapStartY+(j*55), (mapData[i,j].locationSprite[0]).width, (mapData[i,j].locationSprite[0]).height), mapData[i,j].locationSprite[0], "");
					}
						
				}
			}
		}

		GUI.Label (new Rect (50, 310, 325, 200), locationSelected );
		GUI.Label (new Rect (400, 40, 325, 200), currentLocation );
	}


	public void plotLines()
	{
		for(int i=0; i<mapData.GetLength(0); i++)
		{
			for(int j=0; j<mapData.GetLength(1); j++)
			{
				if( mapData[i,j].hasBeenFound == true )
				{
					//Debug.Log("WHAT IS THIS " + (mapData[i,j].connectedTo));
					
					
					for( int k=0; k < (mapData[i,j].connectedTo).Length; k++ )
					{
						//Debug.Log( (mapData[i,j].connectedTo[k]) );
						
						
						if( (mapData[i,j].connectedTo[k]).hasBeenFound == true )
						{
							if( (i==4 && j==2) || (i==7 && j==4) )
							{
								if( j < mapData[i,j].connectedTo[k].yLocation )
								{
									GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, Mathf.Abs ((i*60)-(mapData[i,j].connectedTo[k].xLocation*60)), 10), "");						
									GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(j*55)+35, 10, Mathf.Abs ((j*55)-(mapData[i,j].connectedTo[k].yLocation*55))), "");
								}
								else{
									GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, Mathf.Abs ((i*60)-(mapData[i,j].connectedTo[k].xLocation*60)), 10), "");						
									GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, 10, Mathf.Abs ((j*55)-(mapData[i,j].connectedTo[k].yLocation*55))), "");
								}
							}
							else if( (i==3 && j==1) )
							{
								if( j > mapData[i,j].connectedTo[k].yLocation )
								{
									GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, Mathf.Abs ((i*60)-(mapData[i,j].connectedTo[k].xLocation*60)), 10), "");						
									GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, 10, Mathf.Abs ((j*55)-(mapData[i,j].connectedTo[k].yLocation*55))), "");
								}
							}
							else if( (i==6 && j==5) || (i==7 && j==2) )
							{
								if( mapData[i,j].connectedTo[k].xLocation==7 && mapData[i,j].connectedTo[k].yLocation==2)
								{
									GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(j*55)+35, Mathf.Abs ((i*60)-(mapData[i,j].connectedTo[k].xLocation*60))/2, 10), "");						
									GUI.Box(new Rect( mapStartX+(i*60)+60, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, 10, Mathf.Abs ((j*55)-(mapData[i,j].connectedTo[k].yLocation*55))), "");
									GUI.Box(new Rect( mapStartX+(i*60)+60, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, Mathf.Abs ((i*60)-(mapData[i,j].connectedTo[k].xLocation*60))/2, 10), "");
								}
								else if( j > mapData[i,j].connectedTo[k].yLocation )
								{
									GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(j*55)+35, Mathf.Abs ((i*60)-(mapData[i,j].connectedTo[k].xLocation*60)), 10), "");						
									GUI.Box(new Rect( mapStartX+(mapData[i,j].connectedTo[k].xLocation*60)+30, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, 10, Mathf.Abs ((j*55)-(mapData[i,j].connectedTo[k].yLocation*55))), "");
								}
							}
							else if(i == mapData[i,j].connectedTo[k].xLocation && j < mapData[i,j].connectedTo[k].yLocation )
							{
								GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(j*55)+35, 10, Mathf.Abs ((j*55)-(mapData[i,j].connectedTo[k].yLocation*55))), "");
							}
							else if(i == mapData[i,j].connectedTo[k].xLocation && j > mapData[i,j].connectedTo[k].yLocation )
							{
								GUI.Box(new Rect( mapStartX+(mapData[i,j].connectedTo[k].xLocation*60)+30, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, 10, Mathf.Abs ((j*55)-(mapData[i,j].connectedTo[k].yLocation*55))), "");
							}
							
							else
							{
								GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(j*55)+35, Mathf.Abs ((i*60)-(mapData[i,j].connectedTo[k].xLocation*60)), 10), "");
							}
							
							if(i < mapData[i,j].connectedTo[k].xLocation)
							{
								if( j < mapData[i,j].connectedTo[k].yLocation )
								{
									GUI.Box(new Rect( mapStartX+(i*60)+30, mapStartY+(j*55)+35, Mathf.Abs ((i*60)-(mapData[i,j].connectedTo[k].xLocation*60)), 10), "");
									
									GUI.Box(new Rect( mapStartX+(mapData[i,j].connectedTo[k].xLocation*60)+30, mapStartY+(j*55)+35, 10, Mathf.Abs ((j*55)-(mapData[i,j].connectedTo[k].yLocation*55))), "");
									
									//GUI.Box(new Rect( mapStartX+(mapData[i,j].connectedTo[k].xLocation*60)+30, mapStartY+(mapData[i,j].connectedTo[k].yLocation*55)+35, 10, Mathf.Abs ((j*55)-(mapData[i,j].connectedTo[k].yLocation*55))), "");
								}
							}
						}
						
					}
					
				}
			}
		}
	}

	public void getInput()
	{
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			bool isFound = false;
			int i=xSelected+1;
			int j=ySelected;
			
			while( isFound == false && (i<11 && j<6) )
			{
				if(mapData[i,j].hasBeenFound == true)
				{
					mapData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					mapData[xSelected,ySelected].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 10)
					{						
						//isFound = true;
						j+=1;
						i=0;
					}
					else
					{
						i+=1;
					}
				}
			}	
		}
		else if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			bool isFound = false;
			int i=xSelected-1;
			int j=ySelected;
			
			while( isFound == false && (i>=0 && j>=0) )
			{
				if(mapData[i,j].hasBeenFound == true)
				{
					mapData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					mapData[xSelected,ySelected].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 0)
					{						
						//isFound = true;
						j-=1;
						i=10;
					}
					else
					{
						i-=1;
					}
				}
			}	
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			bool isFound = false;
			int i=xSelected;
			int j=ySelected+1;

			while( isFound == false && j<6 )
			{
				if(mapData[i,j].hasBeenFound == true)
				{
					mapData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					mapData[xSelected,ySelected].isSelected = true;
					
					isFound = true;
				}
				else{
					j++;
				}
			}

			j=ySelected+1;

			while( isFound == false && (i<11 && j<6) )
			{
				if(mapData[i,j].hasBeenFound == true)
				{
					mapData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					mapData[xSelected,ySelected].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 10)
					{						
						//isFound = true;
						j+=1;
						i=0;
					}
					else
					{
						i+=1;
					}
				}
			}	
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			bool isFound = false;
			int i=xSelected;
			int j=ySelected-1;

			while( isFound == false && j>=0 )
			{
				if(mapData[i,j].hasBeenFound == true)
				{
					mapData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					mapData[xSelected,ySelected].isSelected = true;
					
					isFound = true;
				}
				else{
					j--;
				}
			}

			j=ySelected-1;

			while( isFound == false && (i>=0 && j>=0) )
			{
				if(mapData[i,j].hasBeenFound == true)
				{
					mapData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					mapData[xSelected,ySelected].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 10)
					{						
						//isFound = true;
						j-=1;
						i=0;
					}
					else
					{
						i+=1;
					}
				}
			}	
		}

	}
}
