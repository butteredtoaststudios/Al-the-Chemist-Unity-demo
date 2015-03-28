using UnityEngine;
using System.Collections;

public class FallingRockClass : MonoBehaviour {

	private GameObject player;
	private float rockFallSpeed = -0.05f;
	public bool startRockFall = false;
	private bool stopRockFall = false;
	private bool isRockOnTopGround = true;
	private bool hasHitPlayer = false;
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag("Player");

	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player != null)
		{
			if(isAlUnderRock() && !stopRockFall)
				startRockFall = true;

			if(startRockFall)
				rockIsFalling();
		}
		else
			player = GameObject.FindWithTag("Player");
	}


	private bool isAlUnderRock()
	{
		if(player.transform.position.x > this.gameObject.transform.position.x)
		{
			return true;
		}

		return false;
	}

	private void rockIsFalling()
	{
		Vector3 cPosition = this.gameObject.transform.position;
		this.gameObject.transform.position = new Vector3(cPosition.x, cPosition.y + rockFallSpeed, cPosition.z); 
	}

	public void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Ground" && !isRockOnTopGround)
		{	
			startRockFall = false;
			stopRockFall = true;
			hasHitPlayer = true;
		}
		if(coll.gameObject.tag == "Player" && !hasHitPlayer)
		{
			player.GetComponent<AlScript>().AlIsHit();
			hasHitPlayer = true;
		}
	}
	public void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Ground")
		{	
			isRockOnTopGround = false;
		}
	}
}
