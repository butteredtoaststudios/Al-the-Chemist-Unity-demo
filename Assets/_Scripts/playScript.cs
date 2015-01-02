using UnityEngine;
using System.Collections;

public static class playScript {

	public static bool loadScript = false;
	public static int textInt = -1;
	private static bool nextLine = true;
	private static string state = "";

	public static void startScript(string script, bool clickStart = true)
	{
		globalObject go = GameObject.Find ("GlobalObject").GetComponent<globalObject>();

		ArrayList scriptArray = new fileHandler ().Load (script);

		if (textInt < scriptArray.Count) {	

			if(!loadScript)
			{
				loadScript = true;
				go.diag._showDiag = true;
				nextLine = true;
				if(clickStart)
					textInt = -1;
				else
				{
					textInt = 0;
					updateDialogue(go, scriptArray);
				}
	//			go.diag._diagText = (scriptArray [0] as string[]) [1];
			}

			if (Input.GetKeyDown (KeyCode.X) && nextLine) 
			{
				nextLine = false;
				textInt = textInt + 1;

				state = state.Trim();

				if(state == "END")
				{	
					go.diag._showDiag = false;
					nextLine = true;
					textInt = -1;
				}
				else
				{	
					go.diag._showDiag = true;
					string img = (scriptArray [textInt] as string[]) [0];
					string diag = (scriptArray [textInt] as string[]) [1];
					state = (scriptArray [textInt] as string[]) [2];
					
					go.diag._diagText = diag;
					
					go.diag.updatePortrait(int.Parse(img));
				}
			} 
			else if (Input.GetKeyUp (KeyList.keyInteract))
			{
				nextLine = true;
			}
		}
	}

	public static void updateDialogue(globalObject go, ArrayList scriptArray)
	{
		go.diag._showDiag = true;
		string img = (scriptArray [textInt] as string[]) [0];
		string diag = (scriptArray [textInt] as string[]) [1];
		state = (scriptArray [textInt] as string[]) [2];
		
		go.diag._diagText = diag;
		
		go.diag.updatePortrait(int.Parse(img));
	}
}
