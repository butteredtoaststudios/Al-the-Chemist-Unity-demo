
using UnityEngine;
using System.Collections;

public class AlScript : MonoBehaviour {

	public GUISkin alGUI;
	public bool freezeAl = false;
	public float moveSpeed = 0.04f;
	private float jumpSpeed = 0.06f;
	public float imageSpeed = 0.2f;
	public float climbSpeed = 0.01f;
	public float maxJumpHeight = 1.25f;

	public Sprite AlIdle;
	public AlSpriteList AlSprites;
	public SpriteRenderer spriteRender;

	private float time;
	private int imageIndex = 0;
	private bool isJumping = false;
	private bool onGround = false;
	private Vector3 startJumpPosition = Vector3.zero;

	private bool isClimb = false;
	private bool climbStart = false;
	private bool climbBottom = false;
	private bool climbReverse = false;
	private Vector3 startClimbPosition = Vector3.zero;

	public BoxCollider2D AlColliderBox;
	public BoxCollider2D AlColliderRight;
	public BoxCollider2D AlColliderLeft;
	public BoxCollider2D AlColliderBottom;
	public BoxCollider2D AlColliderTop;
	
	globalObject go;
	Inventory AlInventory;

	private bool updateImageReverse = false;

	// Use this for initialization
	void Start () {

		go = GameObject.Find("GlobalObject").GetComponent<globalObject>();
		AlSprites = gameObject.GetComponent<AlSpriteList>();

		AlInventory = go.GetComponent<Inventory>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!freezeAl)
		{
			if(!isClimb && !climbReverse)
			{
				onWalkKey();
				onJumpKey();
			}

			if(climbReverse)
				climbRevFunc();

			if(isClimb && !climbStart)
			{
				climbStartClimbFunc();
			}
			if(isClimb && (climbStart || climbBottom))
				onClimbKey();
		}
		updateImage(updateImageReverse);
	}

	private void onWalkKey()
	{
		if(Input.GetKey(KeyList.keyRight))
		{
			if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - moveSpeed > 0)//Camera.main.GetComponent<SmoothFollow2D>())
				gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
			go.playerFaceRight = false;

			AlSprites.playAnimation("walk");
		}
		else if(Input.GetKey(KeyList.keyLeft))
		{
			if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - moveSpeed < Screen.width)
				gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
			go.playerFaceRight = true;

			AlSprites.playAnimation("walk");
		}
		else
		{
			AlSprites.playAnimation("idle");
		}

		if(go.playerFaceRight)
			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
		else
			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 180, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
	}

	/*
	float vy_0;
	float timeTotal = 0;

	private float timeSpeed = 0.016f;
	private float gravity = 16.4f;
	private float height = 0.14f;
	private float y = 0;
	private void onJumpKey()
	{
		if(Input.GetKeyDown(KeyList.keyJump) && !isJumping && onGround)
		{
			onGround = false;
			isJumping = true;
			timeTotal = 0.0f;
		}
		if(Input.GetKeyUp(KeyList.keyJump) && !onGround)
		{
			timeTotal = 0;
			vy_0 = 0;
			gravity = 16.4f;
			isJumping = false;
		}

		if(Input.GetKey(KeyList.keyJump) && isJumping)
		{
			//timeTotal = 0;
			gravity = 16.4f;
			vy_0 = Mathf.Sqrt(height * 2 * gravity);
		}

		if(onGround && !isJumping)
		{
			timeTotal = 0;
			gravity = 0;
			vy_0 = 0;
		}

		timeTotal += timeSpeed;	
		y = (vy_0 * timeTotal) - 0.5f*(gravity * (timeTotal* timeTotal));
	
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + y, gameObject.transform.position .z);
	}
	*/
	
	private void onJumpKey()
	{
		if(Input.GetKeyDown(KeyCode.Space) && !isJumping && onGround)
		{
			isJumping = true;
			AlSprites.playAnimation("jump");
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			isJumping = false;
		}
		
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
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - jumpSpeed, gameObject.transform.position .z);
		}
	}

	private void onClimbKey()
	{
		if(Input.GetKey(KeyCode.DownArrow))
		{
			gameObject.transform.position = new Vector3(startClimbPosition.x - 0.089f, gameObject.transform.position.y - climbSpeed, startClimbPosition.z);
			spriteRender.sprite = AlSprites.AlClimb[imageIndex%4 + 2]; //AlClimb[imageIndex%4 + 2];
		}

		if(Input.GetKey(KeyCode.UpArrow))
		{
			gameObject.transform.position = new Vector3(startClimbPosition.x - 0.089f, gameObject.transform.position.y + climbSpeed, startClimbPosition.z);
			spriteRender.sprite = AlSprites.AlClimb[imageIndex%4 + 2];
		}
	}

	private void climbStartClimbFunc()
	{
		spriteRender.sprite = AlSprites.AlClimb[imageIndex]; //AlClimb[imageIndex];

		if(imageIndex == 0)
			gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.090f, startClimbPosition.z);
		else if(imageIndex == 1)
			gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.365349f - 0.090f, startClimbPosition.z);
		else if(imageIndex == 2)
			gameObject.transform.position = new Vector3(startClimbPosition.x - 0.089f, startClimbPosition.y - 0.665349f, startClimbPosition.z);

		if(imageIndex == 2)
		{
			imageIndex = 0;
			climbStart = true;
		}
	}

	private void climbRevFunc()
	{
		if(imageIndex > 0)
			spriteRender.sprite = AlSprites.AlClimb[imageIndex]; 

		if(imageIndex == 1)
		{	
			spriteRender.sprite = AlSprites.AlClimb[imageIndex]; 
			gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.365349f - 0.090f, startClimbPosition.z);
		}
		else if(imageIndex == 0)
		{	
			spriteRender.sprite = AlSprites.AlClimb[imageIndex]; 
			gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.090f, startClimbPosition.z);
		}
		else if(imageIndex < 0)
		{
			spriteRender.sprite = AlIdle;

			gameObject.transform.position = new Vector3(gameObject.transform.position.x, startClimbPosition.y + 0.3f, startClimbPosition.z);

			climbReverse = false;
			climbStart = false;
			climbBottom = false;

			updateImageReverse = false;
		}
	}


	void updateImage(bool isReverse = false)
	{
		if(time > imageSpeed)
		{
			time = 0;
			if(!isReverse)
				imageIndex = (imageIndex + 1);
			else
				imageIndex = (imageIndex - 1);
		}
		
		time += Time.deltaTime;
	}

	//Collision objects response 
	//Ground
	//Rocks
	//Slopes
	//Crates
	//Doors

	void stopFall(Collider2D coll, string groundObj)
	{
		if(coll.gameObject.tag == groundObj)
			onGround = true;
	}

	/*
	void OnColliderEnter2D(Collider2D coll)
	{
		Debug.Log("collider");
		if(coll.gameObject.tag == "Ground")
			onGround = true;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log("trigger");
		if(coll.gameObject.tag == "Ground")
		{	
			timeTotal = 0;
			gravity = 0;
			vy_0 = 0;
			onGround = true;
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Ground")
		{	
			onGround = true;
		}
	}


	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag == "Ground")
		{	
			onGround = false;
		}
	}*/


	void OnTriggerEnter2D(Collider2D coll)
	{
		if (coll.gameObject.renderer != null) 
		{
			if(coll.gameObject.tag == "Ground")
				groundCollision (coll);
		}

		if(climbBottom)
			isClimb = false;

		if(coll.gameObject.tag == "Elements")
			selectElements(coll.gameObject);
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
			if(coll.gameObject.tag == "Ground")
				groundCollision (coll);
		}

		if(coll.gameObject.tag =="LadderTop")
		{
			if(Input.GetKeyDown(KeyCode.DownArrow) && !isClimb)
			{	
				imageIndex = 0;
				isClimb = true;
				startClimbPosition = coll.gameObject.transform.position;
				gameObject.transform.position = coll.gameObject.transform.position;
			}
			
			if(Input.GetKey(KeyCode.UpArrow))
			{	
				startClimbPosition = coll.gameObject.transform.position;
				climbReverse = true;
				updateImageReverse = true;
				imageIndex = 2;
				isClimb = false;
			}
		}
		
		if(coll.gameObject.tag =="LadderBottom")
		{	if(Input.GetKey(KeyCode.UpArrow) && !isClimb)
			{	
				startClimbPosition = coll.gameObject.transform.position;
				climbStart = true;
				isClimb = true;
				climbBottom = false;
			}
		}

		if(coll.gameObject.tag == "Elements")
		{
			selectElements(coll.gameObject);
		}
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
