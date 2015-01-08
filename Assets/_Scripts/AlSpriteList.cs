using UnityEngine;
using System.Collections;

public class AlSpriteList : MonoBehaviour {

	public SpriteRenderer spriteRend;
	private int spriteIndex;
	public Sprite spriteIdle;
	private float time;
	public string play = "walk";

	public float spriteSpeed;
	public bool playLoop;
	public bool playOnce;
	public bool playIdle;

	public Sprite AlIde;
	public Sprite[] AlWalk;
	public Sprite[] AlClimb;
	public Sprite[] AlJump;
	public Sprite[] AlDoor;
	public Sprite[] AlFalling;
	public Sprite[] AlOnFire;
	public Sprite[] AlHitOnce;
	public Sprite[] AlHold;
	public Sprite[] AlUse;
	public Sprite[] AlJetpack;
	public Sprite[] AlThinking;
	public Sprite[] AlWalkHolding;

	// Use this for initialization
	void Start () {
		spriteIdle = AlIde;
	}
	
	// Update is called once per frame
	void Update () {
	//	playAnimation(play);
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

	public void playAnimation(string anim)
	{
		switch(anim)
		{
		case "walk":
			animateSprite(AlWalk);
			break;
		case "climb":
			animateSprite(AlClimb);
			break;
		case "jump":
			animateSpriteOnce(AlJump);
			break;
		case "door":
			animateSpriteOnce(AlDoor);
			break;
		case "falling":
			animateSpriteOnce(AlFalling);
			break;
		case "onfire":
			animateSprite(AlOnFire);
			break;
		case "hit":
			animateSpriteOnce(AlHitOnce);
			break;
		case "use":
			animateSpriteOnce(AlUse);
			break;
		case "jetpack":
			animateSprite(AlJetpack);
			break;
		case "thinking":
			animateSpriteOnce(AlThinking);
			break;
		case "walkHolding":
			animateSprite(AlWalkHolding);
			break;
		default:
			spriteRend.sprite = AlIde;
		//	Debug.Log("something went wrong with animation");
			break;
		}
	}
}
