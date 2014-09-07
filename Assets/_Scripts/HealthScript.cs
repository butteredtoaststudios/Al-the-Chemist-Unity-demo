using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public Sprite[] health;
	public AlScript player;

	private int playHealth = 5;

	// Use this for initialization
	void Start () {
	
		player = GameObject.Find("Al").GetComponent<AlScript>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player != null)
			playHealth = player._Health;
	}
	
	void OnGUI()
	{
		if(health.Length > 0)
			GUI.Box(new Rect(20, 20, health[0].texture.width,  health[0].texture.height), health[playHealth-1].texture, "");
	}
}
