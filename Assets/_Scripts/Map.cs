using UnityEngine;
using System.Collections;

public class Map : MapDatabase {

	public Texture2D mapBg;
	public Texture2D locSelected;

	public bool canHasMap;

	public int startX = 0;
	public int startY = 0;

	public int mapStartX;
	public int mapStartY;

	// Use this for initialization
	void Start () {
		initializeMapDBInfo();

		locSelected = Resources.Load("Assets/Map/al_normal2") as Texture2D;

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

			plotMap();
		}
	}

	public void plotMap()
	{
		for(int i=0; i<mapData.GetLength(0); i++)
		{
			for(int j=0; j<mapData.GetLength(1); j++)
			{
				//Debug.Log( mapData[i,j].name );
				//Debug.Log( mapData.GetLength(1) );

				if( mapData[i,j].hasBeenFound == true )
				{
					if( mapData[i,j].isSelected == true )
					{
						GUI.Box(new Rect(mapStartX+(i*60)+8, mapStartY+(j*55)+8, locSelected.width, locSelected.height), locSelected, "");

					}
					else
					{
						GUI.Box(new Rect(mapStartX+(i*60), mapStartY+(j*55), (mapData[i,j].locationSprite).width, (mapData[i,j].locationSprite).height), mapData[i,j].locationSprite, "");
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
						i=5;
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
