using UnityEngine;
using System.Collections;

public class Animate : MonoBehaviour {

	public SpriteRenderer spriteRend;
	public Sprite spriteIdle;
	public Sprite[] spriteObj;

	private int spriteIndex;
	private float time;

	public float spriteSpeed;
	public bool playLoop;
	public bool playOnce;
	public bool playIdle;

	// Use this for initialization
	void Start () {
		spriteRend = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
		if(playLoop)
			animateSprite(spriteObj);
		else if(playOnce)
			playOnce = animateSpriteOnce(spriteObj);

		if(playIdle)
			spriteRend.sprite = spriteIdle;
	}

	public void animateSprite(Sprite[] sprites)
	{
		if(time > spriteSpeed)
		{
			time = 0;
			spriteIndex = (spriteIndex + 1) % sprites.Length;
		}

		spriteRend.sprite = sprites[(int)spriteIndex];

		time += Time.deltaTime;
	}

	public bool animateSpriteOnce(Sprite[] sprites)
	{
		if((spriteIndex + 1) < sprites.Length)
		{
			if(time > spriteSpeed)
			{
				time = 0;
				spriteIndex = spriteIndex + 1;
			}

			spriteRend.sprite = sprites[(int)spriteIndex];
			time += Time.deltaTime;

			return true;
		}
		else
		{	
			spriteIdle = sprites[sprites.Length-1];

			playLoop = false;

			spriteIndex = 0;

			return false;
		}

		return false;
	}
}
