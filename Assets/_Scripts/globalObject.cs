using UnityEngine;
using System.Collections;

public class globalObject : MonoBehaviour 
{
	public GameObject player;
	public Vector3 playerLocation;
	public int playerHealth = 5;
	public bool playerFaceRight = true;

	public DialogScript diag;

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(transform.gameObject);
		diag = gameObject.GetComponent<DialogScript> ();

		if(player == null)
			player = Instantiate(Resources.Load("_Prefab/Al"), playerLocation, Quaternion.identity) as GameObject;

		Camera.main.GetComponent<SmoothFollow>().target = player.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player == null)
			player = Instantiate(Resources.Load("_Prefab/Al"), playerLocation, Quaternion.identity) as GameObject;
	}

	public int _Health
	{
		get{return playerHealth;}
		set{playerHealth = value;}
	}
}
