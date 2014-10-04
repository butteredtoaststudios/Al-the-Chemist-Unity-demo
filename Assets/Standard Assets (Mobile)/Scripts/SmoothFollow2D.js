
#pragma strict

var target : Transform;
var smoothTime = 0.3;
private var thisTransform : Transform;
private var velocity : Vector2;

public var rightPosition:float;

public var roomWidth:float;
public var topPosition:float;
public var bottomPosition:float;

function Start()
{
	thisTransform = transform;
}

function Update() 
{
	if(target == null)
	{
		if(GameObject.FindGameObjectWithTag("Player") != null)
			target = GameObject.FindGameObjectWithTag("Player").transform;
	}

	if(Mathf.SmoothDamp( thisTransform.position.x, target.position.x, velocity.x, smoothTime)  > rightPosition &&
		Mathf.SmoothDamp( thisTransform.position.x, target.position.x, velocity.x, smoothTime)  < roomWidth)
	{
		thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, target.position.x, velocity.x, smoothTime);
			
		Debug.Log(thisTransform.position);
	}
	
	if(Mathf.SmoothDamp( thisTransform.position.y,target.position.y, velocity.y, smoothTime)  < topPosition &&
	Mathf.SmoothDamp( thisTransform.position.y,target.position.y, velocity.y, smoothTime)  > (topPosition * -1))
	{	
		thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, target.position.y, velocity.y, smoothTime);	
	}
}