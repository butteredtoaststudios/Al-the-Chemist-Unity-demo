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

	}
}
