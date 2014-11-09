using UnityEngine;
using System.Collections;

public class Inventory : InventoryDatabase {

	public Texture2D inventoryBg; //= Resources.Load("Assets/Inventory/Inventory Texture2Ds/carbon") as Texture2D;
	public Texture2D itemsBg;
	public Texture2D itemsInfo;
	public Texture2D combinerBg;
	public Texture2D combiner;

	public Texture2D teddy;

	public bool canHasMenu;// = true;

	public int startX = 0;
	public int startY = 0;
	public int itemStartX;
	public int itemStartY;
	//itemStartX = startX + 10;
	//itemStartY = startY + 68;

	//public Texture2D newItemDisplay;
	
	// Use this for initialization
	void Start () {		
		initializeDBInfo();
		initializeCombinerInfo ();

		teddy = Resources.Load("Assets/Inventory/Inventory Sprites/baronvonhuggington") as Texture2D;
		newItemSpr = Resources.Load("Assets/Inventory/Inventory Sprites/carbon") as Texture2D;

		newItemDisplay = Resources.Load("Assets/Inventory/Combiner/Mixing Complete") as Texture2D;
		
		itemStartX = startX + 10;
		itemStartY = startY + 68;

		canHasMenu = false;
		createdNewItem = false;
	}
	
	// Update is called once per frame
	void Update () {

		if( createdNewItem == true && Input.anyKeyDown )
		{
			createdNewItem = false;
		}

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			if( canHasMenu == false )
				canHasMenu = true;
			else
				canHasMenu = false;
		}

		if( canHasMenu == true )
			getInput();

	}
	
	void OnGUI()
	{
		if( canHasMenu == true )
		{
			GUI.Box(new Rect(startX, startY, inventoryBg.width, inventoryBg.height), inventoryBg, "");
			GUI.Box(new Rect(itemStartX, itemStartY, itemsBg.width, itemsBg.height), itemsBg, "");
			GUI.Box(new Rect(startX+10, startY+299, itemsInfo.width, itemsInfo.height), itemsInfo, "");
			GUI.Box(new Rect(startX+367, startY+68, combinerBg.width, combinerBg.height), combinerBg, "");
			GUI.Box(new Rect(startX+400, startY+100, combiner.width, combiner.height), combiner, "");
			
			
			itemize();
			setCombiner ();


			if( createdNewItem == true )
			{
				//Debug.Log( newItemMade.name );
				GUI.Box(new Rect(newItemX, newItemY, newItemDisplay.width, newItemDisplay.height), newItemDisplay, "");
				GUI.Box(new Rect(newItemX+115, newItemY+170, newItemSpr.width, newItemSpr.height), newItemSpr, "");
			}
		}
	}
	
	public void itemize()
	{
		//Debug.Log (inventoryData[0,0].itemSprite[0]);
		
				
		for(int i=0; i<inventoryData.GetLength(0); i++)
		{
			for(int j=0; j<inventoryData.GetLength(1); j++)
			{
				//Debug.Log( inventoryData.GetLength(0) );
				//Debug.Log( inventoryData.GetLength(1) );
				
				if( inventoryData[i,j].hasBeenFound == true)
				{
					
					if( inventoryData[i,j].isSelected == true )
					{
						GUI.Box(new Rect(itemStartX+(i*46)+25, itemStartY+(j*46)+30, (teddy.width), (teddy.height)), teddy, "");

						GUI.Label (new Rect (30, 310, 325, 200), inventoryData[i,j].description);
					}
					else
					{
						GUI.Box(new Rect(itemStartX+(i*46)+25, itemStartY+(j*46)+30, inventoryData[i,j].itemSprite[0].width, inventoryData[i,j].itemSprite[0].height), inventoryData[i,j].itemSprite[0], "");
					}

					GUI.Label (new Rect (itemStartX+(i*46)+25, itemStartY+(j*46)+30, 20, 20), (inventoryData[i,j]._amount).ToString());
				}
			}
		}

	}

	public void setCombiner()
	{
		for (int i=0; i<3; i++) 
		{
			for (int j=0; j<2; j++) 
			{
				if( combinerData[i,j].hasBeenFound == true)
				{
					GUI.Box(new Rect(combinerData[i,j].xLocation+405, combinerData[i,j].yLocation+90, combinerData[i,j].itemSprite[0].width, combinerData[i,j].itemSprite[0].height), combinerData[i,j].itemSprite[0], "");

					GUI.Label (new Rect (combinerData[i,j].xLocation+405, combinerData[i,j].yLocation+90, 20, 20), (combinerData[i,j]._amount).ToString());
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
			
			while( isFound == false && (i<6 && j<4) )
			{
				if(inventoryData[i,j].hasBeenFound == true)
				{
					inventoryData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					inventoryData[xSelected,ySelected].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 5)
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
				if(inventoryData[i,j].hasBeenFound == true)
				{
					inventoryData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					inventoryData[xSelected,ySelected].isSelected = true;
					
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
			
			while( isFound == false && (i<6 && j<4) )
			{
				if(inventoryData[i,j].hasBeenFound == true)
				{
					inventoryData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					inventoryData[xSelected,ySelected].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 5)
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
				if(inventoryData[i,j].hasBeenFound == true)
				{
					inventoryData[xSelected,ySelected].isSelected = false;
					
					xSelected = i;
					ySelected = j;
					
					inventoryData[xSelected,ySelected].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 5)
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
		else if(Input.GetKeyDown(KeyCode.Return))
		{
			if( (xSelected < 6 && ySelected < 2) && inventoryData[xSelected, ySelected]._amount > 0 )
			{
				inventoryData[xSelected, ySelected]._amount-=1;
				combinerData[xSelected, ySelected]._amount+=1;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Backspace))
		{
			if( (xSelected < 6 && ySelected < 2) && combinerData[xSelected, ySelected]._amount > 0 )
			{
				combinerData[xSelected, ySelected]._amount-=1;
				inventoryData[xSelected, ySelected]._amount+=1;
			}
		}
		else if(Input.GetKeyDown(KeyCode.RightShift))
		{
			combine ();
		}
		else
		{

		}
	}
}
