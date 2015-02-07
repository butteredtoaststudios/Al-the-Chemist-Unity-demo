using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(GameObject.FindGameObjectWithTag("Player"));
	}
	
	// Update is called once per frame
	void Update () {
		if(GameObject.FindGameObjectWithTag("Player") != null)
		{	
			Destroy(GameObject.FindGameObjectWithTag("Player"));
			Destroy(GameObject.Find("GlobalObject"));
		}

		if(Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Return))
		{
			Application.LoadLevel("StartLevel");
		}
	}
}
