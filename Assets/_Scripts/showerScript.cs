using UnityEngine;
using System.Collections;

public class showerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Set the sorting layer of the particle system.
		particleSystem.renderer.sortingLayerName = "Default";
		particleSystem.renderer.sortingOrder = 1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
