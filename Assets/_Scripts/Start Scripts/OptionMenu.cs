
using UnityEngine;
using System.Collections;

public class OptionMenu : MonoBehaviour {

	private float startY;
	private float moveSpeed = 5;
	public float moveRate;

	public GUISkin optionGUI;

	private float positionY = -465;

	private int userChoice = 0;

	public bool showMenu = false;
	private bool menuDown = false;
	private bool toUpdateKeys = false;
	private bool toAssignNewKey = false;

	float bgVolume = 0.5f;
	float fxVolume = 0.5f;

	KeyCode selectedKey;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if(showMenu)
		{
			Debug.Log("MOVE");
			if(positionY < 0)
				positionY += moveSpeed;	
			else
				menuDown = true;

			if(menuDown)
				userControl();
		}
		else
		{	
			if(positionY > -465)
				positionY -= moveSpeed;	
			else
			{
				menuDown = false;
				GameObject.Find("Start Menu").GetComponent<StartMenu>().selected = false;
			}
			userChoice = 0;
		}

		updateKeys ();
	}

	public void executeFunction()
	{
		switch(userChoice)
		{
		case 0:
			showMenu = false;
			break;
		case 1:
			break;
		case 2:
			Debug.Log("sound");
			bgVolume = soundVolume(bgVolume, 0.1f);
			Camera.main.audio.volume = bgVolume;
			//audio.volume = bgVolume;
			break;
		case 3:
			break;

		case 4:
			fxVolume = soundVolume(fxVolume, 0.1f);
			break;
		
		case 7:
		case 8:
		case 9:
		case 10:
		case 11:
		case 12:
		case 13:
		case 14:
		case 15:
		case 16:
		case 17:
			toUpdateKeys = true;
			break;
		}
	}

	public float soundVolume(float curVolume, float value)
	{
		Debug.Log("volume");
		if(Input.GetKeyDown(KeyCode.LeftArrow) && curVolume > 0.1f)
			curVolume -= value;
		else if (Input.GetKeyDown(KeyCode.RightArrow)  && curVolume < 1)
			curVolume += value;

		return curVolume;
	}

	private void updateKeys()
	{
		if (toUpdateKeys) {
			Debug.Log ("update key");

			if(Input.GetKeyDown(KeyCode.Return))
			{
				toAssignNewKey = true;
				toUpdateKeys = false;
			}
		}

		if(toAssignNewKey)
		{
			Debug.Log("user start assigning new key");
			
			KeyList.keyUp = assignNewKey(7, KeyList.keyUp, selectedKey);
			KeyList.keyDown = assignNewKey(8, KeyList.keyDown, selectedKey);
			KeyList.keyLeft = assignNewKey(9, KeyList.keyLeft, selectedKey);
			KeyList.keyRight = assignNewKey(10, KeyList.keyRight, selectedKey);
			KeyList.keyJump = assignNewKey(11, KeyList.keyJump, selectedKey);
			KeyList.keyInteract = assignNewKey(12, KeyList.keyInteract, selectedKey);
			KeyList.keyEquipped = assignNewKey(13, KeyList.keyEquipped, selectedKey);
			KeyList.keyInventory = assignNewKey(14, KeyList.keyInventory, selectedKey);
			KeyList.keyMap = assignNewKey(15, KeyList.keyMap, selectedKey);
			KeyList.keyConfirm = assignNewKey(16, KeyList.keyConfirm, selectedKey);
			KeyList.keyMenu = assignNewKey(17, KeyList.keyMenu, selectedKey);

			toAssignNewKey = false;
		}
	}

	private KeyCode assignNewKey(int selectedKeyInt, KeyCode selectedKeyCode, KeyCode newKey)
	{
		if (Input.anyKeyDown) {
			if (userChoice == selectedKeyInt)
					return newKey;
		}
		return selectedKeyCode;
	}

	public void OnGUI()
	{
		selectedKey = Event.current.keyCode;

		GUI.skin = optionGUI;
		GUI.Box(new Rect(Screen.width/2 - 444/2, positionY, 444, 480), "", "optionPanel");

		buttonStyle(0, new Rect(182, positionY + 36, 90, 34), "closeOn", "closeOff");

		buttonStyle(1, new Rect(347, positionY + 90, 83, 26), "resetDefaultOn", "resetDefaultOff");

		buttonStyle(2, new Rect(182 + (bgVolume * 343), positionY + 125, 13, 26), "soundSlideOn", "soundSlideOff");

		buttonStyle(3, new Rect(310, positionY + 169, 83, 26), "resetDefaultOn", "resetDefaultOff");

		buttonStyle(4, new Rect(182 + (fxVolume * 343), positionY + 204, 13, 26), "soundSlideOn", "soundSlideOff");

		buttonStyle(5, new Rect(390, positionY + 250, 44, 26), "saveOn", "saveOff");

		buttonStyle(6, new Rect(440, positionY + 250, 83, 26), "resetDefaultOn", "resetDefaultOff");

		key();
	}

	void userControl()
	{
		if((Input.GetKeyDown(KeyCode.UpArrow) && userChoice > 0))
			userChoice -= 1;
		else if((Input.GetKeyDown(KeyCode.DownArrow) && userChoice < 17))
			userChoice += 1;

		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) || userChoice == 2 || userChoice == 4)
		   executeFunction();

	}

	public void key()
	{
		//up
		buttonStyle(7, new Rect(209, positionY + 317, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyUp.ToString());
		//down
		buttonStyle(8, new Rect(209, positionY + 342, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyDown.ToString());
		//left
		buttonStyle(9, new Rect(209, positionY + 367, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyLeft.ToString());
		//right
		buttonStyle(10, new Rect(209, positionY + 392, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyRight.ToString());

		//jump
		buttonStyle(11, new Rect(275, positionY + 330, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyJump.ToString());
		//interact
		buttonStyle(12, new Rect(369, positionY + 330, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyInteract.ToString());
		//equipped
		buttonStyle(13, new Rect(472, positionY + 330, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyEquipped.ToString());
		//inventory
		buttonStyle(14, new Rect(273, positionY + 391, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyInventory.ToString());

		//map
		buttonStyle(15, new Rect(341, positionY + 391, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyMap.ToString());
		//confirm
		buttonStyle(16, new Rect(409, positionY + 391, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyConfirm.ToString());
		//menu
		buttonStyle(17, new Rect(477, positionY + 391, 47, 25), "keyPanelOn", "keyPanelOff", KeyList.keyMenu.ToString());
	}

	private void buttonStyle(int choice, Rect rect, string onState, string offState, string value = "")
	{
		if(userChoice == choice)
			GUI.Box(rect, value, onState);
		else
			GUI.Box(rect, value, offState);
	}
}
