using UnityEngine;
using System.Collections;

public class ChangeRoomScript : MonoBehaviour {

	public string playRoom = "";
	// Use this for initialization
	void Start () {
	
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
			}
		}
	}
}
