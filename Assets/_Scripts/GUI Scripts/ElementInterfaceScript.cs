using UnityEngine;
using System.Collections;

public class ElementInterfaceScript : MonoBehaviour {

	public Sprite elementInter;
	public GUISkin eleGUI;
	public InventoryDatabase ID;

	private float width;
	private float height;

	private float boxHeight = 64;
	private string fontS = "guiFont";

	// Use this for initialization
	void Start () {
		width = elementInter.texture.width;
		height = elementInter.texture.height;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{  
		GUI.skin = eleGUI;

		GUI.Box(new Rect(Screen.width - width, Screen.height - height, width,  height), elementInter.texture, "");

		//H
		GUI.Box(new Rect(Screen.width - 325, Screen.height - boxHeight, 30,  30), ID.inventoryData[2,0]._amount.ToString(), fontS);

		//O
		GUI.Box(new Rect(Screen.width - 282, Screen.height - boxHeight, 30,  30), ID.inventoryData[1,0]._amount.ToString(), fontS);

		//C
		GUI.Box(new Rect(Screen.width - 240, Screen.height - boxHeight, 30,  30), ID.inventoryData[0,0]._amount.ToString(), fontS);

		//N
		GUI.Box(new Rect(Screen.width - 198, Screen.height - boxHeight, 30,  30), ID.inventoryData[0,1]._amount.ToString(), fontS);

		//Ph
		GUI.Box(new Rect(Screen.width - 156, Screen.height - boxHeight, 30,  30), ID.inventoryData[2,1]._amount.ToString(), fontS);

		//S
		GUI.Box(new Rect(Screen.width - 114, Screen.height - boxHeight, 30,  30), ID.inventoryData[1,1]._amount.ToString(), fontS);

		//equip
		GUI.Box(new Rect(Screen.width - 65, Screen.height - boxHeight - 7, 50,  50), "0", "");
	}


	void onEquipGUI()
	{

	}
}
