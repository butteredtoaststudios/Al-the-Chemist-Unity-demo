using UnityEngine;
using System.Collections;

public class TutorialCamera : MonoBehaviour {

	private float roomWidth = 1440;
	private float roomHeight = 1152;

	private float boundX;
	private float boundY;

	private bool[] movePosition = new bool[]{true, false, false, false};

	public float circleSpeed = 0.01f;
	// Use this for initialization
	void Start () {

		boundX = (3.6f) + ((roomWidth/2) - 720)/100;
		boundY = (2.4f) + ((roomHeight/2) - 480)/100;
	}
	
	// Update is called once per frame
	void Update () {


		if(movePosition[0])
		{
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + circleSpeed, Camera.main.transform.position.y, Camera.main.transform.position.z);
		}
		else if(movePosition[1])
		{
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y - circleSpeed, Camera.main.transform.position.z);
		}
		else if(movePosition[2])
		{
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x - circleSpeed, Camera.main.transform.position.y, Camera.main.transform.position.z);
		}
		else if(movePosition[3])
		{
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + circleSpeed, Camera.main.transform.position.z);
		}

		if(movePosition[0] && Camera.main.transform.position.x > 3.4)
		{
			movePosition[0] = false;
			movePosition[1] = true;
		}

		if(movePosition[1] && Camera.main.transform.position.y < -3.1)
		{
			movePosition[1] = false;
			movePosition[2] = true;
		}

		if(movePosition[2] && Camera.main.transform.position.x < -3.4)
		{
			movePosition[2] = false;
			movePosition[3] = true;
		}

		if(movePosition[3] && Camera.main.transform.position.y > 3.2)
		{
			movePosition[3] = false;
			movePosition[0] = true;
		}
	}
}
