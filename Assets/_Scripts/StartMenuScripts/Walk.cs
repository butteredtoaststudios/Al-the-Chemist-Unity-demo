using UnityEngine;
using System.Collections;

public class Walk : MonoBehaviour {

	public float moveSpeed = 0.05f;
	public float imageSpeed = 0.2f;
	private bool walkRight = true;

	public Sprite[] walk;
	public SpriteRenderer spriteRender;

	public Sprite[] sprays;
	public SpriteRenderer sprayRender;

	float time;
	int imageIndex = 0;

	GameObject spray;

	// Use this for initialization
	void Start () {
		spray = GameObject.Find("Spray");
	}

	// Update is called once per frame
	void Update () 
	{
		if(walk.Length > 0)
		{
			spriteRender.sprite = walk[(imageIndex + 1) % walk.Length];
		}
		if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x < 720 && walkRight)
		{	
			gameObject.transform.position = new Vector3(gameObject.transform.position.x + moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 0, gameObject.transform.rotation.z, gameObject.transform.rotation.w);
		
			spray.transform.position = new Vector3(gameObject.transform.position.x + 0.125914f, spray.transform.position.y, spray.transform.position.z);
			spray.transform.rotation = new  Quaternion(spray.transform.rotation.x, 0, spray.transform.rotation.z, spray.transform.rotation.w);
		}
		else if(Camera.main.WorldToScreenPoint(gameObject.transform.position).x > 0 && !walkRight)
		{	
			gameObject.transform.position = new Vector3(gameObject.transform.position.x - moveSpeed, gameObject.transform.position .y, gameObject.transform.position .z);
			gameObject.transform.rotation = new  Quaternion(gameObject.transform.rotation.x, 180, gameObject.transform.rotation.z, gameObject.transform.rotation.w);

			spray.transform.position = new Vector3(gameObject.transform.position.x - 0.117086f, spray.transform.position.y, spray.transform.position.z);
			spray.transform.rotation = new  Quaternion(spray.transform.rotation.x, 180, spray.transform.rotation.z, spray.transform.rotation.w);
		}
		else
		{
			sprayRender.sprite = sprays[Random.Range(0, sprays.Length - 1)];
			walkRight = !walkRight;
		}
		if(time > imageSpeed)
		{
			time = 0;
			
			imageIndex = (imageIndex + 1);
		}

		time += Time.deltaTime;
	}
}
