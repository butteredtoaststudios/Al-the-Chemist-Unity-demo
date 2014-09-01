
#pragma strict

var target : Transform;
var smoothTime = 0.3;
private var thisTransform : Transform;
private var velocity : Vector2;

public var roomWidth:float;
public var roomHeight:float;

function Start()
{
	roomWidth = 10.8f;
	thisTransform = transform;
}

function Update() 
{
	if(Mathf.SmoothDamp( thisTransform.position.x, target.position.x, velocity.x, smoothTime)  > 3.6f &&
		Mathf.SmoothDamp( thisTransform.position.x, target.position.x, velocity.x, smoothTime)  < roomWidth)
	{
		thisTransform.position.x = Mathf.SmoothDamp( thisTransform.position.x, 
			target.position.x, velocity.x, smoothTime);
	}
	
	if(Mathf.SmoothDamp( thisTransform.position.y,target.position.y, velocity.y, smoothTime)  < -2.4f)
	{	
		thisTransform.position.y = Mathf.SmoothDamp( thisTransform.position.y, 
			target.position.y, velocity.y, smoothTime);	
	}
}