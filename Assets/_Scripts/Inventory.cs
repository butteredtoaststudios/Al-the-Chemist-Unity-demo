using UnityEngine;
using System.Collections;

public class Inventory : InventoryDatabase {

	public Texture2D inventoryBg; //= Resources.Load("Assets/Inventory/Inventory Texture2Ds/carbon") as Texture2D;
	public Texture2D itemsBg;
	public Texture2D itemsInfo;
	public Texture2D combinerBg;
	public Texture2D combiner;

	public Texture2D combineButtonBlue;
	public Texture2D combineButtonRed;
	public Texture2D cancelButtonBlue;
	public Texture2D cancelButtonRed;

	public Texture2D teddy;

	public Texture2D itemsButtonOn;
	public Texture2D itemsButtonOff;
	public Texture2D journalButtonOn;
	public Texture2D journalButtonOff;

	public bool canHasMenu;// = true;

	public int startX = 0;
	public int startY = 0;
	public int itemStartX;
	public int itemStartY;
	public int journalX = 0;
	public int journalY = 0;

	int x_adj = -10;
	int y_adj = -5;

	// Equals itemStartX and itemStartY
	//int sprite_items_x = startX+10+x_adj;
	//int sprite_items_y = startY+68+y_adj;
	
	int combinerStartX;
	int combinerStartY;
	
	int infoBlankStartX;
	int infoBlankStartY;
	
	int mixerStartX;
	int mixerStartY;

	int combinerSelection = 1;

	string mainTitle;
	string combinerTitle;
	string infoTitle;

	string state = "";
	

	// Use this for initialization
	void Start () {		
		initializeDBInfo();
		initializeCombinerInfo ();
		initializeJournalInfo ();

		state = "menu";

		teddy = Resources.Load("Assets/Inventory/Inventory Sprites/baronvonhuggington") as Texture2D;
		newItemSpr = Resources.Load("Assets/Inventory/Inventory Sprites/carbon") as Texture2D;

		combineButtonBlue = Resources.Load("Assets/Inventory/Combiner/combineButtonBlue") as Texture2D;;
		combineButtonRed = Resources.Load("Assets/Inventory/Combiner/combineButtonRed") as Texture2D;;
		cancelButtonBlue = Resources.Load("Assets/Inventory/Combiner/cancelButtonBlue") as Texture2D;;
		cancelButtonRed = Resources.Load("Assets/Inventory/Combiner/cancelButtonRed") as Texture2D;;

		newItemDisplay = Resources.Load("Assets/Inventory/Combiner/Mixing Complete") as Texture2D;

		itemsButtonOn = Resources.Load("Assets/Inventory/button-items-on") as Texture2D;
		itemsButtonOff = Resources.Load("Assets/Inventory/button-items") as Texture2D;
		journalButtonOn = Resources.Load("Assets/Inventory/button-journal-on") as Texture2D;
		journalButtonOff = Resources.Load("Assets/Inventory/button-journal") as Texture2D;
		
		itemStartX = startX;
		itemStartY = startY + 63;

		canHasMenu = false;
		createdNewItem = false;

		combinerStartX = startX+357;
		combinerStartY = startY+63;
		
		infoBlankStartX = startX;
		infoBlankStartY = startY+294;
		
		mixerStartX = startX + 400;
		mixerStartY = startY + 100;
		
		mainTitle = "LAB COMMONS";
		combinerTitle = "CHEMICAL BOND SEQUENCER";
		infoTitle = "";

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
		{
			if( state == "journal" )
			{
				getJournalInput();
			}
			else if(state == "combiner")
			{
				getCombinerInput();
			}
			else
			{
				getInput();
			}
		}
	}
	
	void OnGUI()
	{

		if( canHasMenu == true )
		{
			GUI.Box(new Rect(startX, startY, inventoryBg.width, inventoryBg.height), inventoryBg, "");
			GUI.Box(new Rect(itemStartX, itemStartY, itemsBg.width, itemsBg.height), itemsBg, "");
			GUI.Box(new Rect(infoBlankStartX, infoBlankStartY, itemsInfo.width, itemsInfo.height), itemsInfo, "");
			GUI.Box(new Rect(combinerStartX, combinerStartY, combinerBg.width, combinerBg.height), combinerBg, "");
			GUI.Box(new Rect(mixerStartX, mixerStartY, combiner.width, combiner.height), combiner, "");

			if( state == "journal" )
			{
				GUI.Box(new Rect(itemStartX+70, itemStartY+13, journalButtonOn.width, journalButtonOn.height), journalButtonOn, "");

				GUI.Box(new Rect(itemStartX+34, itemStartY+15, itemsButtonOff.width, itemsButtonOff.height), itemsButtonOff, "");

				setJournal();
			}
			else
			{
				GUI.Box(new Rect(itemStartX+74, itemStartY+15, journalButtonOff.width, journalButtonOff.height), journalButtonOff, "");
				
				GUI.Box(new Rect(itemStartX+30, itemStartY+13, itemsButtonOn.width, itemsButtonOn.height), itemsButtonOn, "");

				itemize();
			}

			GUI.Label (new Rect (startX+80, startY+10, 325, 200), mainTitle);
			GUI.Label (new Rect (combinerStartX+25, combinerStartY+25, 325, 200), combinerTitle);

			GUI.Label (new Rect (infoBlankStartX+30, infoBlankStartY+11, 325, 200), infoTitle); //40,310

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
						//GUI.Box(new Rect(itemStartX+(i*46)+25, itemStartY+(j*46)+30, (teddy.width), (teddy.height)), teddy, "");
						GUI.Box(new Rect(itemStartX+(i*46)+25, itemStartY+(j*46)+30, inventoryData[i,j].itemSprite[1].width, inventoryData[i,j].itemSprite[1].height), inventoryData[i,j].itemSprite[1], "");

						//GUI.Label (new Rect (30, 310, 325, 200), inventoryData[i,j].name);
						infoTitle = inventoryData[i,j].name;
						GUI.Label (new Rect (20, 320, 325, 200), inventoryData[i,j].description);
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
					GUI.Box(new Rect(combinerData[i,j].xLocation+400, combinerData[i,j].yLocation+90, combinerData[i,j].itemSprite[0].width, combinerData[i,j].itemSprite[0].height), combinerData[i,j].itemSprite[0], "");

					if( combinerData[i,j]._amount != 0 )
					{
						GUI.Label (new Rect (combinerData[i,j].xLocation+400, combinerData[i,j].yLocation+90, 20, 20), (combinerData[i,j]._amount).ToString());
					}
				}
			}
		}

		if(state == "combiner" && combinerSelection == 1)
		{
			GUI.Box(new Rect(startX+488, startY+333, combineButtonRed.width, combineButtonRed.height), combineButtonRed, "");
			GUI.Box(new Rect(startX+488, startY+363, cancelButtonBlue.width, cancelButtonBlue.height), cancelButtonBlue, "");
		}
		else if(state == "combiner" && combinerSelection == 2)
		{
			GUI.Box(new Rect(startX+488, startY+333, combineButtonBlue.width, combineButtonBlue.height), combineButtonBlue, "");
			GUI.Box(new Rect(startX+488, startY+363, cancelButtonRed.width, cancelButtonRed.height), cancelButtonRed, "");
		}
		else
		{
			GUI.Box(new Rect(startX+488, startY+333, combineButtonBlue.width, combineButtonBlue.height), combineButtonBlue, "");
			GUI.Box(new Rect(startX+488, startY+363, cancelButtonBlue.width, cancelButtonBlue.height), cancelButtonBlue, "");
		}
	}

	public void setJournal()
	{	
		for(int i=0; i<3; i++)
		{
			for(int j=0; j<7; j++)
			{
				if( journalData[j,i].hasBeenMade == true )
				{
					if(j == journalX && i == journalY)
					{
						GUI.Box(new Rect(itemStartX+(j*46)+25, itemStartY+(i*46)+20, teddy.width, teddy.height), teddy, "");
						//draw_sprite( sprite_bear, 1, (startX+ (j * 46)), (startY+ (i* 46)) );
						GUI.Label (new Rect (startX+20, startY+325, 325, 200), journalData[j,i].pageEntry);
						//draw_text_ext(x+20, y+325, journalData[j, i], -1, infoLength);
						
						infoTitle = "TEXT";
						//draw_text( sprite_info_blank_x + 30, sprite_info_blank_y + 11, info_title );
					}
					else //if(journalData[i,j].pageEntry != "")
					{
						GUI.Box(new Rect(itemStartX+(j*46)+25, itemStartY+(i*46)+30, teddy.width, teddy.height), teddy, "");
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
						//j+=1;
						//i=0;

						isFound = true;

						state = "combiner";
						combinerSelection = 1;
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
		else if(Input.GetKeyDown(KeyCode.Space))
		{
			if( state != "journal" )
			{
				state = "journal";
			}
			else if( state == "journal")
			{
				state = "menu";
			}	
		}
	}

	public void getCombinerInput()
	{
		if(Input.GetKeyDown(KeyCode.LeftArrow))
		{
			state = "menu";
		}
		else if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			if(state == "combiner" && combinerSelection == 2)
			{
				combinerSelection = 1;
			}
		}
		else if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			if(state == "combiner" && combinerSelection == 1)
			{
				combinerSelection = 2;
			}
		}
		else if(Input.GetKeyDown(KeyCode.Return))
		{
			if( combinerSelection == 1 )
			{
				combine();
			}
			else
			{
				cancelCombiner();
			}
		}
	}

	public void getJournalInput()
	{
		
		if(Input.GetKeyDown(KeyCode.RightArrow))
		{
			bool isFound = false;
			int i=journalX+1;
			int j=journalY;
			
			while( isFound == false && (i<7 && j<3) )
			{
				if(journalData[i,j].hasBeenMade == true)
				{
					journalData[journalX,journalY].isSelected = false;
					
					journalX = i;
					journalY = j;
					
					journalData[journalX,journalY].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 6)
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
			int i=journalX-1;
			int j=journalY;
			
			while( isFound == false && (i>=0 && j>=0) )
			{
				if(journalData[i,j].hasBeenMade == true)
				{
					journalData[journalX,journalY].isSelected = false;
					
					journalX = i;
					journalY = j;
					
					journalData[journalX,journalY].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 0)
					{						
						//isFound = true;
						j-=1;
						i=6;
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
			int i=journalX;
			int j=journalY+1;
			
			while( isFound == false && (i<7 && j<3) )
			{
				if(journalData[i,j].hasBeenMade == true)
				{
					journalData[journalX,journalY].isSelected = false;
					
					journalX = i;
					journalY = j;
					
					journalData[journalX,journalY].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 6)
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
			int i=journalX;
			int j=journalY-1;
			
			while( isFound == false && (i>=0 && j>=0) )
			{
				if(journalData[i,j].hasBeenMade == true)
				{
					journalData[journalX,journalY].isSelected = false;
					
					journalX = i;
					journalY = j;
					
					journalData[journalX,journalY].isSelected = true;
					
					isFound = true;
				}
				else
				{
					if(i == 6)
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
		else if(Input.GetKeyDown(KeyCode.Space))
		{
			if( state != "journal" )
			{
				state = "journal";
			}
			else if( state == "journal")
			{
				state = "menu";
			}	
		}
	}

	public void inventoryTutorial()
	{


	}
}
