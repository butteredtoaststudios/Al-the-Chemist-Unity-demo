
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

	float bgVolume = 0.5f;
	float fxVolume = 0.5f;

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

	public void OnGUI()
	{
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
		buttonStyle(7, new Rect(209, positionY + 317, 47, 25), "keyPanelOn", "keyPanelOff", "A");
		//down
		buttonStyle(8, new Rect(209, positionY + 342, 47, 25), "keyPanelOn", "keyPanelOff", "A");
		//left
		buttonStyle(9, new Rect(209, positionY + 367, 47, 25), "keyPanelOn", "keyPanelOff", "A");
		//right
		buttonStyle(10, new Rect(209, positionY + 392, 47, 25), "keyPanelOn", "keyPanelOff", "A");

		//jump
		buttonStyle(11, new Rect(275, positionY + 330, 47, 25), "keyPanelOn", "keyPanelOff", "A");
		//interact
		buttonStyle(12, new Rect(369, positionY + 330, 47, 25), "keyPanelOn", "keyPanelOff", "A");
		//equipped
		buttonStyle(13, new Rect(472, positionY + 330, 47, 25), "keyPanelOn", "keyPanelOff", "A");
		//inventory
		buttonStyle(14, new Rect(273, positionY + 391, 47, 25), "keyPanelOn", "keyPanelOff", "A");

		//map
		buttonStyle(15, new Rect(341, positionY + 391, 47, 25), "keyPanelOn", "keyPanelOff");
		//confirm
		buttonStyle(16, new Rect(409, positionY + 391, 47, 25), "keyPanelOn", "keyPanelOff");
		//menu
		buttonStyle(17, new Rect(477, positionY + 391, 47, 25), "keyPanelOn", "keyPanelOff");
	}

	private void buttonStyle(int choice, Rect rect, string onState, string offState, string value = "")
	{
		if(userChoice == choice)
			GUI.Box(rect, value, onState);
		else
			GUI.Box(rect, value, offState);
	}
}
