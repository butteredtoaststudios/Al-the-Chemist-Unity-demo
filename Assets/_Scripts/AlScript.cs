
using UnityEngine;
using System.Collections;

public class AlScript : MonoBehaviour {

	public GUISkin alGUI;
	public bool freezeAl = false;
	public float moveSpeed = 0.04f;
	private float jumpSpeed = 0.06f;
	public float climbSpeed = 0.01f;
	public float maxJumpHeight = 1.25f;

	private Animator animate;

	private bool isJumping = false;
	private bool onGround = false;
	private bool onLadder = false;
	private Vector3 startJumpPosition = Vector3.zero;

	private bool isClimb = false;
	private bool climbKeys = false;
	private bool ladderBottom = false;

	private Vector3 startClimbPosition = Vector3.zero;

	public BoxCollider2D AlColliderBox;
	public BoxCollider2D AlColliderRight;
	public BoxCollider2D AlColliderLeft;
	public BoxCollider2D AlColliderBottom;
	public BoxCollider2D AlColliderTop;
	
	globalObject go;
	Inventory AlInventory;

	// Use this for initialization
	void Start () 
	{
		go = GameObject.Find("GlobalObject").GetComponent<globalObject>();
		AlInventory = go.GetComponent<Inventory>();
		animate = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!freezeAl)
		{
			if(!isClimb)
			{
				onWalkKey();
				onJumpKey();

				animate.SetBool("ground", onGround);
			}
			else
			{
				animate.SetBool("walk", false);
				animate.SetBool("ground", true);
			}

			if(climbKeys)
				onClimbKey();
		}
	}

	private void onWalkKey()
	{
		if(Input.GetKey(KeyList.keyRight))
		{
			if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - moveSpeed > 0)//Camera.main.GetComponent<SmoothFollow2D>())
				gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
			go.playerFaceRight = false;

			animate.SetBool("walk", true);
		}
		else if(Input.GetKey(KeyList.keyLeft))
		{
			if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - moveSpeed < Screen.width)
				gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
			go.playerFaceRight = true;

			animate.SetBool("walk", true);
		}
		else
			animate.SetBool("walk", false);

		if(go.playerFaceRight)
			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
		else
			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 180, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
	}
	
	private void onJumpKey()
	{
		if(Input.GetKeyDown(KeyList.keyJump) && !isJumping && onGround)
			isJumping = true;
	
		if(Input.GetKeyUp(KeyList.keyJump))
			isJumping = false;

		if(Input.GetKey(KeyList.keyJump) && isJumping)
		{
			if(onGround)
			{
				startJumpPosition = gameObject.transform.position;
				onGround = false;
			}

			if(gameObject.transform.position.y < (startJumpPosition.y + maxJumpHeight))
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + jumpSpeed, gameObject.transform.position .z);
			else 
				isJumping = false;
		}

		else if(!onGround)
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - jumpSpeed, gameObject.transform.position .z);

	}

	private void onClimbKey()
	{
		animate.SetBool("climbLadder", true);
		if(Input.GetKey(KeyList.keyDown))
		{	
			gameObject.transform.position = new Vector3(startClimbPosition.x - 0.089f, gameObject.transform.position.y - climbSpeed, startClimbPosition.z);
			animate.speed = 1;
		}
		else if(Input.GetKey(KeyList.keyUp))
		{	
			gameObject.transform.position = new Vector3(startClimbPosition.x - 0.089f, gameObject.transform.position.y + climbSpeed, startClimbPosition.z);
			animate.speed = 1;
		}
		else
		{
			animate.speed = 0;
		}
	}

	private void climbStartClimbFunc(int frame)
	{
		switch(frame)
		{
		case -1:
			animate.SetBool("climbStart", false);
			animate.SetBool("climbReverse", false);
			animate.SetBool("climbLadder", false);

			climbKeys = false;
			animate.speed = 1;
			onLadder = false;
			isClimb = false;
			break;
		case 0:
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, startClimbPosition.y + 0.31f, startClimbPosition.z);
			break;
		case 1:
			gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.090f, startClimbPosition.z);
			break;
		case 2:
			gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.46f, startClimbPosition.z);
			break;
		case 3:
			climbKeys = true;
			gameObject.transform.position = new Vector3(startClimbPosition.x - 0.089f, startClimbPosition.y - 0.67f, startClimbPosition.z);
			break;
		}

	}

	//Collision objects response 
	//Ground
	//Rocks
	//Slopes
	//Crates
	//Doors

	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log("tag " + coll.gameObject.tag);

		if (coll.gameObject.renderer != null) 
			if(coll.gameObject.tag == "Ground" && !onLadder)
				groundCollision (coll);

		if(coll.gameObject.tag == "Elements")
			selectElements(coll.gameObject);

		if(coll.gameObject.tag == "DeathSquare")
		{	
			Application.LoadLevel("GameOver");
		}
	}

	void groundCollision(Collider2D coll)
	{
		if(AlColliderTop.bounds.Intersects(coll.gameObject.GetComponent<Collider2D>().bounds))
		{
			onGround = false;
			isJumping = false;
		}

		if(AlColliderRight.bounds.Intersects(coll.gameObject.GetComponent<Collider2D>().bounds))
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveSpeed - 0.01f, gameObject.transform.position .y, gameObject.transform.position .z);
		}

		if(AlColliderLeft.bounds.Intersects(coll.gameObject.GetComponent<Collider2D>().bounds))
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed + 0.01f, gameObject.transform.position .y, gameObject.transform.position .z);
		}

		if(AlColliderBox.bounds.Intersects(coll.gameObject.GetComponent<Collider2D>().bounds))
		{
			gameObject.transform.position= new Vector3(gameObject.transform.position.x, coll.gameObject.transform.position.y + 0.64f, gameObject.transform.position.z);
			onGround = true;
		}

		else if(AlColliderBottom.bounds.Intersects(coll.gameObject.GetComponent<Collider2D>().bounds))
		{
			gameObject.transform.position= new Vector3(gameObject.transform.position.x, coll.gameObject.transform.position.y + 0.64f, gameObject.transform.position.z);
			onGround = true;
		}

	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if (coll.gameObject.renderer != null) 
		{
			if(coll.gameObject.tag == "Ground" && !onLadder)
				groundCollision (coll);

			if(ladderBottom)
			{	
				climbStartClimbFunc(-1);
			}
		}

		if(coll.gameObject.tag == "LadderTop")
		{
			startClimbPosition = coll.gameObject.transform.position;

			if(Input.GetKeyDown(KeyList.keyDown) && !isClimb)
			{	
				isClimb = true;
				onLadder = true;
				animate.SetBool("climbStart", true);
			}
			if(Input.GetKey(KeyList.keyUp) && isClimb)
			{	
				climbKeys = false;
				animate.SetBool("climbLadder", false);

				animate.speed = 1;
				animate.SetBool("climbReverse", true);
			}
		}

		if(coll.gameObject.tag =="LadderBottom")
		{	
			if(Input.GetKey(KeyList.keyUp) && !isClimb)
			{
				isClimb = true;
				onLadder = true;
				climbKeys = true;
				ladderBottom = false;
			}

			if(Input.GetKey(KeyList.keyDown) && isClimb)
				ladderBottom = true;
		}


		if(coll.gameObject.tag == "Elements")
			selectElements(coll.gameObject);
	}
	
	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.renderer != null) 
		{
			if(coll.gameObject.tag == "Ground")
			{		
				if (!AlColliderBottom.bounds.Intersects (coll.gameObject.renderer.bounds)) 
					onGround = false;
				else 
					onGround = true;
			}	
		}
	}

	bool itemWait = false;

	void selectElements(GameObject go)
	{
		if(Input.GetKeyUp(KeyList.keyInteract))
			itemWait = false;

		if(Input.GetKeyDown(KeyList.keyInteract))
		{
			go.SetActive(false);

			switch(go.name)
			{
			case "carbon_small":
				AlInventory.inventoryData[0,0]._amount += 1;
				break;
			case "hydrogen_small":
				AlInventory.inventoryData[2,0]._amount += 1;
				break;
			case "oxygen_small":
				AlInventory.inventoryData[1,0]._amount += 1;
				break;
			case "":
				break;
			}
		}
	}

	public bool _groundState
	{
		get{return onGround;}
		set{onGround = value;}
	}
}
