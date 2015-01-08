﻿using UnityEngine;
using System.Collections;

public class MapLocation : MonoBehaviour {
	
	public string name = "";
	public Texture2D locationSprite;
	public int locationNumber = 0;
	public int roomType = 0;
	public bool hasBeenFound = true;
	public bool isCurrentLocation = false;
	public bool isSelected = false;
	public MapLocation[] connectedTo;

	public int xLocation=0;
	public int yLocation=0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
