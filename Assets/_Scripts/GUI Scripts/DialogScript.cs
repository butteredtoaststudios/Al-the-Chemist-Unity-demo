using UnityEngine;
using System.Collections;

public class DialogScript : MonoBehaviour {

	public Sprite[] dialogSprites;

	public Sprite[] pointer;

	private float time;
	private int imageIndex = 0;
	public float imageSpeed = 0.07f;

	private bool showDiag = false;
	private string diagText = "TEST TEST TEST TEST. ";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		updateImage();
	
	}

	public bool _showDiag
	{
		get{return showDiag;}
		set{showDiag = value;}
	}

	public string _diagText
	{
		get{return diagText;}
		set{diagText = value;}
	}

	void OnGUI()
	{
		if(_showDiag)
			GUI.Window(5, new Rect(0, Screen.height - dialogSprites[0].texture.height, dialogSprites[0].texture.width, dialogSprites[0].texture.height), diagWindow, "");
	}

	void diagWindow(int winID)
	{
		GUI.Box(new Rect(0, 0, dialogSprites[0].texture.width, dialogSprites[0].texture.height), dialogSprites[0].texture, "");

		GUI.Box(new Rect(22, 21, 64, 64), dialogSprites[1].texture, "");
		GUI.Box(new Rect(16, 15, 76, 76), dialogSprites[2].texture, "");
		
		GUI.Box(new Rect(122, 23, 76, 76), diagText, "");
		
		
		GUI.Box(new Rect(Screen.width - 31, 60, 16, 32), pointer[(imageIndex + 1) % pointer.Length].texture, "");
	}

	void updateImage()
	{
		if(time > imageSpeed)
		{
			time = 0;
			
			imageIndex = (imageIndex + 1);
		}
		
		time += Time.deltaTime;
	}
}
