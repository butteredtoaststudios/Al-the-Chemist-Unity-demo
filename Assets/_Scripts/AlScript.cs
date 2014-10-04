
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

	private BoxCollider2D alCollider;
	globalObject go;

	// Use this for initialization
	void Start () {

		go = GameObject.Find("GlobalObject").GetComponent<globalObject>();
		alCollider = gameObject.GetComponent<BoxCollider2D>();
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
			StartCoroutine(climbRevFunc(0.25f));

		if(isClimb && !climbStart)
			StartCoroutine(climbStartClimbFunc(0.25f));

		if(isClimb && (climbStart || climbBottom))
			onClimbKey();

		updateImage();
	}

	private void onWalkKey()
	{
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - moveSpeed > 0)//Camera.main.GetComponent<SmoothFollow2D>())
				gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);

	//		gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 180, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
			go.playerFaceRight = false;
			updateImage();
			spriteRender.sprite = AlSprite[(imageIndex + 1) % AlSprite.Length];
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x - moveSpeed < Screen.width)
				gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
	//		gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
			go.playerFaceRight = true;
			updateImage();
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

	private IEnumerator climbStartClimbFunc(float speed)
	{
		spriteRender.sprite = AlClimb[0];
		gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.090f, startClimbPosition.z);
		
		yield return new WaitForSeconds(speed);

		gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.365349f - 0.090f, startClimbPosition.z);
		spriteRender.sprite = AlClimb[1];

		yield return new WaitForSeconds(speed);

		gameObject.transform.position = new Vector3(startClimbPosition.x - 0.089f, startClimbPosition.y - 0.665349f, startClimbPosition.z);
		spriteRender.sprite = AlClimb[2];

		imageIndex = 0;
		climbStart = true;
	}

	private IEnumerator climbRevFunc(float speed)
	{
		gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.365349f - 0.090f, startClimbPosition.z);
		spriteRender.sprite = AlClimb[1];

		yield return new WaitForSeconds(speed);

		spriteRender.sprite = AlClimb[0];
		gameObject.transform.position = new Vector3(startClimbPosition.x, startClimbPosition.y - 0.090f, startClimbPosition.z);
		
		yield return new WaitForSeconds(speed);

		spriteRender.sprite = AlIdle;
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, startClimbPosition.y + 0.3f, startClimbPosition.z);

		climbReverse = false;
		climbStart = false;
		climbBottom = false;
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

#if HIDE
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground")
			onGround = true;
	}
	
	void OnCollisionExit2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ground")
			onGround = false;
	}
#endif 

	void OnTriggerEnter2D(Collider2D coll)
	{
		if(coll.gameObject.tag =="LadderBottom")
			climbBottom = true;

		if (coll.gameObject.tag == "Ground")
		{	
			if(gameObject.transform.position.y < coll.gameObject.transform.position.y)
			{	
				isJumping = false;
			}
			else
				onGround = true;

			if(climbBottom)
				isClimb = false;
		}
	}

	void OnTriggerStay2D(Collider2D coll)
	{
		if(coll.gameObject.tag =="LadderTop")
		{
			if(Input.GetKeyDown(KeyCode.DownArrow) && !isClimb)
			{	
				isClimb = true;
				startClimbPosition = coll.gameObject.transform.position;
				gameObject.transform.position = coll.gameObject.transform.position;
			}

			if(Input.GetKey(KeyCode.UpArrow))
			{	
				startClimbPosition = coll.gameObject.transform.position;
				climbReverse = true;
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
