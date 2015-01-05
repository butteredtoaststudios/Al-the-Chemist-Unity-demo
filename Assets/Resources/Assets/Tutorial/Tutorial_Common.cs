using UnityEngine;
using System.Collections;

public class Tutorial_Common : MonoBehaviour {

	public GUISkin skin;

	private GameObject Al;
	private GameObject Global;

	private bool showInstruction = false;
	private string instructText = "";

	private bool[] scriptActive = new bool[]{true, false, false, false, false, false};

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
