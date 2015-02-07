using UnityEngine;
using System.Collections;

public class Tutorial_Common : MonoBehaviour {

	public GUISkin skin;

	private GameObject Al;
	private GameObject Global;

	private bool showInstruction = false;
	private string instructText = "";

	private bool[] scriptActive = new bool[]{true, false, false, false, false, false};
	public bool[] userPressKey = new bool[]{false, false};
	private bool showInventory = false;

	// Use this for initialization
	void Start () {
	
		Camera.main.GetComponent<SmoothFollow> ().toUpdateCamera = false;
		
		Global = GameObject.Find ("GlobalObject");

		playScript.loadScript = false;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(scriptActive[0])
		{
			playScript.startScript ("tutorial_common01.txt", false);

			showInstruction = true;
			instructText = "Help Al escape his lab!";

			if(playScript.textInt == 2)
				executeFunction("normalCamera");

			if(playScript.textInt == -1)
			{
				scriptActive[0] = false;
				scriptActive[1] = true;
			}
		}

		if(scriptActive[1])
		{
			if(!showInventory)
			{
				instructText = "Make some water to put out the fire! Press '" + KeyList.keyInventory.ToString() + "' to open up the inventory.";

				if(Input.GetKeyDown(KeyList.keyInventory) )
					showInventory = true;
			}
			else
			{
				if(!userPressKey[0] || !userPressKey[1])
				{
					Debug.Log("keyys");
					showInventory = true;
					instructText = "Use the direction keys to select an item";

					if(Input.GetKey(KeyList.keyRight))
						userPressKey[0] = true;
					
					if(Input.GetKey(KeyList.keyLeft))
						userPressKey[1] = true;
				}
				
				if(userPressKey[0] && userPressKey[1])
				{
					Debug.Log("not keyys");

					instructText = "To make water, first add two Hydrogen elements to the combiner by highlighting it with the cursor and then pressing the '" + KeyList.keyConfirm.ToString() + "' key. Use the '<What is the cancel key?" + "' key to subtract elements.";
					
	//				userPressKey[0] = false;
	//				userPressKey[1] = false;
					
					if(Input.GetKey(KeyCode.Return))
						userPressKey[0] = true;
					
					if(Input.GetKey(KeyCode.Backspace))
						userPressKey[1] = true;
					
					if(userPressKey[0] && userPressKey[1])
					{
						instructText = "Try combining 2 Hydrogens and 1 Oxygen with the right 'SHIFT' key";
						
						if( Global.GetComponent<ElementInterfaceScript>().ID.inventoryData[1,2].hasBeenFound )
							instructText = "Press '" + KeyList.keyInventory.ToString() + "' again to close the inventory menu";
					}
					
					
				}
				
			}
		}
	}

	void executeFunction(string func)
	{
		switch(func)
		{
		case "normalCamera":
			Camera.main.GetComponent<TutorialCamera>().enabled = false;
			Camera.main.GetComponent<SmoothFollow> ().toUpdateCamera = true;
			break;
		}
	}

	void OnGUI()
	{
		GUI.skin = skin;
		
		if(showInstruction)
			GUI.Window(22, new Rect(Screen.width - 218 - 6, 6, 218, 85), InstructWindow, "", "instructBox");
	}

	public void InstructWindow(int widID)
	{
		if(showInstruction)
			GUI.Box(new Rect(10, 10, 200, 47), instructText, "instructFont"); 
	}
}
