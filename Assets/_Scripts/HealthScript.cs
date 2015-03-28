using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public Sprite[] health;
	globalObject go;

	// Use this for initialization
	void Start () {
		go = GameObject.Find("GlobalObject").GetComponent<globalObject>();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void OnGUI()
	{
		if(health.Length > 0 && go.playerHealth > 0)
			GUI.Box(new Rect(20, 20, health[0].texture.width,  health[0].texture.height), health[go.playerHealth-1].texture, "");
	}
}
