using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour 
{
	public Sprite[] startButtons;

	GameObject[] buttons = new GameObject[3];
	
	public bool selected = false;
	int selectedOption = 0;

	public Sprite logo;
	private bool showLogo = true;

	// Use this for initialization
	void Start () {
		Screen.SetResolution(720, 480, false); 

		buttons[0] = GameObject.Find("Start");
		buttons[1] = GameObject.Find("Options");
		buttons[2] = GameObject.Find("Exit");
	}
	
	// Update is called once per frame
	void Update () {
		if(!showLogo)
		{
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
		else
		{
			StartCoroutine("logoTime");
		}
	}

	public IEnumerator logoTime()
	{
		yield return new WaitForSeconds(2);

		_alpha -= 0.05f;

		if(_alpha <= 0.01)
			showLogo = false;
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
				Application.LoadLevel("00_Tutorial");
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

	private float _alpha = 1.0f;

	void OnGUI()
	{
		GUI.color = new Color(1, 1, 1, _alpha);

		if(showLogo)
			GUI.Box(new Rect(0, 0, 720, 480), logo.texture, "");
	}
}
