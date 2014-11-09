using UnityEngine;
using System.Collections;

public class MapLocation : MonoBehaviour {
	
	public string name = "";
	public Texture2D locationSprite;
	public int locationNumber = 0;
	public int roomType = 0;
	public bool hasBeenFound = true;
	public bool isCurrentLocation = false;
	public bool isSelected = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
