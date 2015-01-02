using UnityEngine;
using System.Collections;

public class ChangeRoomScript : MonoBehaviour {
	
	globalObject go;

	public string playRoom = "";
	public string currentRoom;

	private Vector2 shift = new Vector2(0.35f, -1.925085f);

	// Use this for initialization
	void Start () {
		currentRoom = Application.loadedLevelName;

		if(GameObject.Find("GlobalObject") != null)
			go = GameObject.Find("GlobalObject").GetComponent<globalObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay2D(Collider2D coll) 
	{
		if (coll.gameObject.tag == "Player")
		{
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				Application.LoadLevel(playRoom);
				go.playerLocation = playerLocation(playRoom);
			}
		}
	}
	
	
	Vector3 playerLocation(string cRoom)
	{
		switch(cRoom)
		{
		case "00_Tutorial2":
			go.playerFaceRight = true;
			return new Vector3(-6.9f + shift.x, 4.475594f + shift.y, 0f);

		case "01_Lab":
			go.playerFaceRight = true;
			return new Vector3(-7.2f,  1.6f + shift.y, 0f);
//			break;
			
		case "02_LabCommon":
			if(currentRoom == "01_Lab")
			{	
				go.playerFaceRight = true;
				return new Vector3(-6.9f + shift.x, 4.475594f + shift.y, 0f);
			}
			else if(currentRoom == "03_LabStorage")
			{	
				go.playerFaceRight = true;
				return new Vector3(-6.9f + shift.x, -1.28f + shift.y, 0f);
			}
			else if(currentRoom == "04_Cave01")
			{	
				go.playerFaceRight = false;
				return new Vector3(6.236631f + shift.x, -1.28f + shift.y, 0f);
			}
			break;
			
		case "03_LabStorage":
				go.playerFaceRight = false;
				return new Vector3(4.638611f + shift.x, 1.6f + shift.y, 0f);
//			break;
			
		case "04_Cave01":
			if(currentRoom == "02_LabCommon")
			{
				go.playerFaceRight = true;
				return new Vector3(-12.47999f + shift.x, 1.28f + shift.y, 0f);
			}
			else if(currentRoom == "05_Cave02")
			{	
				go.playerFaceRight = false;
				return new Vector3(11.84f + shift.x, 1.28f + shift.y, 0f);
			}
			break;
			
		case "05_Cave02":
			if(currentRoom == "04_Cave01")
			{
				go.playerFaceRight = true;
				return new Vector3(-12.48f + shift.x, 1.279999f + shift.y, 0f);
			}
			else if(currentRoom == "05_Ruins")
			{
				go.playerFaceRight = true;
				return new Vector3(-4.479999f + shift.x, 2.66271f + shift.y, 0f);
			}
			else if(currentRoom == "06_Cave03")
			{
				go.playerFaceRight = true;
				return new Vector3(11.83996f + shift.x, 1.28f + shift.y, 0f);
			}
			break;

		case "05_Ruins":
			go.playerFaceRight = true;
			return new Vector3(-6.08f + shift.x, 0.6399993f + shift.y, 0f);
//			break;
		}

/*		case "06_Cave03":
		{
			go.playerFaceRight = true;
			return new Vector3(-6.08f + shift.x, 0.6399993f + shift.y, 0f);
		}
*/		
		return Vector3.zero;
	}
}
