using UnityEngine;
using System.Collections;

public class InventoryDatabase : MonoBehaviour {

	public InventoryItem[,] inventoryData= new InventoryItem[6,4];
	public CombinerItem[,] combinerData= new CombinerItem[3,2];

	public JournalEntry[,] journalData = new JournalEntry[7,3];

	public int xSelected = 0;
	public int ySelected = 0;

	public int newItemX = 460; //+115
	public int newItemY = 180; //+170

	public string prompt = "";
	public Texture2D newItemSpr;
	public InventoryItem newItemMade = new InventoryItem();

	public Texture2D newItemDisplay;

	public bool createdNewItem = false;


	// Use this for initialization
	void Start () {		
		//Debug.Log ("START1");
		//initializeDBInfo();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void initializeDBInfo()
	{
		inventoryData[0,0] = new InventoryItem();
		inventoryData[0,0].name = "Carbon"; //Name
		inventoryData[0,0].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/carbon") as Texture2D;
		inventoryData[0,0].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/carbon_selected") as Texture2D;
		//inventoryData[0,0].itemSprite[1] = (Resources.Load("Assets/Inventory/Inventory Sprites/carbon") as Texture2D);
		//inventoryData[0,0].itemSprite[1].Resize(inventoryData[0,0].itemSprite[1].width+5, inventoryData[0,0].itemSprite[1].height+5);
		inventoryData[0,0]._amount = 9; //Amount
		inventoryData[0,0].description = "The elemental basis for all life on earth.  Diamond is an allotrope of this element. In Dutch, koolstof.  Cool stuff.  Its peculiar atomic structure allows it to readily bonds with most elements, forming the groundwork for a large variety of compounds."; //Inventory Info
		inventoryData[0,0].hasBeenFound = true; //Item has been found
		inventoryData[0,0].isSelected = true;
		
		inventoryData[1,0] = new InventoryItem();
		inventoryData[1,0].name = "Oxygen";
		inventoryData[1,0].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/oxygen") as Texture2D;
		inventoryData[1,0].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/oxygen_selected") as Texture2D;
		inventoryData[1,0]._amount = 9;
		inventoryData[1,0].description = "Third most abundant element in the universe. Most abundant element in the Earth’s crust (by mass).  Its availability in our atmosphere and its readiness to react with other elements and substances allows for the phenomenon known as combustion.";
		inventoryData[1,0].hasBeenFound = true;

		inventoryData[2,0] = new InventoryItem();
		inventoryData[2,0].name = "Hydrogen";
		inventoryData[2,0].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/hydrogen") as Texture2D;
		inventoryData[2,0].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/hydrogen_selected") as Texture2D;
		inventoryData[2,0]._amount = 9;
		inventoryData[2,0].description = "Most abundant element in the universe. The fuel of stars.   Highly flammable.  Hydrogen is unique among the elements due to it lacking the subatomic nuclear particle known as the neutron.  It is believed that hydrogen was the original element that was compressed under gravity and high pressure to form all other elements.";
		inventoryData[2,0].hasBeenFound = true;

		inventoryData[0,1] = new InventoryItem();
		inventoryData[0,1].name = "Nitrogen";
		inventoryData[0,1].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/nitrogen") as Texture2D;
		inventoryData[0,1].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/nitrogen_selected") as Texture2D;
		inventoryData[0,1]._amount = 9;
		inventoryData[0,1].description = "Most abundant gas in Earth’s atmosphere. Makes great beer.  Its importance in organisms stems from it being a component in the elemental makeup of amino acids, which are important to living things.";
		inventoryData[0,1].hasBeenFound = true;

		inventoryData[1,1] = new InventoryItem();
		inventoryData[1,1].name = "Sulfur";
		inventoryData[1,1].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/sulfur") as Texture2D;
		inventoryData[1,1].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/sulfur_selected") as Texture2D;
		inventoryData[1,1]._amount = 9;
		inventoryData[1,1].description = "Referenced in the Bible as brimstone. Used to make high quality gun powder. Used in concrete. Smells like rotten eggs.  Sulfur is a necessary element for proper functioning of life and can be found in certain vitamins.";
		inventoryData[1,1].hasBeenFound = true;

		inventoryData[2,1] = new InventoryItem();
		inventoryData[2,1].name = "Phosphorus";
		inventoryData[2,1].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/whitephosphorus") as Texture2D;
		inventoryData[2,1].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/whitephosphorus_selected") as Texture2D;
		inventoryData[2,1]._amount = 9;
		inventoryData[2,1].description = "Named after the Greek “light-bringer”.  In alchemy, it was one of the elements that was discovered in the search for the philosopher’s stone. Used in matches. TOXIC.  Also a necessary element for life.  Certain forms of this element spontaneously ignite on contact with air.";
		inventoryData[2,1].hasBeenFound = true;


		//-------------------------------------------------------------------------------------------------


		inventoryData[0,2] = new InventoryItem();
		inventoryData[0,2].name = "Methane";
		inventoryData[0,2].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/methane") as Texture2D;
		inventoryData[0,2].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/methane_selected") as Texture2D;
		inventoryData[0,2]._amount = 0;
		inventoryData[0,2].description = "The most abundant organic compound on Earth.  As a gas, is colorless, odorless.  Used as a fuel for heating and power generation.  Can be violently reactive with certain mixtures of air, and is considered a particularly effective greenhouse gas.";
		inventoryData[0,2].hasBeenFound = false;

		inventoryData[1,2] = new InventoryItem();
		inventoryData[1,2].name = "Water";
		inventoryData[1,2].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/water") as Texture2D;
		inventoryData[1,2].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/water_selected") as Texture2D;
		inventoryData[1,2]._amount = 0;
		inventoryData[1,2].description = "Known among junior chemists as the universal solvent.  Water is a byproduct of star formation.  Its molecular structure distributes its charges, creating dipoles which allow it to dissolve many other substances.";
		inventoryData[1,2].hasBeenFound = false;

		inventoryData[2,2] = new InventoryItem();
		inventoryData[2,2].name = "Hydrogen Peroxide";
		inventoryData[2,2].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/hydrogenperoxide") as Texture2D;
		inventoryData[2,2].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/hydrogenperoxide_selected") as Texture2D;
		inventoryData[2,2]._amount = 0;
		inventoryData[2,2].description = "Colorless. Due to its ability to readily react, it can be used as a cleaning agent or rocket fuel.  The simplest form of peroxide, it is commonly used as a sterilizing agent in multiple industries.  This is due to the single bonded oxygen readily reacting with other substances it comes in contact with.";
		inventoryData[2,2].hasBeenFound = false;

		inventoryData[0,3] = new InventoryItem();
		inventoryData[0,3].name = "Nitroglycerin";
		inventoryData[0,3].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/nitroglycerin") as Texture2D;
		inventoryData[0,3].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/nitroglycerin_selected") as Texture2D;
		inventoryData[0,3]._amount = 0;
		inventoryData[0,3].description = "Explosive.  It’s a colorless liquid that also has useful medical applications. Strongest practical explosive created since black powder. KEEP AWAY FROM FIRE. It is the main ingredient in dynamite and was used in everything from land clearance to wartime applications.  NitroGlycerin is a contact explosive, meaning that very low amounts of energy will cause it to explode if it is in a pure state.";
		inventoryData[0,3].hasBeenFound = false;

		inventoryData[1,3] = new InventoryItem();
		inventoryData[1,3].name = "Sulfuric Acid";
		inventoryData[1,3].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/sulfuricacid") as Texture2D;
		inventoryData[1,3].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/sulfuricacid_selected") as Texture2D;
		inventoryData[1,3]._amount = 0;
		inventoryData[1,3].description = "Mineral acid that is highly corrosive.  Known to occur naturally in the upper atmosphere of Venus.  Also known as vitriol it is a catalyst, electrolyte and though non-flammable will release hydrogen on contact with metal. Pure sulfuric acid is unstable.  KEEP AWAY FROM EVERYTHING.";
		inventoryData[1,3].hasBeenFound = false;

		inventoryData[2,3] = new InventoryItem();
		inventoryData[2,3].name = "Nitric Acid";
		inventoryData[2,3].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/nitricacid") as Texture2D;
		inventoryData[2,3].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/nitricacid_selected") as Texture2D;
		inventoryData[2,3]._amount = 0;
		inventoryData[2,3].description = "Corrosive, toxic, highly reactive. This compound decomposes when exposed to light. Used to make fertilizer (REALLY?!), explosives, and in rocket fuel (this I’ll believe).  Sounds like sulfuric acid, but worse.  Maybe I should just keep everything away from everything else.";
		inventoryData[2,3].hasBeenFound = false;

		inventoryData[3,3] = new InventoryItem();
		inventoryData[3,3].name = "Glycerin";
		inventoryData[3,3].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/glycerin") as Texture2D;
		inventoryData[3,3].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/Selected/glycerin_selected") as Texture2D;
		inventoryData[3,3]._amount = 0;
		inventoryData[3,3].description = "BOOM!!!!";
		inventoryData[3,3].hasBeenFound = false;


		//-------------------------------------------------------------------------------------------------


		inventoryData[3,0] = new InventoryItem();
		inventoryData[3,0].name = "Broken Jetpack";
		inventoryData[3,0].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/jetpackincomplete") as Texture2D;
		inventoryData[3,0].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/jetpackincomplete") as Texture2D;
		inventoryData[3,0]._amount = 0;
		inventoryData[3,0].description = "Info";
		inventoryData[3,0].hasBeenFound = false;

		inventoryData[4,0] = new InventoryItem();
		inventoryData[4,0].name = "Jetpack Part1";
		inventoryData[4,0].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/jetpackpart1") as Texture2D;
		inventoryData[4,0].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/jetpackpart1") as Texture2D;
		inventoryData[4,0]._amount = 0;
		inventoryData[4,0].description = "Info";
		inventoryData[4,0].hasBeenFound = false;

		inventoryData[5,0] = new InventoryItem();
		inventoryData[5,0].name = "Jetpack Part2";
		inventoryData[5,0].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/jetpackpart2") as Texture2D;
		inventoryData[5,0].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/jetpackpart2") as Texture2D;
		inventoryData[5,0]._amount = 0;
		inventoryData[5,0].description = "Info";
		inventoryData[5,0].hasBeenFound = false;

		inventoryData[3,1] = new InventoryItem();
		inventoryData[3,1].name = "Broken Radio";
		inventoryData[3,1].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/switchboardincomplete") as Texture2D;
		inventoryData[3,1].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/switchboardincomplete") as Texture2D;
		inventoryData[3,1]._amount = 0;
		inventoryData[3,1].description = "Info";
		inventoryData[3,1].hasBeenFound = false;

		inventoryData[4,1] = new InventoryItem();
		inventoryData[4,1].name = "Radio Part1";
		inventoryData[4,1].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/switchboardpart1") as Texture2D;
		inventoryData[4,1].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/switchboardpart1") as Texture2D;
		inventoryData[4,1]._amount = 0;
		inventoryData[4,1].description = "Info";
		inventoryData[4,1].hasBeenFound = false;

		inventoryData[5,1] = new InventoryItem();
		inventoryData[5,1].name = "Radio Part2";
		inventoryData[5,1].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/switchboardpart2") as Texture2D;
		inventoryData[5,1].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/switchboardpart2") as Texture2D;
		inventoryData[5,1]._amount = 0;
		inventoryData[5,1].description = "Info";
		inventoryData[5,1].hasBeenFound = false;

		inventoryData[3,2] = new InventoryItem();
		inventoryData[3,2].name = "Broken Teleporter";
		inventoryData[3,2].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/teleporterpart3") as Texture2D;
		inventoryData[3,2].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/teleporterpart3") as Texture2D;
		inventoryData[3,2]._amount = 0;
		inventoryData[3,2].description = "Info";
		inventoryData[3,2].hasBeenFound = false;

		inventoryData[4,2] = new InventoryItem();
		inventoryData[4,2].name = "Teleporter Part1";
		inventoryData[4,2].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/teleporterpart1") as Texture2D;
		inventoryData[4,2].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/teleporterpart1") as Texture2D;
		inventoryData[4,2]._amount = 0;
		inventoryData[4,2].description = "Info";
		inventoryData[4,2].hasBeenFound = false;

		inventoryData[5,2] = new InventoryItem();
		inventoryData[5,2].name = "Teleporter Part2";
		inventoryData[5,2].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/teleporterpart2") as Texture2D;
		inventoryData[5,2].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/teleporterpart2") as Texture2D;
		inventoryData[5,2]._amount = 0;
		inventoryData[5,2].description = "Info";
		inventoryData[5,2].hasBeenFound = false;

		inventoryData[4,3] = new InventoryItem();
		inventoryData[4,3].name = "Baron Von Huggington";
		inventoryData[4,3].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/baronvonhuggington") as Texture2D;
		inventoryData[4,3].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/baronvonhuggington") as Texture2D;
		inventoryData[4,3]._amount = 0;
		inventoryData[4,3].description = "Info";
		inventoryData[4,3].hasBeenFound = false;
		
		inventoryData[5,3] = new InventoryItem();
		inventoryData[5,3].name = "Empty";
		inventoryData[5,3].itemSprite[0] = Resources.Load("Assets/Inventory/Inventory Sprites/baronvonhuggington") as Texture2D;
		inventoryData[5,3].itemSprite[1] = Resources.Load("Assets/Inventory/Inventory Sprites/baronvonhuggington") as Texture2D;
		inventoryData[5,3]._amount = 0;
		inventoryData[5,3].description = "Info";
		inventoryData[5,3].hasBeenFound = false;
	
	}
	
	public void initializeCombinerInfo()
	{
		combinerData [0,0] = new CombinerItem ();
		combinerData [0,0].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/carbon") as Texture2D;
		combinerData [0,0]._amount = 0; //Amount
		combinerData [0,0].xLocation = 150;
		combinerData [0,0].yLocation = 45;
		combinerData [0,0].hasBeenFound = inventoryData[0,0].hasBeenFound; //Item has been found
		
		combinerData [1,0] = new CombinerItem ();
		combinerData [1,0].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/oxygen") as Texture2D;
		combinerData [1,0]._amount = 0;
		combinerData [1,0].xLocation = 185;
		combinerData [1,0].yLocation = 115;
		combinerData [1,0].hasBeenFound = inventoryData[1,0].hasBeenFound;
		
		combinerData [2,0] = new CombinerItem ();
		combinerData [2,0].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/hydrogen") as Texture2D;
		combinerData [2,0]._amount = 0;
		combinerData [2,0].xLocation = 155;
		combinerData [2,0].yLocation = 180;
		combinerData [2,0].hasBeenFound = inventoryData[2,0].hasBeenFound;
		
		combinerData [0,1] = new CombinerItem ();
		combinerData [0,1].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/nitrogen") as Texture2D;
		combinerData [0,1]._amount = 0;
		combinerData [0,1].xLocation = 65;
		combinerData [0,1].yLocation = 180;
		combinerData [0,1].hasBeenFound = inventoryData[0,1].hasBeenFound;
		
		combinerData [1,1] = new CombinerItem ();
		combinerData [1,1].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/sulfur") as Texture2D;
		combinerData [1,1]._amount = 0;
		combinerData [1,1].xLocation = 35;
		combinerData [1,1].yLocation = 115;
		combinerData [1,1].hasBeenFound = inventoryData[1,1].hasBeenFound;
		
		combinerData [2,1] = new CombinerItem ();
		combinerData [2,1].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/whitephosphorus") as Texture2D;
		combinerData [2,1]._amount = 0;
		combinerData [2,1].xLocation = 70;
		combinerData [2,1].yLocation = 45;
		combinerData [2,1].hasBeenFound = inventoryData[2,1].hasBeenFound;
	}

	public void combine()
	{
		//int newItemPosX = 0;
		//int newItemPosY = 0;

		newItemMade = inventoryData[0,0];

		if(combinerData[0,0]._amount == 0 && combinerData[1,0]._amount == 0 && combinerData[2,0]._amount == 0 && combinerData[0,1]._amount == 0 && combinerData[1,1]._amount == 0 && combinerData[2,1]._amount == 0 )
		{
			prompt = "Combiner is Empty!";
		}
		else
		{
			string newItem = "";
			//newItemSpr = sprite_empty;
			
			if(combinerData[0,0]._amount == 1 && combinerData[1,0]._amount == 0 && combinerData[2,0]._amount == 4 && combinerData[0,1]._amount == 0 && combinerData[1,1]._amount == 0 && combinerData[2,1]._amount == 0 )
			{
				newItem = "Methane";
				newItemSpr = inventoryData[0,2].itemSprite[0];
				newItemMade = inventoryData[0,2];
			}
			
			else if(combinerData[0,0]._amount == 3 && combinerData[1,0]._amount == 12 && combinerData[2,0]._amount == 3 && combinerData[0,1]._amount == 1 && combinerData[1,1]._amount == 0 && combinerData[2,1]._amount == 0 )
			{
				newItem = "NitroGlycerin";
				newItemSpr = inventoryData[0,3].itemSprite[0];
				newItemMade = inventoryData[0,3];
			}
			else if(combinerData[0,0]._amount == 3 && combinerData[1,0]._amount == 3 && combinerData[2,0]._amount == 8 && combinerData[0,1]._amount == 0 && combinerData[1,1]._amount == 0 && combinerData[2,1]._amount == 0 )
			{
				newItem = "Glycerin";
				newItemSpr = inventoryData[3,3].itemSprite[0];
				newItemMade = inventoryData[3,3];
			}
			
			else if(combinerData[0,0]._amount == 0 && combinerData[1,0]._amount == 3 && combinerData[2,0]._amount == 1 && combinerData[0,1]._amount == 1 && combinerData[1,1]._amount == 0 && combinerData[2,1]._amount == 0 )
			{
				newItem = "Nitric Acid";
				newItemSpr = inventoryData[2,3].itemSprite[0];
				newItemMade = inventoryData[2,3];
			}
			
			else if(combinerData[0,0]._amount == 0 && combinerData[1,0]._amount == 1 && combinerData[2,0]._amount == 2 && combinerData[0,1]._amount == 0 && combinerData[1,1]._amount == 0 && combinerData[2,1]._amount == 0 )
			{
				newItem = "Water";
				newItemSpr = inventoryData[1,2].itemSprite[0];
				newItemMade = inventoryData[1,2];
			}
			
			else if(combinerData[0,0]._amount == 0 && combinerData[1,0]._amount == 2 && combinerData[2,0]._amount == 2 && combinerData[0,1]._amount == 0 && combinerData[1,1]._amount == 0 && combinerData[2,1]._amount == 0 )
			{
				newItem = "Hydrogen Peroxide";
				newItemSpr = inventoryData[2,2].itemSprite[0];
				newItemMade = inventoryData[2,2];
			}
			
			else if(combinerData[0,0]._amount == 0 && combinerData[1,0]._amount == 4 && combinerData[2,0]._amount == 2 && combinerData[0,1]._amount == 0 && combinerData[1,1]._amount == 1 && combinerData[2,1]._amount == 0 )
			{
				newItem = "Sulfuric Acid";
				newItemSpr = inventoryData[1,3].itemSprite[0];
				newItemMade = inventoryData[1,3];
			}
			
			else
			{
				prompt = "Can't Combine!";
			}
			
			if( newItemMade.name != inventoryData[0,0].name )
			{   	
				resetCombiner();

				newItemMade._amount += 1;

				if( newItemMade.hasBeenFound == false )
				{
					newItemMade.hasBeenFound = true;
					createdNewItem = true;
				}



				 
				/*
				for(int i=0; i<inventoryData.GetLength(0); i++)
				{
					for(int j=0; j<inventoryData.GetLength(1); j++)
					{
						if(inventoryData[i,j].itemSprite[0] == newItemSpr)
						{
							if( inventoryData[i,j].hasBeenFound == false )
							{
								inventoryData[i,j].hasBeenFound = true;
								
								//state = "newItemMade";
							}
							
							inventoryData[i,j]._amount += 1;
							
							//instance_create((startX+(inventoryData[i, locX]) * 46), (startY+(inventoryData[i, locY]) * 46), obj_plusOne);
							

							//combinedItem = inventoryData[i,j].itemSprite[0];

							
							prompt = "Congrats! You created some " + newItem + "!";
						}
						
					}
				}
				*/
			}
			
		}
	}

	public void resetCombiner()
	{
		for (int i=0; i<3; i++) 
		{
			for (int j=0; j<2; j++) 
			{	
				if( combinerData[i,j]._amount != 0 )
				{
					combinerData[i,j]._amount = 0;
				}
			}
		}

	}

	public void cancelCombiner()
	{
		for (int i=0; i<3; i++) 
		{
			for (int j=0; j<2; j++) 
			{	
				if( combinerData[i,j]._amount > 0 )
				{
					inventoryData[i,j]._amount += combinerData[i,j]._amount;
					combinerData[i,j]._amount = 0;
				}
			}
			
		}
	}

	public void initializeJournalInfo()
	{
		for( int i=0;i<3; i++ )
		{
			for (int j=0; j<7; j++) 
			{	
				journalData[j,i]= new JournalEntry();
			}
		}

		journalData[0,0].pageEntry = "This is the Journal";
		journalData[0,0].hasBeenMade = true;
		journalData[0,0].isSelected = true;
		journalData[1,0].pageEntry = "It is an Awesome Journal";
		journalData[1,0].hasBeenMade = true;
		journalData[2,0].pageEntry = "When I say 'Buttered' you say Toast";
		journalData[2,0].hasBeenMade = true;
		journalData[3,0].pageEntry = "Buttered!";
		journalData[3,0].hasBeenMade = true;
		journalData[4,0].pageEntry = "Toast!";
		journalData[4,0].hasBeenMade = true;
		journalData[5,0].pageEntry = "I can't believe...";
		journalData[5,0].hasBeenMade = true;
		journalData[6,0].pageEntry = "That its Buttered Toast!";
		journalData[6,0].hasBeenMade = true;
		
		journalData[0,1].pageEntry = "1!";
		journalData[0,1].hasBeenMade = true;
		journalData[1,1].pageEntry = "2!";
		journalData[1,1].hasBeenMade = true;
		journalData[2,1].pageEntry = "3!";
		journalData[2,1].hasBeenMade = true;
		journalData[3,1].pageEntry = "4!";
		journalData[4,1].pageEntry = "5!";
		journalData[5,1].pageEntry = "6!";
		journalData[6,1].pageEntry = "7!";
		
		journalData[0,2].pageEntry = "1!";
		journalData[1,2].pageEntry = "2!";
		journalData[2,2].pageEntry = "";
		journalData[3,2].pageEntry = "";
		journalData[4,2].pageEntry = "";
		journalData[5,2].pageEntry = "";
		journalData[6,2].pageEntry = "";
	}


}


