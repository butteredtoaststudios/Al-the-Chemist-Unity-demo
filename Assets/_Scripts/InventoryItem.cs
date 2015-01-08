using UnityEngine;
using System.Collections;

public class InventoryItem : ScriptableObject {
	
	public string name = "";
	public Texture2D[] itemSprite = new Texture2D[2];
	private int amount = 0;
	public string description = "";
	public bool hasBeenFound = false;
	public bool isSelected = false;
	
	public int _amount
	{
		get{ return amount; }
		set{ amount = value; }
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
