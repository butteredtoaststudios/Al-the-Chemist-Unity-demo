
using UnityEngine;
using System.Collections;

public class AlScript : MonoBehaviour {

	public GUISkin alGUI;
	public float moveSpeed = 0.04f;
	public float imageSpeed = 0.2f;
	public float climbSpeed = 0.01f;
	public float maxJumpHeight = 1.25f;

	public Sprite AlIdle;
	public Sprite[] AlSprite;
	public Sprite[] AlClimb;
	public SpriteRenderer spriteRender;

	private float time;
	private int imageIndex = 0;
	private bool isJumping = false;
	private bool onGround = true;
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

	bool hitLeftRight = false;

	globalObject go;

	private bool updateImageReverse = false;

	// Use this for initialization
	void Start () {

		go = GameObject.Find("GlobalObject").GetComponent<globalObject>();
	}
	
	// Update is called once per frame
	void Update () 
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

		updateImage(updateImageReverse);
	}

	private void onWalkKey()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - moveSpeed > 0)//Camera.main.GetComponent<SmoothFollow2D>())
				gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
			go.playerFaceRight = false;

			spriteRender.sprite = AlSprite[(imageIndex + 1) % AlSprite.Length];
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - moveSpeed < Screen.width)
				gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
			go.playerFaceRight = true;

			spriteRender.sprite = AlSprite[(imageIndex + 1) % AlSprite.Length];
		}
		else
		{
			spriteRender.sprite = AlIdle;
		}

		if(go.playerFaceRight)
			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
		else
			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 180, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
	}

	private void onJumpKey()
	{
		if(Input.GetKeyDown(KeyCode.Space) && !isJumping && onGround)
		{
			isJumping = true;
		}
		if(Input.GetKeyUp(KeyCode.Space))
		{
			isJumping = false;
		}
		
		if(Input.GetKey(KeyCode.Space) && isJumping)
		{
			if(onGround)
			{
				startJumpPosition = gameObject.transform.position;
				onGround = false;
			}
			
			if(gameObject.transform.position.y < (startJumpPosition.y + maxJumpHeight))
				gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + moveSpeed, gameObject.transform.position .z);
			else 
				isJumping = false;
			
		}

		else if(!onGround)
		{
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - moveSpeed, gameObject.transform.position .z);
		}
	}

	private void onClimbKey()
	{
		if(Input.GetKey(KeyCode.DownArrow))
		{
			gameObject.transform.position = new Vector3(startClimbPosition.x - 0.089f, gameObject.transform.position.y - climbSpeed, startClimbPosition.z);
			spriteRender.sprite = AlClimb[imageIndex%4 + 2];
		}

		if(Input.GetKey(KeyCode.UpArrow))
		{
			gameObject.transform.position = new Vector3(startClimbPosition.x - 0.089f, gameObject.transform.position.y + climbSpeed, startClimbPosition.z);
			spriteRender.sprite = AlClimb[imageIndex%4 + 2];
		}
	}

	private void climbStartClimbFunc()
	{
		spriteRender.sprite = AlClimb[imageIndex];

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
			spriteRender.sprite = AlClimb[imageIndex];

		if(imageIndex == 1)
		{	
			spriteRender.sprite = AlClimb[imageIndex];
			gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.365349f - 0.090f, startClimbPosition.z);
		}
		else if(imageIndex == 0)
		{	
			spriteRender.sprite = AlClimb[imageIndex];
			gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.090f, startClimbPosition.z);
		}
		else if(imageIndex < 0)
		{
			spriteRender.sprite = AlIdle;

			gameObject.transform.position = new Vector3(gameObject.transform.position.x, startClimbPosition.y + 0.4f, startClimbPosition.z);

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



	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.renderer != null)
		{
			if(AlColliderRight.bounds.Intersects(coll.gameObject.renderer.bounds))
			{	if(coll.gameObject.tag == "Ground")
					gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed + 0.01f, gameObject.transform.position .y, gameObject.transform.position .z);
				hitLeftRight = true;
			}
			if(AlColliderLeft.bounds.Intersects(coll.gameObject.renderer.bounds))
			{	if(coll.gameObject.tag == "Ground")
					gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed + 0.01f, gameObject.transform.position .y, gameObject.transform.position .z);
				hitLeftRight = true;
			}
		}

		if(coll.gameObject.tag =="LadderBottom")
			climbBottom = true;
		
		if (coll.gameObject.tag == "Ground")// && !hitLeftRight)
		{	
			onGround = true;
			
			if(climbBottom)
			{	
				isClimb = false;
			}
		}

		hitLeftRight = false;
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.renderer != null)
		{
			if(AlColliderRight.bounds.Intersects(coll.gameObject.renderer.bounds))
				if(coll.gameObject.tag == "Ground")
						gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveSpeed - 0.01f, gameObject.transform.position .y, gameObject.transform.position .z);
			if(AlColliderLeft.bounds.Intersects(coll.gameObject.renderer.bounds))
				if(coll.gameObject.tag == "Ground")
					gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed + 0.01f, gameObject.transform.position .y, gameObject.transform.position .z);
		
			if(AlColliderTop.bounds.Intersects(coll.gameObject.renderer.bounds))
			{
			}
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
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if (coll.gameObject.tag == "Ground")
			onGround = false;
	}
}
