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
	private bool climbReady = false;
	private Vector3 startClimbPosition = Vector3.zero;

	private BoxCollider2D alCollider;

	private int playerHealth = 5;

	void Awake() {
		DontDestroyOnLoad(Camera.main);
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start () {
		alCollider = gameObject.GetComponent<BoxCollider2D>();
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!isClimb)
		{
			onWalkKey();
			onJumpKey();
		}
		else
			initClimb();

		if(climbReady)
			onClimbKey();

		updateImage();
	}

	private void onWalkKey()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			if(gameObject.transform.position.x - moveSpeed > 0.0f)
				gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);

			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 180, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
			updateImage();
			spriteRender.sprite = AlSprite[(imageIndex + 1) % AlSprite.Length];
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - moveSpeed < Screen.width)
				gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
			updateImage();
			spriteRender.sprite = AlSprite[(imageIndex + 1) % AlSprite.Length];
		}
		else
		{
			spriteRender.sprite = AlIdle;
		}
	}
	
	private void onJumpKey()
	{
		if(Input.GetKeyDown(KeyCode.Space) && !isJumping && onGround)
			isJumping = true;

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
			gameObject.transform.position = new Vector3(5.35914f, gameObject.transform.position.y - climbSpeed, startClimbPosition.z);
			spriteRender.sprite = AlClimb[imageIndex%4 + 2];
		}

		if(Input.GetKey(KeyCode.UpArrow))
		{
			gameObject.transform.position = new Vector3(5.35914f, gameObject.transform.position.y + climbSpeed, startClimbPosition.z);
			spriteRender.sprite = AlClimb[imageIndex%4 + 2];
		}
	}

	private void initClimb()
	{
		if(!climbStart)
		{
			imageIndex = 0;
			climbStart = true;
			alCollider.isTrigger = true;
			spriteRender.sprite = AlClimb[imageIndex];
			gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.090f, gameObject.transform.position.z);
			
			startClimbPosition = gameObject.transform.position;
		}
		else if(imageIndex == 1)
		{
			gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.365349f, startClimbPosition.z);
			
			spriteRender.sprite = AlClimb[imageIndex];
		}
		else if(imageIndex == 2)
		{
			gameObject.transform.position = new Vector3(5.35914f, startClimbPosition.y - 0.665349f, startClimbPosition.z);
			spriteRender.sprite = AlClimb[imageIndex];

			climbReady = true;
		}
	}

	public int _Health
	{
		get{return playerHealth;}
		set{playerHealth = value;}
	}

	void updateImage()
	{
		if(time > imageSpeed)
		{
			time = 0;
			
			imageIndex = (imageIndex + 1);
		}
		
		time += Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground")
			onGround = true;
	//	Debug.Log(coll.gameObject.renderer.bounds.size);
	}
	
	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground")
			onGround = false;
	}

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag =="LadderTop")
		{

		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.tag =="LadderTop")
		{
			if(Input.GetKeyDown(KeyCode.DownArrow) && !isClimb)
			{	isClimb = true;

				gameObject.transform.position = coll.gameObject.transform.position;
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll)
	{
		if(coll.gameObject.tag =="LadderTop")
		{
			if(Input.GetKeyDown(KeyCode.DownArrow) && !isClimb)
			{	
				isClimb = false;			
			}
		}
	}
}
