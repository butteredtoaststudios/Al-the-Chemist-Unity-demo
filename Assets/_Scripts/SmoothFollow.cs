using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
	
	Transform target;
	float smoothTime = 0.0f;
	Transform thisTransform;
	private Vector2 velocity;

	public float roomWidth;
	public float roomHeight;

	private float boundX;
	private float boundY;
	
	void Start()
	{
		thisTransform = transform;

		boundX = (3.6f) + ((roomWidth/2) - 720)/100;
		boundY = (2.4f) + ((roomHeight/2) - 480)/100;

	
	}
	
	void Update() 
	{
		if(target == null)
		{
			if(GameObject.FindGameObjectWithTag("Player") != null)
				target = GameObject.FindGameObjectWithTag("Player").transform;
		}
		else
		{
			if(Mathf.SmoothDamp( thisTransform.position.x, target.position.x, ref velocity.x, smoothTime)  > -boundX &&
			   Mathf.SmoothDamp( thisTransform.position.x, target.position.x, ref velocity.x, smoothTime)  < boundX)
			{
				float newX = Mathf.SmoothDamp( thisTransform.position.x, target.position.x, ref velocity.x, smoothTime);

				thisTransform.position = new Vector3(newX, thisTransform.position.y, thisTransform.position.z);
			}
			else if(Mathf.SmoothDamp( thisTransform.position.x, target.position.x, ref velocity.x, smoothTime)  <= -boundX)
				thisTransform.position = new Vector3(-boundX, thisTransform.position.y, thisTransform.position.z);
			else 
				thisTransform.position = new Vector3(boundX, thisTransform.position.y, thisTransform.position.z);


			if(Mathf.SmoothDamp( thisTransform.position.y,target.position.y, ref velocity.y, smoothTime)  < boundY &&
			   Mathf.SmoothDamp( thisTransform.position.y,target.position.y, ref velocity.y, smoothTime)  > -boundY)
			{	
				float newY = Mathf.SmoothDamp( thisTransform.position.y, target.position.y, ref velocity.y, smoothTime);	
				thisTransform.position = new Vector3(thisTransform.position.x, newY, thisTransform.position.z);
			}
			else if(Mathf.SmoothDamp( thisTransform.position.y,target.position.y, ref velocity.y, smoothTime)  > boundY)
				thisTransform.position = new Vector3(thisTransform.position.x, boundY, thisTransform.position.z);
			else
				thisTransform.position = new Vector3(thisTransform.position.x, -boundY, thisTransform.position.z);
		}
	}


	public void updateCamera(Vector3 newPosition)
	{
		if(target == null)
		{
			if(GameObject.FindGameObjectWithTag("Player") != null)
				target = GameObject.FindGameObjectWithTag("Player").transform;
		}
		
		if(Mathf.SmoothDamp( thisTransform.position.x, newPosition.x, ref velocity.x, smoothTime)  > -boundX &&
		   Mathf.SmoothDamp( thisTransform.position.x, newPosition.x, ref velocity.x, smoothTime)  < boundX)
		{
			float newX = Mathf.SmoothDamp( thisTransform.position.x, newPosition.x, ref velocity.x, smoothTime);
			
			thisTransform.position = new Vector3(newX, thisTransform.position.y, thisTransform.position.z);
		}
		else if(Mathf.SmoothDamp( thisTransform.position.x, newPosition.x, ref velocity.x, smoothTime)  <= -boundX)
			thisTransform.position = new Vector3(-boundX, thisTransform.position.y, thisTransform.position.z);
		else 
			thisTransform.position = new Vector3(boundX, thisTransform.position.y, thisTransform.position.z);
		
		
		if(Mathf.SmoothDamp( thisTransform.position.y, newPosition.y, ref velocity.y, smoothTime)  < boundY &&
		   Mathf.SmoothDamp( thisTransform.position.y, newPosition.y, ref velocity.y, smoothTime)  > -boundY)
		{	
			float newY = Mathf.SmoothDamp( thisTransform.position.y, newPosition.y, ref velocity.y, smoothTime);	
			thisTransform.position = new Vector3(thisTransform.position.x, newY, thisTransform.position.z);
		}
		else if(Mathf.SmoothDamp( thisTransform.position.y, newPosition.y, ref velocity.y, smoothTime)  > boundY)
			thisTransform.position = new Vector3(thisTransform.position.x, boundY, thisTransform.position.z);
		else
			thisTransform.position = new Vector3(thisTransform.position.x, -boundY, thisTransform.position.z);

	}
}
