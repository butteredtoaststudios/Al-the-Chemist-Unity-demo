using UnityEngine;
using System.Collections;

public class CombinerDatabase : MonoBehaviour {

	public CombinerItem[] combinerData= new CombinerItem[6];

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void initializeCombinerInfo()
	{
		combinerData [0] = new CombinerItem ();
		combinerData [0].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/carbon") as Texture2D;
		combinerData [0]._amount = 0; //Amount
		combinerData [0].xLocation = 150;
		combinerData [0].yLocation = 45;
		combinerData [0].hasBeenFound = true; //Item has been found

		combinerData [1] = new CombinerItem ();
		combinerData [1].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/oxygen") as Texture2D;
		combinerData [1]._amount = 0;
		combinerData [1].xLocation = 185;
		combinerData [1].yLocation = 115;
		combinerData [1].hasBeenFound = true;

		combinerData [2] = new CombinerItem ();
		combinerData [2].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/hydrogen") as Texture2D;
		combinerData [2]._amount = 0;
		combinerData [2].xLocation = 155;
		combinerData [2].yLocation = 180;
		combinerData [2].hasBeenFound = true;

		combinerData [3] = new CombinerItem ();
		combinerData [3].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/nitrogen") as Texture2D;
		combinerData [3]._amount = 0;
		combinerData [3].xLocation = 65;
		combinerData [3].yLocation = 180;
		combinerData [3].hasBeenFound = true;

		combinerData [4] = new CombinerItem ();
		combinerData [4].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/sulfur") as Texture2D;
		combinerData [4]._amount = 0;
		combinerData [4].xLocation = 35;
		combinerData [4].yLocation = 115;
		combinerData [4].hasBeenFound = true;

		combinerData [5] = new CombinerItem ();
		combinerData [5].itemSprite [0] = Resources.Load ("Assets/Inventory/Inventory Sprites/whitephosphorous") as Texture2D;
		combinerData [5]._amount = 0;
		combinerData [5].xLocation = 70;
		combinerData [5].yLocation = 45;
		combinerData [5].hasBeenFound = true;

	}

}
