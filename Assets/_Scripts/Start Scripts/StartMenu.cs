using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour 
{
	public Sprite[] startButtons;

	GameObject[] buttons = new GameObject[3];

	public bool selected = false;
	int selectedOption = 0;

	// Use this for initialization
	void Start () {
		buttons[0] = GameObject.Find("Start");
		buttons[1] = GameObject.Find("Options");
		buttons[2] = GameObject.Find("Exit");
	}
	
	// Update is called once per frame
	void Update () {
		buttons[0].GetComponent<SpriteRenderer>().sprite = startButtons[0];
		buttons[1].GetComponent<SpriteRenderer>().sprite = startButtons[2];
		buttons[2].GetComponent<SpriteRenderer>().sprite = startButtons[4];

		if(selectedOption == 0)
			buttons[0].GetComponent<SpriteRenderer>().sprite = startButtons[1];
		else if(selectedOption == 1)
			buttons[1].GetComponent<SpriteRenderer>().sprite = startButtons[3];
		else if(selectedOption == 2)
			buttons[2].GetComponent<SpriteRenderer>().sprite = startButtons[5];

		if(!selected)
			userControl();
	}

	void userControl()
	{
		if((Input.GetKeyDown(KeyCode.UpArrow) && selectedOption > 0))
			selectedOption -= 1;
		else if((Input.GetKeyDown(KeyCode.DownArrow) && selectedOption < 2))
			selectedOption += 1;

		if(Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
		{
			switch(selectedOption)
			{
			case 0:
				Application.LoadLevel("01_Lab");
				break;
			case 1:
				selected = true;
				GameObject.Find("OptionsPanel").GetComponent<OptionMenu>().showMenu = true;
				break;
			case 2:
				Application.Quit();
				break;
			}
		}
	}
}
